''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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

  Dim _actionViewMock As IInternalLaunchActionView

  Private Shared _viewAssembly = Assembly.GetExecutingAssembly
  Private Shared _viewConfig = New TestLauncherViewConfig

  <TestFixtureSetUp()>
  Public Sub TestFixtureSetup()
    _builder = New LauncherModelBuilder(_viewAssembly, _viewConfig)

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
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testInNothingAssemblyConstructor()
    Dim builder = New LauncherModelBuilder(Nothing, _viewConfig)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testInNothingConfigConstructor()
    Dim builder = New LauncherModelBuilder(_viewAssembly, Nothing)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testInNothingBuildExternalModel()
    _builder.BuildExternalModel(Nothing)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testInNothingKeyBuildInternalModel()
    _builder.BuildInternaModel(Nothing, _actionViewMock)
  End Sub

  <Test()>
  <ExpectedException(GetType(ArgumentException))>
  Public Sub testInNothingActionViewBuildInternalModel()
    _builder.BuildInternaModel("About", Nothing)
  End Sub

  <Test()>
  Public Sub testSuccessfulBuildInternalModel()
    Dim model = _builder.BuildInternaModel("About", _actionViewMock)

    Assert.AreEqual(
      LAUNCH_RESULT.UNTESTED,
      _launchResult
    )

    Assert.IsInstanceOf(Of InternalLaunchAction)(model.LaunchAction)

    model.LaunchAction.Launch()

    Assert.AreEqual(
      LAUNCH_RESULT.VIEW_SHOWN,
      _launchResult
    )

  End Sub

  <Test()>
  Public Sub testSuccessfulBuildExternalModel()
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
  Public Sub testUnsuccessfulBuildExternalModel()
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






