''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Drawing

''' <summary>
''' A Launcher model interface, describing the necessary state that a Launcher must have,
''' including the action it should take upon a launch.  
''' </summary>
''' <remarks></remarks>

Public Interface ILauncherModel
  Property ShortName As String
  Property Description As String
  Property Icon As Image
  Property LaunchAction As ILaunchAction
End Interface

''' <summary>
'''  A launcer model interface specific to externally defined launchers. 
''' </summary>
''' <remarks></remarks>

Public Interface IExternalLauncherModel
  Inherits ILauncherModel
End Interface

''' <summary>
'''  A launcer model interface specific to internally (within the system) defined launchers. 
''' </summary>
''' <remarks></remarks>

Public Interface IInternalLauncherModel
  Inherits ILauncherModel
End Interface

''' <summary>
''' A launch action interface, defined to allow a self-contained instance of a launch action
''' to fire as per the command pattern.
''' </summary>
''' <remarks>See: http://en.wikipedia.org/wiki/Command_pattern</remarks>
''' 
Public Interface ILaunchAction

  ''' <summary>
  ''' Triggers launching the behaviour configured for the given ILaunchAction instance.
  ''' </summary>

  Sub Launch()

End Interface