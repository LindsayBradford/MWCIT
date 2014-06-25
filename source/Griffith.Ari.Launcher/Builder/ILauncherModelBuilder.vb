''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Reflection

Imports Griffith.Ari.Launcher.View

''' <summary>
''' Defines the nessary behaviour expected of a builder that generates IExternalLauncherModel and IInternalLauncherModel instances. 
''' </summary>
''' <remarks></remarks>

Public Interface ILauncherModelBuilder

  ''' <summary>
  ''' Builds an IInternalLauncherMdoel given the supplied launchKey and launchActionView.
  ''' The launchKey is used to retrieve configuration data embedded in the ViewAssembly for this
  ''' particular Launcher's model.
  ''' The launchActionView defines a view, finalsied in the ViewAssembly, that should be 
  ''' 'launched' when launcher built here is fired. 
  ''' </summary>
  ''' <param name="launchKey"></param>
  ''' <param name="launchActionView"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  ''' 
  Function BuildInternaModel(ByRef launchKey As String, ByRef launchActionView As IInternalLaunchActionView) As IInternalLauncherModel

  ''' <summary>
  ''' Builds an IExternalLauncherMdoel given the supplied configuration file (defined, and residing outside the system).
  ''' </summary>
  ''' <param name="configFile"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>

  Function BuildExternalModel(ByRef configFile As String) As IExternalLauncherModel

End Interface
