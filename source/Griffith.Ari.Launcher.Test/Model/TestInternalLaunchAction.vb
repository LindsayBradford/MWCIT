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
Public Class TestInternalLaunchAction

  Private Enum LAUNCH_RESULT
    UNTESTED = 0
    VIEW_SHOWN = 1
  End Enum

  Private _launchResult As LAUNCH_RESULT

  Private _fakeActionView As IInternalLaunchActionView
  Private _launcher As InternalLaunchAction

  <TestFixtureSetUp()>
  Public Sub TestFixtureSetup()

    _fakeActionView = Substitute.For(Of IInternalLaunchActionView)()
    _fakeActionView.When(
      Sub(x) x.Show()
    ).Do(
      Sub(x) _launchResult = LAUNCH_RESULT.VIEW_SHOWN
    )

  End Sub

  <SetUp()>
  Public Sub Setup()

    _launchResult = LAUNCH_RESULT.UNTESTED
    _launcher = Nothing

  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub Constructor_NoActionView_ArgumentException()

    _launcher = New InternalLaunchAction(Nothing)

  End Sub

  <Test()>
  Public Sub Launch_ValidFakeView_ViewShown()

    _launcher = New InternalLaunchAction(_fakeActionView)

    _launcher.Launch()

    Assert.AreEqual(
      LAUNCH_RESULT.VIEW_SHOWN,
      _launchResult
    )
  End Sub
End Class

