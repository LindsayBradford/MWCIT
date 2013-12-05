''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.IO
Imports System.Diagnostics

Imports Griffith.Ari.Launcher.View

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
        "Empty working directoey supplied to Constructor."
      )
    End If

    With Me
      _action = action
      _workingDirectory = workingDirectory
    End With
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

    If _action Is Nothing Then Return

    Directory.SetCurrentDirectory(_workingDirectory)
    Process.Start(_action)
  End Sub

  Private Function isValidAction() As Boolean
    ' An action with value of Nothing is interpreted as a valid 'do nothing'action
    If _action Is Nothing Then Return True

    If _action.Equals("") Then Return False

    ' Mimics .NET Framework 4.5 removal of support for URLs:
    ' http://msdn.microsoft.com/en-us/library/h6ak8zt5(v=vs.110).aspx

    If _action.ToLower().StartsWith("http") Then Return False

    Return True
  End Function

End Class
