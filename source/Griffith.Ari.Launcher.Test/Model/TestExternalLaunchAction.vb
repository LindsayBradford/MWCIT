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

  Private launcher As ExternalLaunchAction

  <SetUp()>
  Public Sub Setup()

    launcher = Nothing

  End Sub

  <TearDown()>
  Public Sub TearDown()
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testURLLaunchAttempt()

    launcher = New ExternalLaunchAction("http://127.0.0.1/", FileUtilityCollection.GetTempPath)

    launcher.Launch()

  End Sub

  <Test()>
  Public Sub testNothingActionLaunchAttempt()

    launcher = New ExternalLaunchAction(Nothing, FileUtilityCollection.GetTempPath)

    launcher.Launch()

  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testNothingWorkingDirectoryAttempt()
    launcher = New ExternalLaunchAction(Nothing, Nothing)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testEmptyWorkingDirectoryAttempt()
    launcher = New ExternalLaunchAction(Nothing, "")
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testEmptyActionLaunchAttempt()

    launcher = New ExternalLaunchAction("", FileUtilityCollection.GetTempPath)

    launcher.Launch()

  End Sub
End Class

