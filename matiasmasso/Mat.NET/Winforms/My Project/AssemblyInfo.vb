Imports System.Resources
Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("Winforms")>
<Assembly: AssemblyDescription("Software de Gestió Corporativa")>
<Assembly: AssemblyCompany("MATIAS MASSO, S.A.")>
<Assembly: AssemblyProduct("Mat.NET")>
<Assembly: AssemblyCopyright("Copyright ©  2015")>
<Assembly: AssemblyTrademark("Matsoft")>

'Added by Mat on 07/01/2020 to solve a bug on VS2019 v16.4.2 that throws eror on reading dates:
<Assembly: DebuggerDisplay("{ToString}", Target:=GetType(Date))>

<Assembly: ComVisible(False)>

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("d88efeba-aae1-42d8-bf90-85aa1e9d1658")>

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("1.0.0.0")>
<Assembly: AssemblyFileVersion("1.0.0.0")>
<Assembly: NeutralResourcesLanguage("es-ES")>
