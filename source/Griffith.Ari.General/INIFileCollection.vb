''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Runtime.InteropServices

''' <summary>
''' A collection of utility methods for INI file manipulation.
''' </summary>
''' <remarks>
''' Based on code here: http://www.vbdotnetforums.com/vb-net-general-discussion/45603-read-write-ini-file.html
''' </remarks>

Public Module INIFileCollection

  Private Declare Ansi Function GetPrivateProfileSectionNames Lib "kernel32.dll" Alias "GetPrivateProfileSectionNamesA" _
    (ByVal lpReturnedString As IntPtr, _
     ByVal nSize As Integer, _
     ByVal lpFileName As String) _
  As Integer

  ''' <summary>
  ''' Reads the sections present in the specified INI file and returns them as an array of Strings.
  ''' </summary>
  ''' <param name="iniFilePath"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>

  Public Function ReadSections(ByVal iniFilePath As String) As String()
    Dim MAX_BUFFER As UInteger = 32767
    Dim pReturnedString As IntPtr = Marshal.AllocCoTaskMem(CInt(MAX_BUFFER))

    Dim i = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, iniFilePath)


    Dim local As String = Marshal.PtrToStringAnsi(pReturnedString, CInt(i)).ToString()

    If local.Length = 0 Then Return Nothing

    Marshal.FreeCoTaskMem(pReturnedString)
    'use of Substring below removes terminating null for split
    Return local.Substring(0, local.Length - 1).Split(ControlChars.NullChar)
  End Function

  Private Declare Ansi Function GetPrivateProfileString Lib "kernel32.dll" Alias "GetPrivateProfileStringA" _
  (ByVal lpApplicationName As String, _
   ByVal lpKeyName As String, _
   ByVal lpDefault As String, _
   ByVal lpReturnedString As System.Text.StringBuilder, _
   ByVal nSize As Integer, _
   ByVal lpFileName As String) _
As Integer

  ''' <summary>
  ''' Read value from INI file
  ''' </summary>
  ''' <param name="section">The section of the file to look in</param>
  ''' <param name="key">The key in the section to look for</param>
  ''' 
  Public Function ReadValue(ByVal Path As String, ByVal section As String, ByVal key As String) As String
    Dim sb As New System.Text.StringBuilder(255)
    Dim i = GetPrivateProfileString(section, key, "", sb, 255, Path)
    Return sb.ToString()
  End Function

  Private Declare Ansi Function WritePrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringA" _
    (ByVal lpApplicationName As String, _
     ByVal lpKeyName As String, _
     ByVal lpString As String, _
     ByVal lpFileName As String) _
  As Integer

  ''' <summary>
  ''' Write value to INI file
  ''' </summary>
  ''' <param name="iniFilePath">The INI file to write to</param>
  ''' <param name="iniFileSection">The section of the INI file to write to</param>
  ''' <param name="key">The key to write to the INI file in the given section</param>
  ''' <param name="value">The value to write to the INI file against the given key</param>
  ''' 
  Public Sub WriteValue(ByVal iniFilePath As String, ByVal iniFileSection As String, ByVal key As String, ByVal value As String)
    WritePrivateProfileString(iniFileSection, key, value, iniFilePath)
  End Sub

  ''' <summary>
  ''' Convenience method that retuns the value stored against the specified key, which is expected to appear in the first section of the specified INI file. 
  ''' </summary>
  ''' <param name="iniFilePath"></param>
  ''' <param name="key"></param>
  ''' <returns></returns>
  ''' <remarks>Assumes the key is present in the first section. Doesn't care about the name of the section.</remarks>

  Public Function ReadValueOfFirstSection(ByVal iniFilePath As String, ByVal key As String) As String
    Dim sections = ReadSections(iniFilePath)
    If sections Is Nothing Then Return Nothing
    Return ReadValue(iniFilePath, sections(0), key)
  End Function

End Module
