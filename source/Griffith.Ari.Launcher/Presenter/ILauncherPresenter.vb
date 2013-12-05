''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports Griffith.Ari.Launcher.View

''' <summary>
'''  An interface specifing the necessary behaviour for a LauncherPresenter, allowing
'''  it to bind an ILauncherView to a particular launcher model. 
''' </summary>
''' <remarks>This interface is a Presenter in the MVP pattern.</remarks>

Public Interface ILauncherPresenter

  ''' <summary>
  ''' Binds an ILauncherView to an IInternalLauncherModel, coodinating all model updates
  ''' to the view, and delegating view launch events to the launch action defined for the model.
  ''' </summary>
  ''' <param name="launchView"></param>
  ''' <param name="internalLauncher"></param>

  Sub Bind(ByRef launchView As ILauncherView, ByRef internalLauncher As IInternalLauncherModel)

  ''' <summary>
  ''' Binds an ILauncherView to an IExternalLauncherModel, coodinating all model updates
  ''' to the view, and delegating view launch events to the launch action defined for the model.
  ''' </summary>
  ''' <param name="launchView"></param>
  ''' <param name="externalLauncher"></param>

  Sub Bind(ByRef launchView As ILauncherView, ByRef externalLauncher As IExternalLauncherModel)

End Interface
