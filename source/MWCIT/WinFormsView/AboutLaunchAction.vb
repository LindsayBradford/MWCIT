''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Windows.Forms

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

Public Class AboutLaunchAction
  Implements IAboutlLaunchActionView

  Private Shared PRODUCT_DELIMITER() As Char = New Char() {"."c}

  Public Sub Show() Implements Griffith.Ari.Launcher.View.IInternalLaunchActionView.Show
    Dim sha256Key = AssemblyUtiityCollection.RetrieveEntryAssemblySHA256AsString()

    MessageBox.Show(
      "Copyright in the software that comprises the " + getAppName() + " is the property of Australian Rivers Institute, " +
      "Griffith University, Australia (ARI). " +
       Environment.NewLine + Environment.NewLine +
      "The Crown in right of the Australian Government and the State of New South Wales is granted an " +
      "irrevocable, perpetual and non-exclusive licence to use this tool and any data this tool depends " +
       "upon for non-commercial purposes." +
      Environment.NewLine + Environment.NewLine +
      "This software may not be used, copied or reproduced in whole or part for any purpose other than that for " +
      "which it was supplied. " +
       Environment.NewLine + Environment.NewLine +
       "ARI makes no representation, undertakes no duty and accepts no responsibility " +
       "to any third party outside the Murrumbidgee Catchment Management Authority who may use or rely upon this software." +
        Environment.NewLine + Environment.NewLine + "SHA-256 Verification Key: " +
        Environment.NewLine + Environment.NewLine + sha256Key,
      "About " + getAppName() + " " + getVersionString(),
    MessageBoxButtons.OK,
    MessageBoxIcon.Information
    )
  End Sub

  Private Function getAppName() As String
    Return Application.ProductName.Split(PRODUCT_DELIMITER)(0)
  End Function

  Private Function getVersionString() As String
    Dim versionData As String()
    versionData = Application.ProductVersion.Split(PRODUCT_DELIMITER)
    Return String.Format(
      "{0}.{1}.{2}",
      versionData(0), versionData(1), versionData(2)
    )
  End Function

End Class
