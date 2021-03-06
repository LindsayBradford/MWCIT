﻿''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.IO
Imports System.Diagnostics

Imports Griffith.Ari.Launcher.View
Imports System.Security
Imports System.Security.Permissions

''' <summary>
''' A concrete implementation of ILaunchAction, designed to launch an action
''' as an external process to the framework's runtime environment. 
''' </summary>

Public Class ExternalLaunchAction
  Implements ILaunchAction

  Private _action As String
  Private _workingDirectory As String

  ''' <summary>
  ''' Creates a new action to be run as an external prrocess, using workingDirectory as the 
  ''' current directory when the process starts.
  ''' </summary>

  Public Sub New(ByVal action As String, ByVal workingDirectory As String)
    If workingDirectory Is Nothing OrElse workingDirectory.Equals("") Then
      Throw New ArgumentException(
        "Empty working directory supplied to Constructor."
      )
    End If

    With Me
      _action = action
      _workingDirectory = workingDirectory
    End With

    If Not isValidAction() Then
      Throw New ArgumentException(
        String.Format("Invalid Action '{0}' supplied to constructor.", _action)
      )
    End If

  End Sub

  ''' <summary>
  ''' Launches an external process as specified on the constructor.
  ''' </summary>

  Public Sub Launch() Implements ILaunchAction.Launch
    If Not isValidAction() Then
      Throw New ArgumentException(
        String.Format("Action '{0}' will result in no action being taken.", _action)
      )
    End If

    GetExecutePermission(_action).Demand()
    Directory.SetCurrentDirectory(_workingDirectory)
    Process.Start(_action)
  End Sub

  Private Function GetExecutePermission(ByVal filePath As String) As PermissionSet
    Dim thisSet = New PermissionSet(Security.Permissions.PermissionState.None)
    thisSet.AddPermission(
      New SecurityPermission(SecurityPermissionFlag.Execution)
    )

    Return thisSet
  End Function


  Private Function isValidAction() As Boolean
    ' An action with value of Nothing is interpreted as a valid 'do nothing'action
    If _action Is Nothing OrElse _action.Equals("") Then Return False

    ' Mimics .NET Framework 4.5 removal of support for URLs:
    ' http://msdn.microsoft.com/en-us/library/h6ak8zt5(v=vs.110).aspx

    If _action.ToLower().StartsWith("http") Then Return False

    Return True
  End Function

End Class
