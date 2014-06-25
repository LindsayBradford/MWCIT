''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.IO

''' <summary>
''' A collecion of methods to make file handling uniform across the application.
''' </summary>
''' <remarks></remarks>

Public Module FileUtilityCollection

  ''' <summary>
  ''' Returns true if the filename represents a file that can be loaded.
  ''' </summary>
  ''' <param name="filename"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>

  Public Function CanLoadFile(ByRef filename As String) As Boolean

    If filename Is Nothing Then Return False
    If filename.Length() = 0 Then Return False

    If Not File.Exists(filename) Then Return False

    Return True
  End Function

  ''' <summary>
  ''' Returns true if the filename represents a file that can be saved.
  ''' </summary>
  ''' <param name="filename"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>

  Public Function CanSaveFile(ByRef filename As String) As Boolean
    Try
      Dim fileAttribs As FileAttributes = File.GetAttributes(filename)
      Return Not ((fileAttribs And FileAttributes.ReadOnly) = FileAttributes.ReadOnly)
    Catch fnfEx As FileNotFoundException
      Return True
    Catch fnfEx As DirectoryNotFoundException
      Return True
    Catch ex As Exception
      Return False
    End Try
  End Function

  ''' <summary>
  ''' Return a standardised (all lowercase) file extension for the supplied filename
  ''' </summary>
  ''' <param name="filename"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>

  Public Function GetFileExtension(ByRef filename As String) As String
    Return Path.GetExtension(filename).ToLower()
  End Function

  ''' <summary>
  ''' Returns a string encoding of the path to the environment's temporary directory. 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>

  Public Function GetTempPath() As String
    Return Path.GetTempPath()
  End Function

  ''' <summary>
  ''' Copies one the input stream to the output stream in 32K blocks 
  ''' </summary>

  Public Sub CopyStream(ByRef input As Stream, ByRef output As Stream)
    Dim streamBuffer(32768) As Byte
    While (True)
      Dim read As Integer = input.Read(streamBuffer, 0, streamBuffer.Length)
      If (read <= 0) Then Return
      output.Write(streamBuffer, 0, read)
    End While
  End Sub

  ''' <summary>
  ''' Saves the raw byte data in byteArray directly to filepath
  ''' </summary>
  ''' <remarks>Assumes the file at filepath can be written to.</remarks>

  Public Sub CreateFileFromByteArray(ByRef byteArray As Byte(), ByRef filepath As String)
    Using fileStream As New FileStream(filepath, FileMode.Create)
      fileStream.Write(byteArray, 0, byteArray.Length)
    End Using
  End Sub

  ''' <summary>
  ''' Deletes the file at filepath
  ''' </summary>
  ''' <remarks>Assumes the file at filepath can be deleted.</remarks>

  Public Sub DeleteFile(ByRef filePath As String)
    File.Delete(filePath)
  End Sub

  ''' <summary>
  ''' Returns an array of strings containing all the subdirectories of specified parentDirectory.
  ''' </summary>
  ''' <remarks>Returns only those directories the user has permission to see.</remarks>

  Public Function GetSubDirectoriesOf(ByVal parentDirectory As String) As String()
    Dim subDirectories As String() = Nothing
    Try
      subDirectories = Directory.GetDirectories(parentDirectory)
    Catch ex As Exception
      Return Nothing
    End Try

    Return subDirectories
  End Function

  ''' <summary>
  ''' Convenience method. If thisDirectory contains thisFile, the method returns the full path through
  ''' to the file. If thisDirectory does not contain thisFile, the method returns Nothing.
  ''' </summary>

  Public Function DirectoryContainingFile(ByVal thisDirectory As String, ByVal thisFile As String) As String
    For Each file In Directory.GetFiles(thisDirectory)
      If file.EndsWith(thisFile) Then Return file
    Next ' file
    Return Nothing
  End Function

  ''' <summary>
  ''' Assumes the file path specified by thisFile is relative to the directory thisPath, and constructs 
  ''' a complete path through to the file relative to the directory. 
  ''' </summary>
  ''' <param name="thisPath"></param>
  ''' <param name="thisFile"></param>
  ''' <returns></returns>
  ''' <remarks>
  ''' Example: We know a file "../../trashIcon.png" is relative to a directory "c:/tmp/test/icons".
  ''' This method returns "c:/tmp/test/Icons/../../trashIcon.png", allowing us to resolve the 
  ''' desired file from directory it is relative to. 
  ''' </remarks>

  Public Function GetFileRelativeToPath(ByVal thisPath As String, ByVal thisFile As String) As String
    Dim basePath As String = Path.GetDirectoryName(thisPath)
    Return Path.Combine(basePath, thisFile)
  End Function

End Module
