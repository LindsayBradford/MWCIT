''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Windows.Forms

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

Public Class ErrorLaunchAction
  Implements ILaunchActionErrorView

  Public Sub Show() Implements Griffith.Ari.Launcher.View.ILaunchActionErrorView.Show
    MessageBox.Show(
      "The action defined for this launcher contains errors. Please fix the launcher configuration and try again.",
      "Launch Failure",
      MessageBoxButtons.OK,
      MessageBoxIcon.Error
    )
  End Sub

End Class
