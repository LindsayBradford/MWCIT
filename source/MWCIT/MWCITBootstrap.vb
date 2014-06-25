''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.IO
Imports System.Reflection

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher

''' <summary>
''' This is the class that bootstraps the running of a WinForms-fronted Launcher project configured 
''' for the MWCIT project. 
''' </summary>
''' <remarks></remarks>

Public NotInheritable Class MWCITBootstrap

  Private Enum ERROR_LEVEL
    Success = 0
    Failure = 1
  End Enum

  ''' <summary>
  ''' The entry-point for the executable produced by the MWCIT project. Sets up retrieval of 
  ''' embedded assemblies (allowing for a single exeutable assembly being deployed), then bootstraps the
  ''' construction of the User-interface. 
  ''' </summary>
  ''' <param name="args"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>

  Public Shared Function Main(args() As String) As Integer

#If Config = "Release" Then
    ' For potential issues, see: http://www.aboutmycode.com/net-framework/assemblyresolve-event-tips/
    AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf HandleAssemblyRetrieval
#End If

    BootstrapGUI()

    Return ERROR_LEVEL.Success
  End Function

  ''' <summary>
  ''' Bootstraps the construction of the user-interface for the MWCIT bo calling down into the
  ''' LauncherBuilder, telling it to build the view from this assembly, and the INI file found
  ''' in the same directory as this assembly. 
  ''' </summary>
  ''' <remarks></remarks>

  Private Shared Sub BootstrapGUI()
    Dim builder = New LauncherSetBuilder()

    Dim thisAssembly = Assembly.GetExecutingAssembly()
    Dim entryAssemblyPath = Path.GetDirectoryName(thisAssembly.Location)

    builder.BuildFrom(
      thisAssembly,
      INIFileCollection.ReadValue(
        Path.Combine(entryAssemblyPath, "MWCIT.INI"),
        "MWCIT",
        "DataDirectory"
      )
    ).Show()
  End Sub

  ''' <summary>
  ''' Retrieves an unresolved Assembly by fetching it from this assembly as an embedded resource.
  ''' </summary>
  ''' <remarks>
  ''' This method must exist in the entry assembly. Placing it in a dependent assmebly defeats the purpose.
  ''' All assemblies that must ship with this entry assembly should be included within it as an embedded resource. 
  ''' </remarks>

  Private Shared Function HandleAssemblyRetrieval(sender As Object, args As System.ResolveEventArgs) As System.Reflection.Assembly
    Dim entryAssembly = [Assembly].GetEntryAssembly()
    Dim entryAssemblyQualifier = entryAssembly.GetName().Name
    Dim unqualifiedAssemblyName = New AssemblyName(args.Name).Name

    Dim qualifiedAssemblyName = String.Format("{0}.{1}.dll", entryAssemblyQualifier, unqualifiedAssemblyName)

    Using stream = entryAssembly.GetManifestResourceStream(qualifiedAssemblyName)

      Dim assemblyDataSize As Integer = CInt(stream.Length - 1)

      Dim assemblyData = New Byte(assemblyDataSize) {}

      stream.Read(assemblyData, 0, assemblyData.Length)

      stream.Close()

      Return Assembly.Load(assemblyData)

    End Using ' stream
  End Function

End Class
