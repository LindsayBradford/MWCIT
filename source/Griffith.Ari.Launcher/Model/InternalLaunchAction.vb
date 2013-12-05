''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports Griffith.Ari.Launcher.View

''' <summary>
''' A concrete implementation of ILaunchAction, designed to launch an action
''' as internal behaviour to the framework's runtime environment. 
''' </summary>

Public Class InternalLaunchAction
  Implements ILaunchAction

  Private _view As IInternalLaunchActionView

  Public Sub New(ByRef actionView As IInternalLaunchActionView)
    If actionView Is Nothing Then
      Throw New ArgumentException("Attempted to create InternalLaunchAction with no action view defined.")
    End If

    Me._view = actionView
  End Sub

  Public Sub Launch() Implements ILaunchAction.Launch
    _view.Show()
  End Sub
End Class
