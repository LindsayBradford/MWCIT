''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '

Imports System.Reflection
Imports Griffith.Ari.Launcher.View

''' <summary>
''' An interface decribing the needed behaviour of a builder that produces a fully-functional set of launchers. 
''' </summary>
''' <remarks>This launcher framework is a simplified instance of the MVP Pattern</remarks>

Public Interface ILauncherSetBuilder

  ''' <summary>
  ''' Builds an instance of the interface ILauncherSetView using viewAssembly to construct needed view classes, and 
  ''' the content of launcherSetDirectory to construct the launchers and actions to fire per launcher to tie into those views. 
  ''' The end-result should be a fully-functional set of launchers rendered entirely from classes found in viewAssembly. 
  ''' </summary>
  ''' <param name="viewAssembly">The assembly containing concrete implementations of views from Griffith.Ari.Launcher.View</param>
  ''' <param name="launcherSetDirectory">A directory containing configuration data for the launchers to construct</param>
  ''' <returns>A concrete fully-functional implementation of ILauncherSetView.</returns>
  ''' <remarks></remarks>

  Function BuildFrom(ByRef viewAssembly As [Assembly], ByVal launcherSetDirectory As String) As ILauncherSetView
End Interface
