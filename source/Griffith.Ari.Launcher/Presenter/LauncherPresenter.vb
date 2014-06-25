''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports Griffith.Ari.Launcher.View
Imports Griffith.Ari.General

''' <summary>
''' A default concrete implementation of ILauncherPresenter, which binds ILauncherView behaviour to an IExternalLauncherModel or IInternalLauncherModel.
''' </summary>

Public Class LauncherPresenter
  Implements ILauncherPresenter

  Private _action As ILaunchAction = Nothing

  Private _errorView As ILaunchActionErrorView = Nothing
  Private _securityErrorView As ILaunchActionSecurityErrorView = Nothing

  Public Sub New(ByRef launchView As ILauncherView, ByVal launcher As IExternalLauncherModel,
                 ByVal errorView As ILaunchActionErrorView, ByVal securityErrorView As ILaunchActionSecurityErrorView)

    If errorView Is Nothing Then
      Throw New ArgumentException(
        "Attempted to supply Nothing as the errorView."
      )
    End If

    If securityErrorView Is Nothing Then
      Throw New ArgumentException(
        "Attempted to supply Nothing as the securityErrorView."
      )
    End If

    With Me
      _errorView = errorView
      _securityErrorView = securityErrorView
    End With
    Bind(launchView, launcher)
  End Sub

  Public Sub New(ByRef launchView As ILauncherView, ByVal launcher As IInternalLauncherModel)
    Bind(launchView, launcher)
  End Sub

  Public Sub Bind(ByRef view As ILauncherView, ByRef externalLauncher As IExternalLauncherModel) Implements ILauncherPresenter.Bind
    If view Is Nothing Then
      Throw New ArgumentException(
        "Attempted to bind Nothing as an ILauncherView."
      )
    End If

    If externalLauncher Is Nothing Then
      Throw New ArgumentException(
        "Attempted to bind Nothing as an IExternalLauncherModel."
      )
    End If

    UpdateView(view, externalLauncher)

    _action = externalLauncher.LaunchAction

    AddHandler view.LauncherFired, AddressOf ExternalLauncherFired
  End Sub

  Public Sub Bind(ByRef view As ILauncherView, ByRef internalLauncher As IInternalLauncherModel) Implements ILauncherPresenter.Bind
    If view Is Nothing Then
      Throw New ArgumentException(
        "Attempted to bind Nothing as an ILauncherView."
      )
    End If

    If internalLauncher Is Nothing Then
      Throw New ArgumentException(
        "Attempted to bind Nothing as an IInternalLauncherModel."
      )
    End If

    UpdateView(view, internalLauncher)

    _action = internalLauncher.LaunchAction

    AddHandler view.LauncherFired, AddressOf InternalLauncherFired
  End Sub

  Private Sub UpdateView(ByRef view As ILauncherView, ByRef model As ILauncherModel)
    With view
      .ShortName = model.ShortName
      .Description = model.Description
      .Icon = model.Icon
    End With
  End Sub

  Private Sub ExternalLauncherFired()
    Try
      _action.Launch()
    Catch sex As System.Security.SecurityException
      _securityErrorView.Show()
    Catch ex As Exception
      _errorView.Show()
    End Try
  End Sub

  Public Sub InternalLauncherFired()
    _action.Launch()
  End Sub

End Class
