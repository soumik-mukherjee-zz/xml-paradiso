## Instructions for Release v1.0 - Building and working with this Example

### What this example is about
XML, and file based integrations, are not yet a thing of the past. And while they used to pose "happy" challenges to developers, the toolset community has forever evolved to reduce the "burden" of technology on us. Today we will be exploring a particular problem set:
- A B2B integration scenario which begins with the definition of a data model, often in the form of a XSD, in the XML world. Developers will have little or no control on how the XSD is defined but expected to write code that produces XML messages adhereing to the XSD. This approach is often referred to a "Contract First" (the opposite being "Code first").
- With a traditional XML programming paradigm, developer required polygot skills - one set focussing on the core platform (e.g. .NET) and the other on XML APIs and specs (e.g. the well known [W3C DOM](https://www.w3.org/DOM/)). 
- While toolsets have reduced this burden, there are competeing products, often from the same vendor, with frequently changing roadmaps. e.g. .NET Framework DataContractSerializer vs XmlSerializer (there is a good read on the subject [here](http://www.cnblogs.com/asingna/archive/2011/11/06/2237904.html)). Amongst the pitfalls, is the risk of your code falling out of vendor support.
- Toolsets automating the "Code first" workflow are widely popular, while those in the "Contract First" world, not as much. This has less to do with the fact of the toolset's maturity, and more to do with the natural preference for a "Code first" approach in a majority of cases. However this problem magnifies when "Code First" is not an option (e.g. B2B integrations)

Throughout this example, we will try to address the above problems, with a certain choice of toolset, i.e. WCF & [DataContractSerializer](https://msdn.microsoft.com/en-us/library/ms731073(v=vs.110).aspx)
- We will start with a XSD that has already been defined
- See how WCF toolsets can automate DataContract generation, specifically `svcutil` ([MSDN link](https://msdn.microsoft.com/en-us/library/aa347733(v=vs.110).aspx)). This may not seem an obvious choice, but `svcutil` can go beyond its percieved role. The competing toolset (which is already losing favor from within MS) would be `Xsd.exe` ([MSDN link](https://msdn.microsoft.com/en-us/library/x6c1kb0s(v=vs.110).aspx))
- While "Contract First" has out of box support with `svcutil` on .NET 4.5+, its not the end of the world for earthlings on .NET 4.0. We will explore how we can achieve this.
- How the DataContract, once "defined", eases XML generation to a huge extent

### The development workflow
We are dealing with a scenario, where data needs to be fetched/processed/transformed, resulting in .NET objects, which are then finally transformed in XML. The concern of transport, i.e. file, is rather trivial, compared to the rest. The diagram below shows the interaction between components that we will "develop", and their sequence of development within the worlflow.
```
                                         [1.XSD]  
                                           |
                               (Generate)  |<-----------------> svcutil
                                           |
                                           Y
  [Plain Old C# Objects]------------>[2.DataContract]------------->[XML]
                             Y                             Y
                             |                             |
                             |                             |
                [3.Business Logic in .NET]   [4.Serialization Logic in .NET]
```



1. A Windows PC. The following steps were tested on a Windows 10, 64-bit Home Edition PC.
2. .NET framework installed, 4.0 and above should work.
3. A text editor of your choice. I am using [Atom](https://atom.io/), but really anything would work. YOU DONT NEED Visual Studio, for this excercise.

### Getting the code
The code we shall use is available for download from [here](https://github.com/soumik-mukherjee/xml-paradiso/archive/v1.0.zip). Download the zip archive and extract to any locally accessible folder. All the following steps will assume it was extracted to `D:\xml-paradiso-1.0`

### A brief on the code base
Open a windows command propmt and you change directory to where you extracted the codebase. Explore the contents with a `dir` command and you will see the following inside.
``` bash
D:\>cd xml-paradiso-1.0

D:\xml-paradiso-1.0>dir
 Volume in drive D is LENOVO
 Volume Serial Number is C878-EEF6

 Directory of D:\xml-paradiso-1.0

02/12/2016  07:29    <DIR>          .
02/12/2016  07:29    <DIR>          ..
02/12/2016  06:38    <DIR>          blogs
02/12/2016  06:38    <DIR>          dotnet
02/12/2016  06:38            11,346 LICENSE
02/12/2016  06:38                 0 README.md
02/12/2016  06:38    <DIR>          xml-schemas
```
This is a dotnet console based application. The following is a description of the folders seen here:

|Folder|Purpose
|:---|:---
|`dotnet`| The root directory for all dotnet code and build files. More on this later.
|`xml-schemas`| Contains the sample XSD we will work with. In this case it the internet's favourite sample - the Purchase Order Schema, with some tweaks

Ignore the rest as of now. Change directory to `dotnet` and explore the codebase as follows:

``` bash
D:\xml-paradiso-1.0>cd dotnet

D:\xml-paradiso-1.0\dotnet>dir
 Volume in drive D is LENOVO
 Volume Serial Number is C878-EEF6

 Directory of D:\xml-paradiso-1.0\dotnet

02/12/2016  06:38    <DIR>          .
02/12/2016  06:38    <DIR>          ..
02/12/2016  06:38    <DIR>          buildfiles
02/12/2016  06:38    <DIR>          src
02/12/2016  06:38    <DIR>          tools
```
The following is a description of the folders seen here:

|Folder|Purpose
|:---|:---
|`buildfiles`| Contains the scripts that will help us in building the application. This is required only if you decide not to use Visual Studio
|`src`|Contains all the dotnet source code files we will use in this example
Ignore the rest.

### Preparing the environment
From the command prompt you need to issue the following commands. Take a note of the paths used here, they may not be exactly the same for you (will depend on both Windows and .NET framework version you are using). Adjust accordingly.
``` bash
D:\xml-paradiso-1.0\dotnet>set PATH=%PATH%;"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools";"C:\Program Files (x86)\MSBuild\14.0\Bin"
```
Once done check that the following commands run successfully, to assert the previous command is doing its job.

``` bash
Command#1
D:\xml-paradiso-1.0\dotnet>svcutil /?
Microsoft (R) Service Model Metadata Tool
[Microsoft (R) Windows (R) Communication Foundation, Version 4.6.1055.0]
Copyright (c) Microsoft Corporation.  All rights reserved.

USES:

 - Generate code from running services or static metadata documents.
 - Export metadata documents from compiled code.
 - Validate compiled service code.
 - Download metadata documents from running services.
 - Pre-generate serialization code.
 .....
 .....
 Command#2
 D:\xml-paradiso-1.0\dotnet>msbuild /?
Microsoft (R) Build Engine version 14.0.25123.0
Copyright (C) Microsoft Corporation. All rights reserved.

Syntax:              MSBuild.exe [options] [project file]

Description:         Builds the .....
 D:\xml-paradiso-1.0\dotnet>
 ```
#### Build Step 1 : Processing the XSD
From the command prompt issue the following commands:
``` bash
Command#1
D:\xml-paradiso-1.0\dotnet>cd ..

Command#2
D:\xml-paradiso-1.0>svcutil /t:code /dcOnly /namespace:"http://tempuri.org/PurchaseOrderSchema.xsd","Org.Example.DataContracts" /l:CS /serializer:DataContractSerializer /out:.\dotnet\src\Org.Example.DataContracts\PurchaseOrderDataContract.cs .\xml-schemas\PO.xsd
Microsoft (R) Service Model Metadata Tool
[Microsoft (R) Windows (R) Communication Foundation, Version 4.6.1055.0]
Copyright (c) Microsoft Corporation.  All rights reserved.

Generating files...
D:\xml-paradiso-1.0\dotnet\src\Org.Example.DataContracts\PurchaseOrderDataContract.cs

```
As explained by the  output, you will note that this tool `svcutil` has now generated a dotnet class for you. This class is nothing but a WCF DataContract, and is the quivalent of the XSD (well for a large part atleast). This DataContract is the basis of this solution example, as it automates the tasks of marshalling and un-marshalling XML data, while maintaining sanity with the XSD.
Will see more about this later. If you want, go ahead and check the contents of the file in your favorite editor.

#### Build Step 2 : Executing the build script
Before we execute the build script we need to check and adjust (if required) where the application will be generated. To  check this, open up the file `D:\xml-paradiso-1.0\dotnet\buildfiles\DemoPurchaseOrderService.exe.build` in your favorite text editor. The output path value is enclosed within the ``` <OutputPath>...</OutputPath> ``` XML elements, as shown below:

``` xml
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
<PropertyGroup>
  <AssemblyName>RunDemo</AssemblyName>
  <OutputPath>C:\MyConsoleApps\DemoPurchaseOrderService\</OutputPath>
</PropertyGroup>
  ...
</Project>
```
You must have write access to the path mentioned here. Also take a note of the path for future reference. The ** PATH NEED NOT EXIST**.

Change your directory to `buildfiles` and execute the command as shown below
``` bash
D:\xml-paradiso-1.0>cd dotnet\buildfiles

D:\xml-paradiso-1.0\dotnet\buildfiles>MSBuild DemoPurchaseOrderService.exe.build /t:Build
Microsoft (R) Build Engine version 14.0.25123.0
Copyright (C) Microsoft Corporation. All rights reserved.

Build started 02/12/2016 10:03:24.
Project "D:\xml-paradiso-1.0\dotnet\buildfiles\DemoPurchaseOrderService.exe.build" on node 1 (Build target(s)).
Build:
  C:\Program Files (x86)\MSBuild\14.0\bin\csc.exe /out:C:\MyConsoleApps\DemoPurchaseOrderService\RunDemo.exe ..\src\Org.Example.DataContracts\PurchaseOrderDataContract
  .cs ..\src\Org.Example.SampleData\PurchaseOrderSampleData.cs ..\src\Org.Example.Services\FileBasedPurchaseOrderService.cs ..\src\Org.Example.Services\IPurchaseOrderS
  ervice.cs ..\src\Org.Example.Programs\RunPurchaseOrderServiceProgram.cs
Done Building Project "D:\xml-paradiso-1.0\dotnet\buildfiles\DemoPurchaseOrderService.exe.build" (Build target(s)).


Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:07.36
```
If you followed the steps correctly so far, the build will succeed. You are now ready to run the application.

#### Step 3: Run the application
As I mentioned before, this is a console application , so we will run it from the command prompt. The application takes a single parameter as input, a Purchase Order Id, and for this example the demo data is setup with two such IDs:
1. `DummyPO1`
2. `DummyPO2`

Also, you need to recall the Output Path you set in the build script, in the previous step. If you did not change the default value in the script, the path where the application would be is:
`C:\MyConsoleApps\DemoPurchaseOrderService`
Change your directory to the output directory and explore its contents

``` bash
D:\xml-paradiso-1.0\dotnet\buildfiles>cd C:\MyConsoleApps\DemoPurchaseOrderService

D:\xml-paradiso-1.0\dotnet\buildfiles>c:

C:\MyConsoleApps\DemoPurchaseOrderService>dir
 Volume in drive C is Windows
 Volume Serial Number is 6CDE-EDB8

 Directory of C:\MyConsoleApps\DemoPurchaseOrderService

02/12/2016  03:36    <DIR>          .
02/12/2016  03:36    <DIR>          ..
02/12/2016  03:40    <DIR>          PurchaseOrders
02/12/2016  10:03            13,312 RunDemo.exe
               1 File(s)         13,312 bytes
               3 Dir(s)  800,040,124,416 bytes free
```
The `RunDemo.exe` is the application while the Purchase Order XML file will be generated in the folder `PurchaseOrders`. Go ahead and run the application with one of the Purchase Order IDs as follows:

``` bash
RunDemo.exe DummyPO1
```
or
``` bash
RunDemo.exe DummyPO2
```
Expected output:
``` bash
Successfully generated PO file for PO Id: DummyPO1.
Press any key to exit ...
```
Check the generated XML file in the `PurchaseOrders` folder. Where did the data come from? Well its all hardcoded within the source file:
`dotnet\src\Org.Example.SampleData\PurchaseOrderSampleData.cs`
You can open it with any text editor, change any of the data, rebuild the application (start from Step 2 above), and that would reflect in the out put XML.
###### Release: v1.0
