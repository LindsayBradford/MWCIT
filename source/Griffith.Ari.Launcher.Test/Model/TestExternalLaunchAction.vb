''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports NUnit.Framework
Imports NSubstitute

Imports System.Reflection
Imports System.Security
Imports System.Security.Permissions

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

<TestFixture(), Category("Unit")>
Public Class TestExternalLaunchAction

  Private _launcher As ExternalLaunchAction

  <SetUp()>
  Public Sub Setup()

    _launcher = Nothing

  End Sub

  <TearDown()>
  Public Sub TearDown()
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub Launch_URLasAction_ArgumentException()

    _launcher = New ExternalLaunchAction("http://127.0.0.1/", FileUtilityCollection.GetTempPath)

    _launcher.Launch()

  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub Constructor_NoAction_ArgumentException()

    _launcher = New ExternalLaunchAction(Nothing, FileUtilityCollection.GetTempPath)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub Constructor_EmptyAction_ArgumentException()

    _launcher = New ExternalLaunchAction("", FileUtilityCollection.GetTempPath)

    _launcher.Launch()

  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub Constructor_NoWorkingDirectory_ArgumentException()
    _launcher = New ExternalLaunchAction(Nothing, Nothing)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub Constructor_EmptyWorkingDirectory_ArgumentException()
    _launcher = New ExternalLaunchAction(Nothing, "")
  End Sub

End Class

