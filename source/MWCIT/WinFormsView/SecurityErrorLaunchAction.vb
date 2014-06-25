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

Public Class SecurityErrorLaunchAction
  Implements ILaunchActionSecurityErrorView

  Public Sub Show() Implements Griffith.Ari.Launcher.View.ILaunchActionSecurityErrorView.Show
    MessageBox.Show(
      "You do not have security permissions to run the action defined for this launcher. " +
      "Please seek security permission for this action and try again.",
      "Insufficient Security Permissions",
      MessageBoxButtons.OK,
      MessageBoxIcon.Error
    )
  End Sub

End Class
