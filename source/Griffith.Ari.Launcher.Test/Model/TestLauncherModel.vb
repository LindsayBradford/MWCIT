''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports NUnit.Framework
Imports NSubstitute

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

<TestFixture(), Category("Unit")>
Public Class TestLauncherModel

  Private _launcher As LauncherModel
  Private _fakeLaunchAction As ILaunchAction

  Private Shared _viewConfig As ILauncherConfig = New TestLauncherViewConfig()

  <SetUp()>
  Public Sub Setup()

    _fakeLaunchAction = Substitute.For(Of ILaunchAction)()

    _launcher = New LauncherModel

  End Sub

  <TearDown()>
  Public Sub TearDown()
  End Sub

  <Test()>
  Public Sub ShortName_InitialProperties_ValueNothing()
    Assert.AreEqual(
      Nothing,
      _launcher.ShortName
    )
  End Sub

  <Test()>
  Public Sub ShortName_UpdateProperties_ValueMatches()
    Dim newShortName = "Escape from the farty-pants zone at 'Ludicrous Speed'!"

    _launcher.ShortName = newShortName

    Assert.AreEqual(
      newShortName,
      _launcher.ShortName
    )
  End Sub

  <Test()>
  Public Sub Description_InitialProperties_ValueNothing()
    Assert.AreEqual(
      Nothing,
      _launcher.Description
    )
  End Sub

  <Test()>
  Public Sub Description_UpdateProperties_ValueMatches()
    Dim newDescription = "howdydoody!"

    _launcher.Description = newDescription

    Assert.AreEqual(
      newDescription,
      _launcher.Description
    )
  End Sub

  <Test()>
  Public Sub Icon_InitialProperties_ValueNothing()
    Assert.AreEqual(
      Nothing,
      _launcher.Icon
    )
  End Sub

  <Test()>
  Public Sub Icon_UpdateProperties_ValueMatches()
    _launcher.Icon = _viewConfig.DefaultIcon

    Assert.AreEqual(
      GetHashForIcon(_viewConfig.DefaultIcon),
      GetHashForIcon(_launcher.Icon)
    )
  End Sub

  <Test()>
  Public Sub LaunchAction_InitialProperties_ValueNothing()
    Assert.AreEqual(
      Nothing,
      _launcher.LaunchAction
    )
  End Sub

  <Test()>
  Public Sub LaunchAction_UpdateProperties_ValueMatches()
    _launcher.LaunchAction = _fakeLaunchAction

    Assert.AreEqual(
      _fakeLaunchAction,
      _launcher.LaunchAction
    )
  End Sub

End Class