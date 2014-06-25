''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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
