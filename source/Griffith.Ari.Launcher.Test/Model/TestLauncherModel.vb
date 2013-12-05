''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports NUnit.Framework
Imports NSubstitute

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher.View

<TestFixture(), Category("Unit")>
Public Class TestLauncherModel

  Private _launcher As LauncherModel
  Private _launchActionMock As ILaunchAction

  Private Shared _viewConfig As ILauncherConfig = New TestLauncherViewConfig()

  <SetUp()>
  Public Sub Setup()

    _launchActionMock = Substitute.For(Of ILaunchAction)()

    _launcher = New LauncherModel

  End Sub

  <TearDown()>
  Public Sub TearDown()
  End Sub

  <Test()>
  Public Sub testShortNameInitial()
    Assert.AreEqual(
      Nothing,
      _launcher.ShortName
    )
  End Sub

  <Test()>
  Public Sub testShortNameUpdate()
    Dim newShortName = "Escape from the farty-pants zone at 'Ludicrous Speed'!"

    _launcher.ShortName = newShortName

    Assert.AreEqual(
      newShortName,
      _launcher.ShortName
    )
  End Sub

  <Test()>
  Public Sub testDescriptionInitial()
    Assert.AreEqual(
      Nothing,
      _launcher.Description
    )
  End Sub

  <Test()>
  Public Sub testDescriptionUpdate()
    Dim newDescription = "howdydoody!"

    _launcher.Description = newDescription

    Assert.AreEqual(
      newDescription,
      _launcher.Description
    )
  End Sub

  <Test()>
  Public Sub testIconInitial()
    Assert.AreEqual(
      Nothing,
      _launcher.Icon
    )
  End Sub

  <Test()>
  Public Sub testIconUpdate()
    _launcher.Icon = _viewConfig.DefaultIcon

    Assert.AreEqual(
      GetHashForIcon(_viewConfig.DefaultIcon),
      GetHashForIcon(_launcher.Icon)
    )
  End Sub

  <Test()>
  Public Sub testLaunchActionInitial()
    Assert.AreEqual(
      Nothing,
      _launcher.LaunchAction
    )
  End Sub

  <Test()>
  Public Sub testLaunchActionUpdate()
    _launcher.LaunchAction = _launchActionMock

    Assert.AreEqual(
      _launchActionMock,
      _launcher.LaunchAction
    )
  End Sub

End Class

