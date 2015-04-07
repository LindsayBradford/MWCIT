''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Security.Permissions

''' <summary>
''' A collection of utility methods for INI file manipulation.
''' </summary>
''' <remarks>
''' Based on code here: http://www.vbdotnetforums.com/vb-net-general-discussion/45603-read-write-ini-file.html
''' </remarks>

Public Module INIFileCollection

  Private Function GetReadPermission(ByVal filePath As String) As PermissionSet
    Dim thisSet = New PermissionSet(Security.Permissions.PermissionState.None)
    thisSet.AddPermission(
      New FileIOPermission(FileIOPermissionAccess.Read, filePath)
    )

    Return thisSet
  End Function

  Private Function GetWritePermission(ByVal filePath As String) As PermissionSet
    Dim thisSet = New PermissionSet(Security.Permissions.PermissionState.None)
    thisSet.AddPermission(
      New FileIOPermission(FileIOPermissionAccess.Write, filePath)
    )

    Return thisSet
  End Function

  ''' <summary>
  ''' Reads the sections present in the specified INI file and returns them as an array of Strings.
  ''' </summary>
  ''' <param name="iniFilePath"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  ''' 

  Public Function ReadSections(ByVal iniFilePath As String) As String()

    GetReadPermission(iniFilePath).Demand()

    Dim MAX_BUFFER As UInteger = 32767
    Dim pReturnedString As IntPtr = Marshal.AllocCoTaskMem(CInt(MAX_BUFFER))

    Dim i = NativeMethods.GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, iniFilePath)

    Dim local As String = Marshal.PtrToStringUni(pReturnedString, CInt(i)).ToString()

    If local.Length = 0 Then Return Nothing

    Marshal.FreeCoTaskMem(pReturnedString)
    'use of Substring below removes terminating null for split
    Return local.Substring(0, local.Length - 1).Split(ControlChars.NullChar)
  End Function


  ''' <summary>
  ''' Read value from INI file
  ''' </summary>
  ''' <param name="section">The section of the file to look in</param>
  ''' <param name="key">The key in the section to look for</param>
  ''' 
  Public Function ReadValue(ByVal Path As String, ByVal section As String, ByVal key As String) As String
    GetReadPermission(Path).Demand()

    Dim sb As New System.Text.StringBuilder(255)
    Dim i = NativeMethods.GetPrivateProfileString(section, key, "", sb, 255, Path)
    Return sb.ToString()
  End Function


  ''' <summary>
  ''' Write value to INI file
  ''' </summary>
  ''' <param name="iniFilePath">The INI file to write to</param>
  ''' <param name="iniFileSection">The section of the INI file to write to</param>
  ''' <param name="key">The key to write to the INI file in the given section</param>
  ''' <param name="value">The value to write to the INI file against the given key</param>
  ''' 
  Public Sub WriteValue(ByVal iniFilePath As String, ByVal iniFileSection As String, ByVal key As String, ByVal value As String)
    GetWritePermission(iniFilePath).Demand()

    NativeMethods.WritePrivateProfileString(iniFileSection, key, value, iniFilePath)
  End Sub

  ''' <summary>
  ''' Convenience method that retuns the value stored against the specified key, which is expected to appear in the first section of the specified INI file. 
  ''' </summary>
  ''' <param name="iniFilePath"></param>
  ''' <param name="key"></param>
  ''' <returns></returns>
  ''' <remarks>Assumes the key is present in the first section. Doesn't care about the name of the section.</remarks>

  Public Function ReadValueOfFirstSection(ByVal iniFilePath As String, ByVal key As String) As String
    GetReadPermission(iniFilePath).Demand()

    Dim sections = ReadSections(iniFilePath)
    If sections Is Nothing Then Return Nothing
    Return ReadValue(iniFilePath, sections(0), key)
  End Function

  Private Class NativeMethods

    Friend Declare Unicode Function GetPrivateProfileSectionNames Lib "kernel32.dll" Alias "GetPrivateProfileSectionNamesW" _
      (ByVal lpReturnedString As IntPtr, _
       ByVal nSize As Integer, _
       ByVal lpFileName As String) _
    As Integer

    Friend Declare Unicode Function GetPrivateProfileString Lib "kernel32.dll" Alias "GetPrivateProfileStringW" _
      (ByVal lpApplicationName As String, _
       ByVal lpKeyName As String, _
       ByVal lpDefault As String, _
       ByVal lpReturnedString As System.Text.StringBuilder, _
       ByVal nSize As Integer, _
       ByVal lpFileName As String) _
    As Integer

    Friend Declare Unicode Function WritePrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringW" _
      (ByVal lpApplicationName As String, _
       ByVal lpKeyName As String, _
       ByVal lpString As String, _
       ByVal lpFileName As String) _
    As Integer

  End Class
End Module
