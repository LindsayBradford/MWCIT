''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Drawing

''' <summary>
''' A default concrete implementation of both IExternalLauncherModel and IInternalLauncherModel.
''' </summary>
''' <remarks>
''' Both model interfaces have exactly the same run-time behaviour. Depending on which interface
''' an instance of this class is accessed from however, will dictate how to handle running the
''' LauncAction defined for it. 
''' </remarks>
''' 
Public Class LauncherModel
  Implements IExternalLauncherModel, IInternalLauncherModel

  Private _shortName As String = Nothing
  Private _description As String = Nothing
  Private _icon As Image = Nothing
  Private _launchAction As ILaunchAction = Nothing

  Public Property ShortName As String Implements ILauncherModel.ShortName
    Get
      Return _shortName
    End Get
    Set(value As String)
      Me._shortName = value
    End Set
  End Property

  Public Property Description As String Implements ILauncherModel.Description
    Get
      Return _description
    End Get
    Set(value As String)
      Me._description = value
    End Set
  End Property

  Public Property Icon As Image Implements ILauncherModel.Icon
    Get
      Return _icon
    End Get
    Set(value As Image)
      Me._icon = value
    End Set
  End Property

  Public Property LaunchAction As ILaunchAction Implements ILauncherModel.LaunchAction
    Get
      Return _launchAction
    End Get
    Set(value As ILaunchAction)
      Me._launchAction = value
    End Set
  End Property

End Class
