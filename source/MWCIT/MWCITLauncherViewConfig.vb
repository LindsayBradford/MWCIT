''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Drawing

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

''' <summary>
''' A concrete implementation of ILauncherConfig, specifiying defaults and configuration information
''' for this assembly, as it acts to configure the behaviour of Griffith.Ari.Launcher factories. 
''' </summary>
''' <remarks></remarks>

Public NotInheritable Class MWCITLauncherViewConfig
  Implements ILauncherConfig

  Private Const INTERNAL_CONFIG_FILE = "View.INI"
  Private Const EXTERNAL_CONFIG_FILES = "Launcher.INI"

  Private Const DEFAULT_SHORT_NAME = "Unspecified"
  Private Const DEFAULT_DESCRIPTION = "This launcher's configuration has no description specified"

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
