''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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
