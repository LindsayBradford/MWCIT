''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Drawing
Imports System.Windows.Forms

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

''' <summary>
'''  Am abstract implementation of  ILauncherView, as a WinForms button. 
''' Clicking the button will fire an ILaunchView.ItemSelected event.
''' </summary>

Public NotInheritable Class LauncherButton
  Inherits Button
  Implements ILauncherView

  Public Sub New()
    MyBase.New()

    TextAlign = ContentAlignment.BottomCenter
    ImageAlign = ContentAlignment.TopCenter
  End Sub

  ''' <summary>
  '''  Returns the ShortName property of this view object. 
  ''' </summary>
  ''' <remarks>The ILaunchView.ShortName property of this view is also it's Button.Text property.</remarks>

  Public Property ShortName As String Implements ILauncherView.ShortName
    Get
      Return Me.Text
    End Get
    Set(value As String)
      Me.Text = value
    End Set
  End Property

  Private _description As String

  ''' <summary>
  '''  Returns the Description property of this view object. 
  ''' </summary>

  Public Property Description As String Implements ILauncherView.Description
    Get
      Return Me._description
    End Get
    Set(value As String)
      Me._description = value
    End Set
  End Property

  ''' <summary>
  '''  Returns the Icon property of this view object. 
  ''' </summary>
  ''' <remarks>The ILaunchView.Icon  property of this view is also it's Button.Image property.</remarks>

  Public Property Icon As Image Implements ILauncherView.Icon
    Get
      Return Me.Image
    End Get
    Set(value As Image)
      Me.Image = value

      Dim squareLength = value.Height + 10 + Me.FontHeight
      Me.Size = New Size(squareLength, squareLength)

      Me.MinimumSize = Me.Size
      Me.MaximumSize = Me.Size

    End Set
  End Property

  ''' <summary>
  ''' This event is fired whenever its Button.OnClick() method is invoked. 
  ''' </summary>

  Public Event LauncherFired() Implements ILauncherView.LauncherFired

  Protected Overrides Sub OnClick(e As System.EventArgs)
    MyBase.OnClick(e)
    RaiseEvent LauncherFired()
  End Sub

  <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> _
  Public Event HoveringOverLauncher(ByRef hoverDescription As String)

  Protected Overrides Sub OnMouseHover(e As System.EventArgs)
    MyBase.OnMouseHover(e)
    RaiseEvent HoveringOverLauncher(Me.Description)
  End Sub

  <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> _
  Public Event LeavingLauncher()

  Protected Overrides Sub OnMouseLeave(e As System.EventArgs)
    MyBase.OnMouseLeave(e)
    RaiseEvent LeavingLauncher()
  End Sub

End Class
