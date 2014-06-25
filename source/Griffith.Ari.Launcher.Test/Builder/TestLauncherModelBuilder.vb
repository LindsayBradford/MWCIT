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

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

<TestFixture(), Category("Integration")>
Public Class TestLauncherModelBuilder

  Private Enum LAUNCH_RESULT
    UNTESTED = 0
    VIEW_SHOWN = 1
  End Enum

  Private _launchResult As LAUNCH_RESULT

  Dim _builder As ILauncherModelBuilder

  Dim _fakeActionView As IInternalLaunchActionView

  Private Shared _viewAssembly = Assembly.GetExecutingAssembly
  Private Shared _viewConfig = New TestLauncherViewConfig

  <TestFixtureSetUp()>
  Public Sub TestFixtureSetup()
    _builder = New LauncherModelBuilder(_viewAssembly, _viewConfig)

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
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub Constructor_MissingAssembly_ArgumentException()
    Dim builder = New LauncherModelBuilder(Nothing, _viewConfig)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub Constructor_MissingConfigFile_ArgumentException()
    Dim builder = New LauncherModelBuilder(_viewAssembly, Nothing)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub BuildExternalModel_MissingConfigFile_ArgumentException()
    _builder.BuildExternalModel(Nothing)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub BuildInternalModel_MissingLaunchKey_ArgumentException()
    _builder.BuildInternaModel(Nothing, _fakeActionView)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub BuildInternalModel_MissingLaunchActionView_ArgumentException()
    _builder.BuildInternaModel("About", Nothing)
  End Sub

  <Test()>
  Public Sub LaunchAction_ValidInternalModel_IsInternalLaunchAction()
    Dim model = _builder.BuildInternaModel("About", _fakeActionView)
    Assert.IsInstanceOf(Of InternalLaunchAction)(model.LaunchAction)
  End Sub

  <Test()>
  Public Sub Launch_InternalModel_ViewShown()
    Dim model = _builder.BuildInternaModel("About", _fakeActionView)

    model.LaunchAction.Launch()

    Assert.AreEqual(
      LAUNCH_RESULT.VIEW_SHOWN,
      _launchResult
    )
  End Sub

  <Test()>
  Public Sub LaunchAction_ValidExternalModel_IsExternalLaunchAction()
    Dim configFilePath =
    AssemblyUtiityCollection.UnpackResource(
      _viewAssembly,
      FileUtilityCollection.GetTempPath,
      "Valid_Null_Launcher.INI"
    )

    Dim model = _builder.BuildExternalModel(configFilePath)

    Assert.IsInstanceOf(Of ExternalLaunchAction)(model.LaunchAction)
  End Sub

  <Test()>
  Public Sub LaunchAction_InvalidExternalModel_IsInternalLaunchAction()
    Dim configFilePath =
    AssemblyUtiityCollection.UnpackResource(
      _viewAssembly,
      FileUtilityCollection.GetTempPath,
      "Really_Invalid_Launcher.INI"
    )

    Dim model = _builder.BuildExternalModel(configFilePath)

    Assert.IsInstanceOf(Of InternalLaunchAction)(model.LaunchAction)
  End Sub

End Class






