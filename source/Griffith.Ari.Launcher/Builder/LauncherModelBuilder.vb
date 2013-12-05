''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Drawing
Imports System.IO
Imports System.Reflection

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

''' <summary>
''' A default implementation of ILauncherModelBuilder.
''' </summary>
''' <remarks></remarks>

Public NotInheritable Class LauncherModelBuilder
  Implements ILauncherModelBuilder

  Private _viewAssembly As Assembly
  Private _viewConfig As ILauncherConfig

  Private _internalConfigBridge As InternalConfigBridge
  Private _externalConfigBridge As ExternalConfigBridge

  Public Sub New(ByRef viewAssembly As Assembly, ByRef viewConfig As ILauncherConfig)
    If viewAssembly Is Nothing Then
      Throw New ArgumentException(
        "Constructor passed Nothing as its viewAssembly."
      )
    End If

    If viewConfig Is Nothing Then
      Throw New ArgumentException(
        "Constructor passed Nothing as its viewConfig."
      )
    End If

    Me._viewAssembly = viewAssembly
    Me._viewConfig = viewConfig

    Dim internalConfigFile = AssemblyUtiityCollection.UnpackResource(
      _viewAssembly,
      FileUtilityCollection.GetTempPath(),
      _viewConfig.InternalConfigFile
    )

    Me._internalConfigBridge = New InternalConfigBridge(_viewAssembly, internalConfigFile)
    Me._externalConfigBridge = New ExternalConfigBridge(_viewAssembly, _viewConfig)
  End Sub

  Public Function BuildExternalModel(ByRef configFile As String) As IExternalLauncherModel Implements ILauncherModelBuilder.BuildExternalModel
    If configFile Is Nothing OrElse configFile.Equals("") Then
      Throw New ArgumentException(
        "Nothing passed as the value for configFile."
      )
    End If

    Me._externalConfigBridge.ConfigFile = configFile

    Dim launcher = New LauncherModel
    With launcher
      .ShortName = Me._externalConfigBridge.GetShortName()
      .Description = Me._externalConfigBridge.GetDescription()
      .Icon = Me._externalConfigBridge.GetIcon()
    End With

    Dim action = Me._externalConfigBridge.GetAction()
    If action Is Nothing Then
      Dim invalidLaunchActionView = BuildInstanceOfInterface(Of IMissingActionConfigView)(Me._viewAssembly)

      launcher.LaunchAction = New InternalLaunchAction(invalidLaunchActionView)
    Else
      Dim validLaunchAction = New ExternalLaunchAction(
        Me._externalConfigBridge.GetAction,
        Me._externalConfigBridge.GetWorkingDirectory()
      )

      launcher.LaunchAction = validLaunchAction
    End If

    Return launcher

  End Function

  Public Function BuildInternalModel(ByRef launchKey As String, ByRef launchActionView As IInternalLaunchActionView) As IInternalLauncherModel Implements ILauncherModelBuilder.BuildInternaModel

    If launchKey Is Nothing OrElse launchKey.Equals("") Then
      Throw New ArgumentException(
        "Nothing passed as the value for launchKey."
      )
    End If

    If launchActionView Is Nothing Then
      Throw New ArgumentException(
        "Nothing passed as the value for launchActionView."
      )
    End If

    Dim launcher = New LauncherModel
    With launcher
      .ShortName = Me._internalConfigBridge.GetShortName(launchKey)
      .Description = Me._internalConfigBridge.GetDescription(launchKey)
      .Icon = Me._internalConfigBridge.GetIcon(launchKey)
      .LaunchAction = New InternalLaunchAction(launchActionView)
    End With

    Return launcher

  End Function

End Class

