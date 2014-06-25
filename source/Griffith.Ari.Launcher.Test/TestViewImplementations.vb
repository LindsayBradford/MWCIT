''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Drawing
Imports System.IO
Imports System.Security.Cryptography

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

Module TestViewImplementations

  Public NotInheritable Class TestLauncherViewConfig
    Implements ILauncherConfig

    Private Const INTERNAL_CONFIG_FILE = "View.INI"
    Private Const EXTERNAL_CONFIG_FILES = "Launcher.INI"

    Private Const DEFAULT_SHORT_NAME = "TestShortName"
    Private Const DEFAULT_DESCRIPTION = "TestDescription"

    Private Const DEFAULT_ICON_PATH = "Alert.png"
    Private Shared DEFAULT_ICON As Image = AssemblyUtiityCollection.RetrieveEmbeddedImage(DEFAULT_ICON_PATH)

    Public ReadOnly Property InternalConfigFile As String Implements Griffith.Ari.Launcher.View.ILauncherConfig.InternalConfigFile
      Get
        Return INTERNAL_CONFIG_FILE
      End Get
    End Property

    Public ReadOnly Property ExternalConfigFiles As String Implements Griffith.Ari.Launcher.View.ILauncherConfig.ExternaConfigFiles
      Get
        Return EXTERNAL_CONFIG_FILES
      End Get
    End Property

    Public ReadOnly Property DefaultDescription As String Implements Griffith.Ari.Launcher.View.ILauncherConfig.DefaultDescription
      Get
        Return DEFAULT_DESCRIPTION
      End Get
    End Property

    Public ReadOnly Property DefaultIcon As Image Implements Griffith.Ari.Launcher.View.ILauncherConfig.DefaultIcon
      Get
        Return DEFAULT_ICON
      End Get
    End Property

    Public ReadOnly Property DefaultShortName As String Implements Griffith.Ari.Launcher.View.ILauncherConfig.DefaultShortName
      Get
        Return DEFAULT_SHORT_NAME
      End Get
    End Property
  End Class

  Public Class TestLauncherSet
    Implements ILauncherSetView

    Private launcherCount As Integer = 0

    Public Sub AddLauncher(ByRef launcher As ILauncherView) Implements ILauncherSetView.AddLauncherView
      launcherCount = launcherCount + 1
    End Sub

    Public Sub Show() Implements ILauncherSetView.Show

    End Sub

    Public Function Count() As Integer Implements ILauncherSetView.Count
      Return launcherCount
    End Function
  End Class

  Public Class TestLaunchView
    Implements ILauncherView

    Public Property Description As String Implements View.ILauncherView.Description
      Get
        Return "test internal description"
      End Get
      Set(value As String)

      End Set
    End Property

    Public Event LauncherFired() Implements View.ILauncherView.LauncherFired

    Public Property ShortName As String Implements View.ILauncherView.ShortName
      Get
        Return "TestShortName"
      End Get
      Set(value As String)

      End Set
    End Property

    Public Property Icon As Image Implements ILauncherView.Icon
      Get
        Return Nothing
      End Get
      Set(value As Image)

      End Set
    End Property
  End Class

  Public Class TestInternalLaunchActionView
    Implements IInternalLaunchActionView

    Public Sub Show() Implements View.IInternalLaunchActionView.Show

    End Sub
  End Class

  Public Class TestExternalLaunchActionView
    Implements IInternalLaunchActionView

    Public Sub Show() Implements View.IInternalLaunchActionView.Show

    End Sub
  End Class

  Public Class TestLaunchActionErrorView
    Implements ILaunchActionErrorView

    Public Sub Show() Implements View.ILaunchActionErrorView.Show

    End Sub
  End Class

  Public Class TestLaunchActionSecurityErrorView
    Implements ILaunchActionSecurityErrorView

    Public Sub Show() Implements View.ILaunchActionSecurityErrorView.Show

    End Sub
  End Class

  Public Class TestMissingActionConfigView
    Implements IMissingActionConfigView

    Public Sub Show() Implements View.IMissingActionConfigView.Show

    End Sub
  End Class

  Public Class TestMAboutLaunchActionView
    Implements IAboutlLaunchActionView

    Public Sub Show() Implements View.IAboutlLaunchActionView.Show

    End Sub
  End Class

  Public Function GetHashForIcon(ByRef icon As System.Drawing.Image) As Byte()
    Dim mySHA256 As SHA256 = SHA256Managed.Create()

    Dim iconStream = New MemoryStream()
    icon.Save(iconStream, System.Drawing.Imaging.ImageFormat.Png)

    Return mySHA256.ComputeHash(iconStream)
  End Function

End Module
