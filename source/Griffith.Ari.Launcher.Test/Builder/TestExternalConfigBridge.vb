''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University and other contributors. '
'                                                                                              '
' This program and the accompanying materials are made available under the terms of the        '
' BSD 3-Clause licence which accompanies this distribution, and is available at                '
' http://opensource.org/licenses/BSD-3-Clause                                                  '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports NUnit.Framework
Imports NSubstitute

Imports System.IO
Imports System.Reflection

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher
Imports Griffith.Ari.Launcher.View

<TestFixture(), Category("Unit")>
Public Class TestExternalConfigBridge

  Structure BridgeState
    Public bridge As ExternalConfigBridge
    Public path As String
  End Structure

  Dim _validBridgeState, _invalidBridgeState As BridgeState

  Dim _viewConfig As ILauncherConfig = New TestLauncherViewConfig

  <TestFixtureSetUp()>
  Public Sub FixtureSetup()

    _validBridgeState = New BridgeState

    _validBridgeState.path =
      AssemblyUtiityCollection.UnpackResource(
        Assembly.GetExecutingAssembly,
        FileUtilityCollection.GetTempPath,
        "Valid_Null_Launcher.INI"
      )

    _validBridgeState.bridge =
      New ExternalConfigBridge(
        Assembly.GetExecutingAssembly,
       _viewConfig
    )

    _validBridgeState.bridge.ConfigFile = _validBridgeState.path

    _invalidBridgeState = New BridgeState

    _invalidBridgeState.path =
      AssemblyUtiityCollection.UnpackResource(
        Assembly.GetExecutingAssembly,
        FileUtilityCollection.GetTempPath,
        "Really_Invalid_Launcher.INI"
      )

    _invalidBridgeState.bridge =
      New ExternalConfigBridge(
        Assembly.GetExecutingAssembly,
        _viewConfig
    )
    _invalidBridgeState.bridge.ConfigFile = _invalidBridgeState.path

  End Sub

  <TestFixtureTearDown()>
  Public Sub FixtureTearDown()
    FileUtilityCollection.DeleteFile(_validBridgeState.path)
    FileUtilityCollection.DeleteFile(_invalidBridgeState.path)
  End Sub

  <Test()>
  Public Sub GetShortName_ValidBridge_ValidShortName()
    Assert.AreEqual(
      "VALID",
      _validBridgeState.bridge.GetShortName()
    )
  End Sub

  <Test()>
  Public Sub GetShortName_InvalidBridge_DefaultShortName()
    Assert.AreEqual(
      _invalidBridgeState.bridge.GetShortName(),
      _viewConfig.DefaultShortName
    )
  End Sub

  <Test()>
  Public Sub GetDescription_ValidBridge_ValidDescription()
    Assert.AreEqual(
      _validBridgeState.bridge.GetDescription(),
      "Valid Null Launcher"
    )
  End Sub

  <Test()>
  Public Sub GetDescription_InvalidBridge_DefaultDescription()
    Assert.AreEqual(
      _viewConfig.DefaultDescription,
      _invalidBridgeState.bridge.GetDescription()
    )
  End Sub

  <Test()>
  Public Sub GetIcon_ValidBridge_ValidIcon()

    Dim bridgeIcon = _validBridgeState.bridge.GetIcon()
    Dim embeddedIcon = AssemblyUtiityCollection.RetrieveEmbeddedImage("Info.png")

    Assert.AreEqual(
      GetHashForIcon(bridgeIcon),
      GetHashForIcon(embeddedIcon)
    )
  End Sub

  <Test()>
  Public Sub GetIcon_InvalidBridge_DefaultIcon()

    Dim bridgeIcon = _invalidBridgeState.bridge.GetIcon()
    Dim embeddedIcon = _viewConfig.DefaultIcon

    Assert.AreEqual(
      GetHashForIcon(bridgeIcon),
      GetHashForIcon(embeddedIcon)
    )
  End Sub

End Class

