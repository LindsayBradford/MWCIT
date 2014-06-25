''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Text
Imports System.Text.RegularExpressions

''' <summary>
''' A collection of utility methods for string manipulation.
''' </summary>
''' <remarks></remarks>

Public Module StringUtilityCollection

  ''' <summary>
  ''' Creates a timestamp string of the current time. Formatted as "yyyMMddHHHmmss".
  ''' </summary>

  Public Function CreateTimestamp() As String
    Return DateTime.Now.ToString("yyyyMMddHHmmss")
  End Function


  ''' <summary>
  ''' Returns the string name of an Enum value
  ''' in human readable format, split with spaces
  ''' along CamelCase boundaries.  For example, the call 
  ''' EnumInstanceToString(GetType(SomeEnum), SomeEnum.CamelCase)
  ''' returns the string "Camel Case"
  ''' </summary>
  ''' <param name="enumType"></param>
  ''' <param name="value"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>

  Public Function EnumInstanceToString(enumType As Type, value As Object) As String

    Return CamelCaseToHumanReadableString(
      [Enum].GetName(enumType, value)
    )

  End Function


  ''' <summary>
  ''' Split the supplied string at CamelCase boundaries
  ''' (i.e., "CamelCase" becomes ("Camel","Case")
  ''' Joins the array back together with a space separater
  ''' (e.g., "Camel Case")
  ''' </summary>
  ''' <remarks>
  ''' http://msdn.microsoft.com/en-us/library/az24scfc.aspx
  ''' http://stackoverflow.com/questions/4519739/split-camelcase-word-into-words-with-php-preg-match-regular-expression
  ''' </remarks>

  Private CamelCaseRegex As New Regex("(?<=[a-z])(?=[A-Z])")

  Public Function CamelCaseToHumanReadableString(ByRef inputString As String) As String
    Return String.Join(
      " ",
      CamelCaseRegex.Split(inputString)
    )
  End Function

  ' Print the byte array in a readable format. 
  Public Function ByteArrayToHumanReadableString(ByVal array() As Byte) As String
    Dim bytesAsString As New StringBuilder()
    For byteIndex As Integer = 0 To array.Length - 1
      bytesAsString.Append(
        String.Format(
          "{0:X2}", array(byteIndex)
        )
      )
      If byteIndex Mod 4 = 3 Then
        bytesAsString.Append(" ")
      End If
    Next 'byteIndex
    Return bytesAsString.ToString()
  End Function 'PrintByteArray

End Module
