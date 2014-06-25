''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Drawing
Imports System.Reflection

Namespace View

  ''' <summary>
  ''' Defines the interface necessary for a view that displays a set of launchers.
  ''' </summary>

  Public Interface ILauncherSetView
    Sub AddLauncherView(ByRef launcher As ILauncherView)
    Function Count() As Integer
    Sub Show()
  End Interface

  ''' <summary>
  ''' Defines the interface necessary for a view that displays a launcher.
  ''' </summary>

  Public Interface ILauncherView

    Property ShortName As String
    Property Description As String
    Property Icon As Image

    Event LauncherFired As Action

  End Interface

  ''' <summary>
  ''' Defines the interface necessary for views that should be shown as a result
  ''' of an ILauncherModel's LaunchAction firing. 
  ''' </summary>
  ''' <remarks></remarks>

  Public Interface IInternalLaunchActionView
    Sub Show()
  End Interface

  ''' <summary>
  ''' Defines the interface necessary for a view that reports a launch action with missing 'Action' configuration.
  ''' </summary>
  ''' <remarks>See also: ILaunchAction</remarks>

  Public Interface IMissingActionConfigView
    Inherits IInternalLaunchActionView
  End Interface

  ''' <summary>
  ''' Defines the interface necessary for a view that reports a general error to a launch action firing.
  ''' </summary>
  ''' <remarks>See also: ILaunchAction</remarks>

  Public Interface ILaunchActionErrorView
    Inherits IInternalLaunchActionView
  End Interface

  ''' <summary>
  ''' Defines the interface necessary for a view that reports a general error to a launch action firing.
  ''' </summary>
  ''' <remarks>See also: ILaunchAction</remarks>

  Public Interface ILaunchActionSecurityErrorView
    Inherits IInternalLaunchActionView
  End Interface

  ''' <summary>
  ''' Defines the interface necessary for a view that reports a general 'about message' view as a result of a launch action firing.
  ''' </summary>
  ''' <remarks>See also: ILaunchAction</remarks>

  Public Interface IAboutlLaunchActionView
    Inherits IInternalLaunchActionView
  End Interface

  ''' <summary>
  ''' Defines the interface nessary for a View assembly to configure its specific file and launcher defaults. 
  ''' </summary>
  ''' <remarks></remarks>

  Public Interface ILauncherConfig

    ''' <summary>
    ''' The file-name of the single internal config file embedded in the ViewAssembly that configures all
    ''' internal launcher views for use. 
    ''' </summary>
    ''' <remarks></remarks>

    ReadOnly Property InternalConfigFile As String

    ''' <summary>
    ''' The exepcted INI configuration file name for all launchers run externally to the sustem. 
    ''' </summary>
    ''' <remarks>If this file is not found when searching for external launchers, no launcher will be created for the file's launcher directory.</remarks>

    ReadOnly Property ExternaConfigFiles As String

    ''' <summary>
    ''' The default Launcher ShortName to use when one is not specified in a configuration file. 
    ''' </summary>
    ''' <remarks></remarks>

    ReadOnly Property DefaultShortName As String

    ''' <summary>
    ''' The default Launcher Description to use when one is not specified in a configuration file. 
    ''' </summary>
    ''' <remarks></remarks>

    ReadOnly Property DefaultDescription As String

    ''' <summary>
    ''' The default Launcher Icon image file path to use when one is not specified in a configuration file. 
    ''' </summary>
    ''' <remarks>Note that the image file expected to be an embedded resoutce of the ViewAssembly.</remarks>

    ReadOnly Property DefaultIcon As Image
  End Interface

End Namespace

