''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

''' <summary>
''' A Module defining all constants that pertain specifically to managing the interaction
''' between launchers and launch actions.
''' </summary>

Public Module LauncherConstants

  ''' <summary>
  ''' The INI configuration file keys that matter to allowing a launcher to fire an action. 
  ''' </summary>

  Public Structure Config
    Const SHORT_NAME = "ShortName"
    Const DESCRIPTION = "Description"
    Const ACTION = "Action"
    Const ICON = "Icon"
  End Structure ' Config

  ''' <summary>
  ''' The set of keys to launch actions that a view must implement, but has complete control over how it renders the action. 
  ''' </summary>

  Public Structure InternalLauncherKeys
    ' 'About' information appropriate to the view relying on this framework.
    Const SHOW_ABOUT_INFO = "About"
  End Structure

End Module
