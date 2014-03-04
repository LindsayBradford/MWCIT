''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports NUnit.Framework
Imports NSubstitute

Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Security
Imports System.Security.Permissions

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

<TestFixture(), Category("Integration")>
Public Class TestLauncherSetBuilder

  Private _launcherSetBuilder As ILauncherSetBuilder
  Private _assemblyResourcePaths() As String

  Structure TestPaths
    Public baseLaunchDirectory As String
    Public validLaunchPath As String
    Public invalidLaunchPath As String
  End Structure

  Private _paths As TestPaths

  Enum Resources
    INVALID_CONFIG = 0
    VALID_CONFIG = 1
  End Enum

  <SetUp()>
  Public Sub Setup()

    _paths = New TestPaths

    _assemblyResourcePaths =
      AssemblyUtiityCollection.UnpackResources(
        [Assembly].GetExecutingAssembly(),
        FileUtilityCollection.GetTempPath()
      )

    _launcherSetBuilder = Nothing

  End Sub

  <Test()>
  Public Sub BuildFrom_ExecutingAssembly_SetCountIs1()

    _launcherSetBuilder = New LauncherSetBuilder()

    ' Because the builder interrogates the assembly supplied for implementations of needed interfaces, 
    ' we supply mock test implementations of those interfaces the builder instantiates below. 

    Dim launcherSet = _launcherSetBuilder.BuildFrom(
      Assembly.GetExecutingAssembly,
      Assembly.GetExecutingAssembly.Location
    )

    ' The assembly's location is assumed to have no sub-directories. The launcherSet should only contain
    ' the 'About' entry, built from the mock interface implementations below. 

    Assert.AreEqual(1, launcherSet.Count())
  End Sub

  <Test()>
  Public Sub BuildFrom_ValidBuildConfig_SetCountIs3()

    _paths.baseLaunchDirectory =
      String.Format(
        "{0}{1}{2}",
        FileUtilityCollection.GetTempPath(),
        Path.AltDirectorySeparatorChar,
        Guid.NewGuid
      )

    Directory.CreateDirectory(_paths.baseLaunchDirectory)

    _paths.validLaunchPath =
      String.Format(
        "{0}{1}{2}",
        _paths.baseLaunchDirectory,
        Path.AltDirectorySeparatorChar,
        "VALID"
      )

    Directory.CreateDirectory(_paths.validLaunchPath)

    _paths.validLaunchPath =
      String.Format(
        "{0}{1}{2}",
        _paths.validLaunchPath,
        Path.AltDirectorySeparatorChar,
        "Launcher.INI"
      )

    File.Move(
      _assemblyResourcePaths(Resources.VALID_CONFIG),
      _paths.validLaunchPath
    )

    _paths.invalidLaunchPath =
      String.Format(
        "{0}{1}{2}",
        _paths.baseLaunchDirectory,
        Path.AltDirectorySeparatorChar,
        "INVALID"
      )

    Directory.CreateDirectory(_paths.invalidLaunchPath)

    _paths.invalidLaunchPath =
      String.Format(
        "{0}{1}{2}",
        _paths.invalidLaunchPath,
        Path.AltDirectorySeparatorChar,
        "Launcher.INI"
      )

    File.Move(
      _assemblyResourcePaths(Resources.INVALID_CONFIG),
      _paths.invalidLaunchPath
    )

    _launcherSetBuilder = New LauncherSetBuilder()

    ' Because the builder interrogates the assembly supplied for implementations of needed interfaces, 
    ' we supply mock test implementations of those interfaces the builder instantiates below. 

    Dim launcherSet = _launcherSetBuilder.BuildFrom(
      Assembly.GetExecutingAssembly,
      _paths.baseLaunchDirectory
    )

    'The count should have the two external launchers set up from above + the one default internal launcher.

    Assert.AreEqual(3, launcherSet.Count())

    Directory.Delete(_paths.baseLaunchDirectory, True)
  End Sub

  <Test()>
  Public Sub BuildFrom_InvalidBuildConfig_SetCountIs1()

    _launcherSetBuilder = New LauncherSetBuilder()

    ' Because the builder interrogates the assembly supplied for implementations of needed interfaces, 
    ' we supply mock test implementations of those interfaces the builder instantiates below. 

    Dim launcherSet = _launcherSetBuilder.BuildFrom(
      Assembly.GetExecutingAssembly,
      Guid.NewGuid.ToString()
    )

    ' The assembly's location is assumed to have no sub-directories. The launcherSet should only contain
    ' the 'About' entry, built from the mock interface implementations below. 

    Assert.AreEqual(1, launcherSet.Count())
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub BuildFrom_InvalidAssembly_ArgumentException()

    _launcherSetBuilder = New LauncherSetBuilder()

    Dim launcherSet = _launcherSetBuilder.BuildFrom(
      Nothing,
      Guid.NewGuid.ToString()
    )
  End Sub

End Class






