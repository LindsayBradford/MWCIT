''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Windows.Forms

Imports Griffith.Ari.Launcher.View

Public Class MissingActionConfigView
  Implements IMissingActionConfigView

  Public Sub Show() Implements IMissingActionConfigView.Show
    MessageBox.Show(
      "No action has been defined for this launcher. Please fix the launcher configuration and try again.",
      "Launch Failure",
      MessageBoxButtons.OK,
      MessageBoxIcon.Warning
    )
  End Sub
End Class
