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
Public Class TestLauncherPresenter

  Private _fakeLauncherView As ILauncherView

  Private _fakeInternalModel As IInternalLauncherModel
  Private _fakeExternalModel As IExternalLauncherModel

  Private _fakeInternalLaunchAction As ILaunchAction
  Private _fakeExternalLaunchAction As ILaunchAction

  Private _errorLaunchAction As ExternalLaunchAction

  Private _fakeErrorView As ILaunchActionErrorView
  Private _fakeSecurityView As ILaunchActionSecurityErrorView

  Private Enum LAUNCH_RESULT
    UNTESTED = 0
    SECURITY_MESSAGE_SHOWN = 1
    ERROR_MESSAGE_SHOWN = 2
    INTERNAL_ACTION_LAUNCHED = 3
    EXTERNAL_ACTION_LAUNCHED = 4
  End Enum

  Private _launchResult As LAUNCH_RESULT = LAUNCH_RESULT.UNTESTED
  Private _launcherPresenter As ILauncherPresenter

  <SetUp()>
  Public Sub Setup()

    _launcherPresenter = Nothing
    _launchResult = LAUNCH_RESULT.UNTESTED

    _fakeLauncherView = Substitute.For(Of ILauncherView)()
    _fakeLauncherView.ShortName.Returns("testViewShortName")

    _fakeInternalLaunchAction = Substitute.For(Of ILaunchAction)()
    _fakeInternalLaunchAction.When(
      Sub(x) x.Launch()
    ).Do(
      Sub(x) _launchResult = LAUNCH_RESULT.INTERNAL_ACTION_LAUNCHED
    )

    _fakeInternalModel = Substitute.For(Of IInternalLauncherModel)()
    _fakeInternalModel.ShortName.Returns("testInternalModelShortName")
    _fakeInternalModel.LaunchAction.Returns(_fakeInternalLaunchAction)

    _fakeExternalLaunchAction = Substitute.For(Of ILaunchAction)()
    _fakeExternalLaunchAction.When(
      Sub(x) x.Launch()
    ).Do(
      Sub(x) _launchResult = LAUNCH_RESULT.EXTERNAL_ACTION_LAUNCHED
    )

    _fakeExternalModel = Substitute.For(Of IExternalLauncherModel)()
    _fakeExternalModel.ShortName.Returns("testExternalModelShortName")
    _fakeExternalModel.LaunchAction.Returns(_fakeExternalLaunchAction)

    _fakeErrorView = Substitute.For(Of ILaunchActionErrorView)()
    _fakeErrorView.When(
      Sub(x) x.Show()
    ).Do(
      Sub(x) _launchResult = LAUNCH_RESULT.ERROR_MESSAGE_SHOWN
    )

    _fakeSecurityView = Substitute.For(Of ILaunchActionSecurityErrorView)()
    _fakeSecurityView.When(
      Sub(x) x.Show()
    ).Do(
      Sub(x) _launchResult = LAUNCH_RESULT.SECURITY_MESSAGE_SHOWN
    )

  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub InternalConstructor_MissingParameter_ArgumentException1()
    _launcherPresenter = New LauncherPresenter(Nothing, _fakeInternalModel)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub InternalConstructor_MissingParameter_ArgumentException2()
    _launcherPresenter = New LauncherPresenter(_fakeLauncherView, Nothing)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub ExternalConstructor_MissingParameter_ArgumentException1()
    _launcherPresenter = New LauncherPresenter(Nothing, _fakeExternalModel, _fakeErrorView, _fakeSecurityView)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub ExternalConstructor_MissingParameter_ArgumentException2()
    _launcherPresenter = New LauncherPresenter(_fakeLauncherView, Nothing, _fakeErrorView, _fakeSecurityView)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub ExternalConstructor_MissingParameter_ArgumentException3()
    _launcherPresenter = New LauncherPresenter(_fakeLauncherView, _fakeExternalModel, Nothing, _fakeSecurityView)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub ExternalConstructor_MissingParameter_ArgumentException4()
    _launcherPresenter = New LauncherPresenter(_fakeLauncherView, _fakeExternalModel, _fakeErrorView, Nothing)
  End Sub

  <Test()>
  Public Sub LauncherFired_ValidExternalLauncherPresenter_ActionLaunched()

    ' Passing in a URL will see Process.Start() returning nothing without throwing an exception.

    _launcherPresenter = New LauncherPresenter(_fakeLauncherView, _fakeExternalModel, _fakeErrorView, _fakeSecurityView)

    ' See: http://programmaticallyspeaking.com/nsubstitute-vs-moq-a-quick-comparison.html
    AddHandler _fakeLauncherView.LauncherFired, Raise.Event(Of Action)()

    Assert.AreEqual(_launchResult, LAUNCH_RESULT.EXTERNAL_ACTION_LAUNCHED)

  End Sub

  <Test()>
  Public Sub LauncherFired_ValidInternalLauncherPresenter_ActionLaunched()

    ' Passing in a URL will see Process.Start() returning nothing without throwing an exception.

    _launcherPresenter = New LauncherPresenter(_fakeLauncherView, _fakeInternalModel)

    ' See: http://programmaticallyspeaking.com/nsubstitute-vs-moq-a-quick-comparison.html
    AddHandler _fakeLauncherView.LauncherFired, Raise.Event(Of Action)()

    Assert.AreEqual(_launchResult, LAUNCH_RESULT.INTERNAL_ACTION_LAUNCHED)

  End Sub

  <Test()>
  Public Sub LauncherFired_ValidExternalLauncherPresenter_SecurityMessageShown()

    Dim untouchableFilename = Guid.NewGuid().ToString
    Dim premissions = New FileIOPermission(FileIOPermissionAccess.NoAccess, "C:\" + untouchableFilename)

    _errorLaunchAction = New ExternalLaunchAction(
      untouchableFilename,
      FileUtilityCollection.GetTempPath
    )

    _fakeExternalModel.LaunchAction.Returns(_errorLaunchAction)

    premissions.PermitOnly()

    _launcherPresenter = New LauncherPresenter(_fakeLauncherView, _fakeExternalModel, _fakeErrorView, _fakeSecurityView)

    ' See: http://programmaticallyspeaking.com/nsubstitute-vs-moq-a-quick-comparison.html
    AddHandler _fakeLauncherView.LauncherFired, Raise.Event(Of Action)()

    CodeAccessPermission.RevertPermitOnly()

    Assert.AreEqual(_launchResult, LAUNCH_RESULT.SECURITY_MESSAGE_SHOWN)

  End Sub

  <Test()>
  Public Sub LauncherFired_ValidExternalLauncherPresenter_ErrorMessageShown()

    _errorLaunchAction = New ExternalLaunchAction(
      Guid.NewGuid().ToString,
      FileUtilityCollection.GetTempPath
    )

    _fakeExternalModel.LaunchAction.Returns(_errorLaunchAction)

    _launcherPresenter = New LauncherPresenter(_fakeLauncherView, _fakeExternalModel, _fakeErrorView, _fakeSecurityView)

    ' See: http://programmaticallyspeaking.com/nsubstitute-vs-moq-a-quick-comparison.html
    AddHandler _fakeLauncherView.LauncherFired, Raise.Event(Of Action)()

    Assert.AreEqual(_launchResult, LAUNCH_RESULT.ERROR_MESSAGE_SHOWN)
  End Sub

End Class

