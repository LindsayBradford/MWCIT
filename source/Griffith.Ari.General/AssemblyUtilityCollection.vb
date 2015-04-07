''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Security.Cryptography

Imports Griffith.Ari.General

''' <summary>
''' A collection of utilities that interact with .NET Assemblies.
''' </summary>
''' <remarks></remarks>
''' 
Public Module AssemblyUtiityCollection

  ''' <summary>
  ''' Returns the short name of the suppled assembly. 
  ''' </summary>

  Public Function GetAssemblyName(ByRef theAssembly As [Assembly]) As String
    If theAssembly Is Nothing Then
      Throw New ArgumentException("Assembly specified has value of Nothing.")
    End If

    Return theAssembly.GetName().Name
  End Function

  ''' <summary>
  ''' Upacks all the resources of the executing assembly into the 
  ''' path specified. 
  ''' </summary>
  ''' <param name="filePath"></param>
  ''' <returns>Returns a string array of absolute paths
  ''' through to each of the unpacked resources.</returns>
  ''' <remarks></remarks>
  ''' 
  Public Function UnpackResources(ByRef assemblyWithResources As Assembly, ByRef filePath As String) As String()

    If assemblyWithResources Is Nothing Then
      Throw New ArgumentException("Assembly specified has value of Nothing.")
    End If

    Dim resourcePaths = New List(Of String)

    For Each resource As String In assemblyWithResources.GetManifestResourceNames()
      Using resourceStream As Stream = assemblyWithResources.GetManifestResourceStream(resource)
        Dim fileName As String = Path.GetFileName(resource)

        If fileName.EndsWith("resources") Then Continue For 'not interested in the resource list

        'Debug.WriteLine(
        '  String.Format("Writing resource file ({0}) to ({1}).", fileName, filePath)
        ')

        Dim absoluteFilePath = Path.Combine(filePath, fileName)

        Using outputStream As New FileStream(absoluteFilePath, FileMode.Create)

          FileUtilityCollection.CopyStream(resourceStream, outputStream)
          resourcePaths.Add(absoluteFilePath)

        End Using 'outputStream
      End Using 'resourceStream
    Next

    Return resourcePaths.ToArray()
  End Function

  ''' <summary>
  ''' Upacks the desired resource file from the calling assembly and saves it into the 
  ''' path specified. 
  ''' </summary>
  ''' <remarks>
  ''' The desiredFileName does not to be qualified by its containing assembly.
  ''' For instance, the file "Icon.png", stored in assembly "Some.Assembly.dll"
  ''' whould have a qualified reference of "Some.Assembly.Icon.png". This method
  ''' only needs the base "Icon.png" filename, and resolves the rest.
  ''' </remarks>

  Public Function UnpackResource(ByRef filePath As String, ByRef desiredFileName As String) As String
    Return UnpackResource(
      Assembly.GetCallingAssembly(),
      filePath,
      desiredFileName
    )
  End Function

  ''' <summary>
  ''' Upacks the desired resource file from the embeddingAssembly and saves it into the 
  ''' path specified. 
  ''' </summary>
  ''' <remarks>
  ''' The desiredFileName does not to be qualified by its containing assembly.
  ''' For instance, the file "Icon.png", stored in assembly "Some.Assembly.dll"
  ''' whould have a qualified reference of "Some.Assembly.Icon.png". This method
  ''' only needs the base "Icon.png" filename, and resolves the rest.
  ''' </remarks>

  Public Function UnpackResource(ByRef embeddingAssembly As Assembly, ByRef filePath As String, ByRef desiredFileName As String) As String

    For Each resource In embeddingAssembly.GetManifestResourceNames()
      Using resourceStream = embeddingAssembly.GetManifestResourceStream(resource)
        Dim fileName = Path.GetFileName(resource)

        Dim qualifiedFileName = String.Format("{0}.{1}", GetAssemblyName(embeddingAssembly), desiredFileName)

        If Not fileName.Equals(qualifiedFileName) Then Continue For 'not interested in the resource list

        'Debug.WriteLine(
        '  String.Format("Writing resource file ({0}) to ({1}).", fileName, filePath)
        ')

        Dim absoluteFilePath = Path.Combine(filePath, desiredFileName)

        Using outputStream As New FileStream(absoluteFilePath, FileMode.Create)

          FileUtilityCollection.CopyStream(resourceStream, outputStream)

          Return absoluteFilePath

        End Using 'outputStream
      End Using 'resourceStream
    Next

    Return Nothing
  End Function

  ''' <summary>
  ''' Retrieves the file specified on imagePath, embedded in embeddingAssembly, and returns it as 
  ''' an Image object. 
  ''' </summary>
  ''' <remarks>Assumes the file is in fact a valid image file.</remarks>

  Public Function RetrieveEmbeddedImage(ByRef embeddingAssembly As Assembly, ByRef imagePath As String) As Image

    Dim fullPath As String = String.Format("{0}.{1}", GetAssemblyName(embeddingAssembly), imagePath)

    If Not embeddingAssembly.GetManifestResourceNames().Contains(fullPath) Then
      Return Nothing
    End If

    Using stream = embeddingAssembly.GetManifestResourceStream(fullPath)

      Dim embeddedImage = Image.FromStream(stream)

      Return embeddedImage
    End Using
  End Function

  ''' <summary>
  ''' Retrieves the file specified on imagePath, embedded in the calling assembly, and returns it as 
  ''' an Image object. 
  ''' </summary>
  ''' <remarks>Assumes the file is in fact a valid image file.</remarks>

  Public Function RetrieveEmbeddedImage(ByRef imagePath As String) As Image

    Return RetrieveEmbeddedImage(
      [Assembly].GetCallingAssembly(),
      imagePath
    )

  End Function

  ''' <summary>
  ''' Returns all class types that can be instantiated for the given interface that are packaged within the specified assembly.
  ''' </summary>
  ''' <param name="assemblyWithTypes"></param>
  ''' <param name="interfaceType"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>

  Public Function GetConcreteTypesForInterface(ByRef assemblyWithTypes As Assembly, ByRef interfaceType As Type) As List(Of Type)
    Dim implementingTypeList = New List(Of Type)

    If assemblyWithTypes Is Nothing Then
      Return implementingTypeList
    End If

    For Each assemblyType As Type In assemblyWithTypes.GetTypes()
      If assemblyType.GetInterfaces().Contains(interfaceType) AndAlso Not assemblyType.IsAbstract Then
        'Debug.WriteLine(
        '  String.Format(
        '    "Type ({0}) implements ({1}). Adding to list.",
        '     assemblyType.ToString,
        '     mediatorType.ToString
        '  )
        ')
        implementingTypeList.Add(assemblyType)
      End If
    Next 'assemblyType

    Return implementingTypeList
  End Function

  ''' <summary>
  ''' Returns an instance of a class from within viewAssembly that implements the interface T.  
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="viewAssembly"></param>
  ''' <returns></returns>
  ''' <remarks>
  ''' If there are more thane one classes that implement the specified interface within the assembly,
  ''' no guarantee is made on which you'll get back.
  ''' Throws an ArgumentException if there are no implementing classses found. 
  ''' </remarks>

  Public Function BuildInstanceOfInterface(Of T)(ByRef viewAssembly As [Assembly]) As T

    If viewAssembly Is Nothing Then
      Throw New ArgumentException("Assembly specified has value of Nothing.")
    End If

    Dim matchingTypes As List(Of Type) = GetConcreteTypesForInterface(viewAssembly, GetType(T))

    If matchingTypes.Count = 0 Then
      Dim exceptionMessage =
        String.Format(
          "Assembly {0} does not contain a class implementing interface {1}.",
          GetAssemblyName(viewAssembly),
          GetType(T).Name
        )
      Throw New ArgumentException(exceptionMessage)
    End If

    Return Activator.CreateInstance(matchingTypes(0))

  End Function

  Public Function RetrieveSHA256(ByRef thisAssembly As [Assembly]) As Byte()

    If thisAssembly Is Nothing Then
      Throw New ArgumentException("Assembly specified has value of Nothing.")
    End If

    Dim mySHA256 As SHA256 = SHA256Managed.Create()
    Dim hashValue() As Byte

    Using stream = File.OpenRead(thisAssembly.Location)

      hashValue = mySHA256.ComputeHash(stream)

    End Using

    Return hashValue
  End Function

  Public Function RetrieveEntryAssemblySHA256AsString() As String
    Return ByteArrayToHumanReadableString(
      RetrieveSHA256([Assembly].GetEntryAssembly)
    )
  End Function

  Public Function RetrieveSHA256AsString(ByRef thisAssembly As [Assembly]) As String
    Return ByteArrayToHumanReadableString(
      RetrieveSHA256(thisAssembly)
    )
  End Function

End Module
