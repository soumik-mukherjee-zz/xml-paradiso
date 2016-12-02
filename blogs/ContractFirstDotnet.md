## Instructions for Release v1.0 - Building and working with this Example

### What this example is about
This example deals with the following:
1. Start with a XSD
2. Prefer WCF over others as platform
3. Employ techniques that work with .NET framework 4.0 too
4. Abstract XML/XSD complexity from developer
5. Use serialization techniques to generate XML files

### Pre-requisites
1. A Windows PC. The following steps were tested on a Windows 10, 64-bit Home Edition PC.
2. .NET framework installed, 4.0 and above should work.
3. Any good text editor. I am using Atom, but really anything would work. YOU DONT NEED Visual Studio.

### Getting the code
The code used here is available for download from [here](https://github.com/soumik-mukherjee/xml-paradiso/archive/v1.0.zip). Download the zip archive and extract to any locally accessible folder. All the following steps will assume it was extracted to `D:\xml-paradiso-1.0`

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
As explained by the  output, you will note that this tool `svcutil` has now generated a dotnet class for you. This class is nothing but a WCF DataContract, and is the quivalent of the XSD for a large part. This DataContract is the basis of this solution example, as it automates the tasks of marshalling and un-marshalling XML data, while maintaining sanity with the XSD.
Will see more of this later.

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
cd xml-paradiso\dotnet\buildfiles
```




```
MSBuild DemoPurchaseOrderService.exe.build /t:Build
```

``` bash
cd C:\MyConsoleApps\DemoPurchaseOrderService
```

``` bash
RunDemo.exe DummyPO1
```
or
``` bash
RunDemo.exe DummyPO2
```

``` bash
Successfully generated PO file for PO Id: DummyPO1.
Press any key to exit ...
```
###### Release: v1.0
