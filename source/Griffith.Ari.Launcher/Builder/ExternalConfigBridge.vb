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

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

''' <summary>
''' A helper class to LauncherModelBuilder that acts as a bridge through to a
''' config file specifying launcher detail. This class takes care of 
''' resolving incomplete configuration data when none was specified in the
''' external config files.
''' </summary>
''' <remarks>
''' This class is reusable across config files. 
''' Modify the ConfigFile property to have it bridge to a different config file.
''' </remarks>

Public Class ExternalConfigBridge
  Private _viewConfig As ILauncherConfig
  Private _embeddingAssembly As Assembly

  Private _configFile As String

  ''' <summary>
  ''' Creates a new bridge using embeddingAssembly to retrieve view data, and viewConfig
  ''' to specify defaults when the config file this object acts as a bridge for does not
  ''' contain complete configuration. 
  ''' </summary>
  ''' <param name="embeddingAssembly"></param>
  ''' <param name="viewConfig"></param>
  ''' <remarks></remarks>

  Public Sub New(ByRef embeddingAssembly As Assembly, ByVal viewConfig As ILauncherConfig)
    Me._embeddingAssembly = embeddingAssembly
    Me._viewConfig = viewConfig
  End Sub

  ''' <summary>
  ''' The path through to a launcer configuration file. that this class acts as a 
  ''' bridge for.  
  ''' </summary>

  Public Property ConfigFile As String
    Get
      Return Me._configFile
    End Get
    Set(value As String)
      Me._configFile = value
    End Set
  End Property

  ''' <summary>
  '''  Retrieves the first ShortName value stored in the bridged INI configuration file. 
  ''' </summary>
  ''' <remarks>
  ''' Assumes the file specified can be read, and is a valid INI file.
  ''' Uses the default ShortName supplied by viewConfig supplied on the constructor when 
  ''' one cannot be found in the configuration file.
  ''' </remarks>

  Public Function GetShortName() As String
    Return ReadValue(Config.SHORT_NAME, _viewConfig.DefaultShortName)
  End Function

  ''' <summary>
  '''  Retrieves the first Description value stored in the bridged INI configuration file. 
  ''' </summary>
  ''' <remarks>
  ''' Assumes the file specified can be read, and is a valid INI file.
  ''' Uses the default desciption supplied by viewConfig supplied on the constructor when 
  ''' one cannot be found in the configuration file.
  ''' </remarks>

  Public Function GetDescription() As String
    Return ReadValue(Config.DESCRIPTION, _viewConfig.DefaultDescription)
  End Function

  ''' <summary>
  '''  Retrieves an Image file based on the Icon value stored in the bridged INI configuration file. 
  ''' </summary>
  ''' <remarks>
  ''' Assumes the file specified can be read, and is a valid INI file.
  ''' Tries loading a file external to the system as a relative-path as specified by the Icon value.
  ''' If that fails, it checks internally with the embeddedAssembly to see if there is a matching image
  ''' file, and supplies that instead.
  ''' Finally, if there is no external or no internal image file for the Icon config item, a default
  ''' icon image as defined by the embeddingAssembly's ViewConfig is used as a last resort.
  ''' </remarks>


  Public Function GetIcon() As Image

    Dim baseIconFile = ReadValue(Config.ICON)

    If baseIconFile Is Nothing Then Return _viewConfig.DefaultIcon

    Dim externalImageFile = GetFileRelativeToPath(ConfigFile, baseIconFile)
    If CanLoadFile(externalImageFile) Then Return Image.FromFile(externalImageFile)

    Dim internalImage = AssemblyUtiityCollection.RetrieveEmbeddedImage(_embeddingAssembly, baseIconFile)
    If internalImage IsNot Nothing Then Return internalImage

    Return _viewConfig.DefaultIcon

  End Function

  ''' <summary>
  '''  Retrieves the first Action value stored in the bridged INI configuration file. 
  ''' </summary>
  ''' <remarks>Assumes the file specified can be read, and is a valid INI file.</remarks>

  Public Function GetAction() As String
    Return ReadValue(Config.ACTION)
  End Function

  ''' <summary>
  ''' Returns the directory in which the config file was located. This directory will be
  ''' used as the 'working directory' for any action fired for this config file. 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>

  Public Function GetWorkingDirectory() As String
    Return Path.GetDirectoryName(ConfigFile)
  End Function

  Private Function ReadValue(ByVal configKey As String, Optional defaultValue As String = Nothing) As String
    Dim value = INIFileCollection.ReadValueOfFirstSection(ConfigFile, configKey)

    If value Is Nothing OrElse value.Equals("") Then value = defaultValue

    Return value
  End Function
End Class ' ExternalConfigBridge
