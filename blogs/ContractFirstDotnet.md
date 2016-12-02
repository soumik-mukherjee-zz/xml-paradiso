
``` bash
C:\>set PATH=%PATH%;"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools";"C:\Program Files (x86)\MSBuild\14.0\Bin"
```
``` bash
C:\>cd xml-paradiso
C:\Users\PC owner\GitLab\xml-paradiso>dir
Volume in drive C is Windows
 Volume Serial Number is 6CDE-EDB8

 Directory of C:\Users\PC owner\GitLab\xml-paradiso

01/12/2016  18:25    <DIR>          .
01/12/2016  18:25    <DIR>          ..
01/12/2016  18:25    <DIR>          blogs
30/11/2016  17:53    <DIR>          dotnet
30/11/2016  15:39            11,547 LICENSE
30/11/2016  15:39                 0 README.md
30/11/2016  15:39    <DIR>          xml-schemas
               2 File(s)         11,547 bytes
               5 Dir(s)  800,248,336,384 bytes free
```

``` bash
svcutil /t:code /dcOnly /namespace:"http://tempuri.org/PurchaseOrderSchema.xsd","Org.Example.DataContracts" /l:CS /serializer:DataContractSerializer /out:.\dotnet\src\Org.Example.DataContracts\PurchaseOrderDataContract.cs .\xml-schemas\PO.xsd```

``` bash
cd xml-paradiso\dotnet\buildfiles
```

``` xml
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
<PropertyGroup>
  <AssemblyName>RunDemo</AssemblyName>
  <OutputPath>C:\MyConsoleApps\DemoPurchaseOrderService\</OutputPath>
</PropertyGroup>
  ...
</Project>
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
