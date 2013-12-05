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
Public Class TestLauncherPresenter

  Private _viewMock As ILauncherView

  Private _internalModelMock As IInternalLauncherModel
  Private _externalModelMock As IExternalLauncherModel

  Private _internalLaunchActionMock As ILaunchAction
  Private _externalLaunchActionMock As ILaunchAction

  Private _errorLaunchAction As ExternalLaunchAction

  Private _errorViewMock As ILaunchActionErrorView
  Private _securityViewMock As ILaunchActionSecurityErrorView

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

    _viewMock = Substitute.For(Of ILauncherView)()
    _viewMock.ShortName.Returns("testViewShortName")

    _internalLaunchActionMock = Substitute.For(Of ILaunchAction)()
    _internalLaunchActionMock.When(
      Sub(x) x.Launch()
    ).Do(
      Sub(x) _launchResult = LAUNCH_RESULT.INTERNAL_ACTION_LAUNCHED
    )

    _internalModelMock = Substitute.For(Of IInternalLauncherModel)()
    _internalModelMock.ShortName.Returns("testInternalModelShortName")
    _internalModelMock.LaunchAction.Returns(_internalLaunchActionMock)

    _externalLaunchActionMock = Substitute.For(Of ILaunchAction)()
    _externalLaunchActionMock.When(
      Sub(x) x.Launch()
    ).Do(
      Sub(x) _launchResult = LAUNCH_RESULT.EXTERNAL_ACTION_LAUNCHED
    )

    _externalModelMock = Substitute.For(Of IExternalLauncherModel)()
    _externalModelMock.ShortName.Returns("testExternalModelShortName")
    _externalModelMock.LaunchAction.Returns(_externalLaunchActionMock)

    _errorViewMock = Substitute.For(Of ILaunchActionErrorView)()
    _errorViewMock.When(
      Sub(x) x.Show()
    ).Do(
      Sub(x) _launchResult = LAUNCH_RESULT.ERROR_MESSAGE_SHOWN
    )

    _securityViewMock = Substitute.For(Of ILaunchActionSecurityErrorView)()
    _securityViewMock.When(
      Sub(x) x.Show()
    ).Do(
      Sub(x) _launchResult = LAUNCH_RESULT.SECURITY_MESSAGE_SHOWN
    )

  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testNothingInternalLaunchViewConstructor()
    _launcherPresenter = New LauncherPresenter(Nothing, _internalModelMock)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testNothingInternalLaunchModelConstructor()
    _launcherPresenter = New LauncherPresenter(_viewMock, Nothing)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testNothingExternalLaunchViewConstructor()
    _launcherPresenter = New LauncherPresenter(Nothing, _externalModelMock, _errorViewMock, _securityViewMock)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testNothingExternalLaunchModelConstructor()
    _launcherPresenter = New LauncherPresenter(_viewMock, Nothing, _errorViewMock, _securityViewMock)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testNothingErrorViewConstructor()
    _launcherPresenter = New LauncherPresenter(_viewMock, _externalModelMock, Nothing, _securityViewMock)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testNothingSecurityErrorViewConstructor()
    _launcherPresenter = New LauncherPresenter(_viewMock, _externalModelMock, _errorViewMock, Nothing)
  End Sub

  <Test()>
  Public Sub testSuccessfulExternalLaunch()

    ' Passing in a URL will see Process.Start() returning nothing without throwing an exception.

    _launcherPresenter = New LauncherPresenter(_viewMock, _externalModelMock, _errorViewMock, _securityViewMock)

    ' See: http://programmaticallyspeaking.com/nsubstitute-vs-moq-a-quick-comparison.html
    AddHandler _viewMock.LauncherFired, Raise.Event(Of Action)()

    Assert.AreEqual(_launchResult, LAUNCH_RESULT.EXTERNAL_ACTION_LAUNCHED)

  End Sub

  <Test()>
  Public Sub testSuccessfulInternalLaunch()

    ' Passing in a URL will see Process.Start() returning nothing without throwing an exception.

    _launcherPresenter = New LauncherPresenter(_viewMock, _internalModelMock)

    ' See: http://programmaticallyspeaking.com/nsubstitute-vs-moq-a-quick-comparison.html
    AddHandler _viewMock.LauncherFired, Raise.Event(Of Action)()

    Assert.AreEqual(_launchResult, LAUNCH_RESULT.INTERNAL_ACTION_LAUNCHED)

  End Sub

  <Test()>
  Public Sub testSecurityBreachExternalLaunch()

    Dim untouchableFilename = Guid.NewGuid().ToString
    Dim premissions = New FileIOPermission(FileIOPermissionAccess.NoAccess, "C:\" + untouchableFilename)

    _errorLaunchAction = New ExternalLaunchAction(
      untouchableFilename,
      FileUtilityCollection.GetTempPath
    )

    _externalModelMock.LaunchAction.Returns(_errorLaunchAction)

    premissions.PermitOnly()

    _launcherPresenter = New LauncherPresenter(_viewMock, _externalModelMock, _errorViewMock, _securityViewMock)

    ' See: http://programmaticallyspeaking.com/nsubstitute-vs-moq-a-quick-comparison.html
    AddHandler _viewMock.LauncherFired, Raise.Event(Of Action)()

    CodeAccessPermission.RevertPermitOnly()

    Assert.AreEqual(_launchResult, LAUNCH_RESULT.SECURITY_MESSAGE_SHOWN)

  End Sub

  <Test()>
  Public Sub testErrorExternalLaunch()

    _errorLaunchAction = New ExternalLaunchAction(
      Guid.NewGuid().ToString,
      FileUtilityCollection.GetTempPath
    )

    _externalModelMock.LaunchAction.Returns(_errorLaunchAction)

    _launcherPresenter = New LauncherPresenter(_viewMock, _externalModelMock, _errorViewMock, _securityViewMock)

    ' See: http://programmaticallyspeaking.com/nsubstitute-vs-moq-a-quick-comparison.html
    AddHandler _viewMock.LauncherFired, Raise.Event(Of Action)()

    Assert.AreEqual(_launchResult, LAUNCH_RESULT.ERROR_MESSAGE_SHOWN)
  End Sub

End Class

