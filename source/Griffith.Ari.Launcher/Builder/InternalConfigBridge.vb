''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Reflection

Imports Griffith.Ari.General

''' <summary>
''' A helper class to LauncherModelBuilder that acts as a bridge through to a
''' config file specifying launcher detail embedded within embeddingAssembly. 
''' </summary>

Public Class InternalConfigBridge
  Private _embeddingAssembly As Assembly
  Private _configFile As String

  Public Sub New(ByRef embeddingAssembly As Assembly, ByVal configFile As String)
    Me._embeddingAssembly = embeddingAssembly
    Me._configFile = configFile
  End Sub

  ''' <summary>
  ''' Returns the ShortName value spsecified for section of the internal config file.
  ''' </summary>
  ''' <param name="section"></param>

  Public Function GetShortName(ByVal section As String) As String
    Return GetValue(section, Config.SHORT_NAME)
  End Function

  ''' <summary>
  ''' Returns the Description value spsecified for section of the internal config file.
  ''' </summary>
  ''' <param name="section"></param>

  Public Function GetDescription(ByVal section As String) As String
    Return GetValue(section, Config.DESCRIPTION)
  End Function

  ''' <summary>
  ''' Returns the Icon image spsecified for section of the internal config file.
  ''' </summary>
  ''' <param name="section"></param>
  ''' <remarks>
  ''' Resolves the icon image off its path value in the given section, and
  ''' converts it to an image. 
  ''' </remarks>

  Public Function GetIcon(section As String) As System.Drawing.Image
    Return AssemblyUtiityCollection.RetrieveEmbeddedImage(
        _embeddingAssembly,
        GetValue(section, Config.ICON)
     )
  End Function

  Private Function GetValue(ByVal section As String, ByVal key As String) As String
    Return INIFileCollection.ReadValue(_configFile, section, key)
  End Function
End Class
