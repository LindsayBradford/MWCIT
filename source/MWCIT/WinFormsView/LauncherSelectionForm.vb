''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Drawing
Imports System.Windows.Forms

Imports Griffith.Ari.Launcher.View

''' <summary>
''' A WinForms view of ILauncherSetView, implemented as a Windows Form. 
''' </summary>

Public NotInheritable Class LauncherSelectionForm
  Implements ILauncherSetView

  Private maxLauncherSize As New Size()
  Private launcherCount As Integer = 0

  ''' <summary>
  ''' Adds a new instance of ILauncherView to this windows form.  
  ''' </summary>
  ''' <param name="launcher"></param>
  ''' <remarks>
  ''' Assumes that the instance is also a WinForms Control, and that they are
  ''' of roughly similar size, allowing for simple layout via a FlowLayoutPanel.
  ''' </remarks>

  Private Sub AddLauncherView(ByRef launcher As ILauncherView) Implements ILauncherSetView.AddLauncherView
    Dim launcherAsButton = DirectCast(launcher, LauncherButton)

    AddHandler launcherAsButton.HoveringOverLauncher, AddressOf OnLauncherHover
    AddHandler launcherAsButton.LeavingLauncher, AddressOf OnLauncherLeave

    If Me.maxLauncherSize.Width < launcherAsButton.Size.Width Then
      Me.maxLauncherSize.Width = launcherAsButton.Size.Width
    End If
    If Me.maxLauncherSize.Height < launcherAsButton.Size.Height Then
      Me.maxLauncherSize.Height = launcherAsButton.Size.Height
    End If

    Me.DynamicControlsPanel.Controls.Add(launcherAsButton)

    Me.launcherCount = Me.launcherCount + 1

  End Sub

  Public Sub ShowLauncherSet() Implements ILauncherSetView.Show

    For Each launcherControl As Control In Me.DynamicControlsPanel.Controls
      launcherControl.MinimumSize = Me.maxLauncherSize
      launcherControl.MaximumSize = Me.maxLauncherSize
    Next

    Me.DynamicControlsPanel.MinimumSize = Me.maxLauncherSize

    Me.MinimumSize =
      New Size(
        Me.MinimumSize.Width,
        Me.DynamicControlsPanel.MinimumSize.Height + Me.ToolStripStatusLabel.Size.Height + 40
      )

    Me.Size = New Size(
      Me.PreferredSize.Width,
      Me.MinimumSize.Height
    )

    Application.Run(Me)
  End Sub

  Public Function Count() As Integer Implements ILauncherSetView.Count
    Return Me.launcherCount
  End Function

  Private Sub OnLauncherHover(ByRef description As String)
    Me.ToolStripStatusLabel.Text = description
  End Sub

  Private Sub OnLauncherLeave()
    Me.ToolStripStatusLabel.Text = ""
  End Sub
End Class