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

  Private _actionViewMock As IInternalLaunchActionView
  Private _launcher As InternalLaunchAction

  <TestFixtureSetUp()>
  Public Sub TestFixtureSetup()

    _actionViewMock = Substitute.For(Of IInternalLaunchActionView)()
    _actionViewMock.When(
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
  Public Sub testNothingActionViewInitialisation()

    _launcher = New InternalLaunchAction(Nothing)

  End Sub

  <Test()>
  Public Sub testActionLaunch()

    _launcher = New InternalLaunchAction(_actionViewMock)

    Assert.AreEqual(
      LAUNCH_RESULT.UNTESTED,
      _launchResult
    )

    _launcher.Launch()

    Assert.AreEqual(
      LAUNCH_RESULT.VIEW_SHOWN,
      _launchResult
    )
  End Sub


End Class

