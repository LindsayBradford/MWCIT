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
''' The default implementation of the ILauncherSetBuilder interface, responsible for creating a set of launchers
''' and rendering them as per the supplied viewAsssembly and launcherDirectory. 
''' </summary>

Public NotInheritable Class LauncherSetBuilder
  Implements ILauncherSetBuilder

  Private _viewAssembly As Assembly
  Private _viewConfig As ILauncherConfig

  Dim _defaultErrorActionView As ILaunchActionErrorView
  Dim _securityErrorActionView As ILaunchActionSecurityErrorView

  Public Function BuildFrom(ByRef viewAssembly As Assembly, ByVal launcherDirectory As String) As ILauncherSetView Implements ILauncherSetBuilder.BuildFrom

    Me._viewAssembly = viewAssembly
    Me._viewConfig = AssemblyUtiityCollection.BuildInstanceOfInterface(Of ILauncherConfig)(Me._viewAssembly)

    Me._defaultErrorActionView = BuildView(Of ILaunchActionErrorView)()
    Me._securityErrorActionView = BuildView(Of ILaunchActionSecurityErrorView)()

    Dim modelBuilder = New LauncherModelBuilder(_viewAssembly, _viewConfig)

    Dim itemSelector = BuildView(Of ILauncherSetView)()

    For Each launcherConfigFile In ResolveLauncherConfigFiles(launcherDirectory)

      Dim launcherView = BuildView(Of ILauncherView)()

      Dim externalPresenter = New LauncherPresenter(
        launcherView,
        modelBuilder.BuildExternalModel(
          launcherConfigFile
        ),
        _defaultErrorActionView,
        _securityErrorActionView
      )

      itemSelector.AddLauncherView(launcherView)
    Next ' launcherConfigFile

    Dim aboutLauncherView = BuildView(Of ILauncherView)()

    Dim internalPresenter = New LauncherPresenter(
        aboutLauncherView,
        modelBuilder.BuildInternalModel(
          InternalLauncherKeys.SHOW_ABOUT_INFO,
          BuildView(Of IAboutlLaunchActionView)()
        )
    )

    itemSelector.AddLauncherView(
      aboutLauncherView
    )

    Return itemSelector
  End Function

  Private Function ResolveLauncherConfigFiles(ByVal launcherDirectory As String) As List(Of String)

    Dim launcherConfigFiles As New List(Of String)

    If Not Directory.Exists(launcherDirectory) Then Return launcherConfigFiles

    Dim subDirectories As String() = FileUtilityCollection.GetSubDirectoriesOf(launcherDirectory)

    If subDirectories Is Nothing Then Return launcherConfigFiles

    For Each subDirectory As String In subDirectories
      Dim file = FileUtilityCollection.DirectoryContainingFile(subDirectory, _viewConfig.ExternaConfigFiles)
      If file IsNot Nothing Then
        launcherConfigFiles.Add(file)
      End If
    Next ' each subDirectory

    Return launcherConfigFiles
  End Function

  Private Function BuildView(Of T)() As T
    Return BuildInstanceOfInterface(Of T)(_viewAssembly)
  End Function

End Class
