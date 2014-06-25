''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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

