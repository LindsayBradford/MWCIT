''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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
