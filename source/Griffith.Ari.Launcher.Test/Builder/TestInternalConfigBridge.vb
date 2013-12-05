''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Copyright (c) 2013, Australian Rivers Institute, Griffith University.              '
' Author: Lindsay Bradford                                                           '
' Created as part of the MWCIT project                                               '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports NUnit.Framework
Imports NSubstitute

Imports System.IO
Imports System.Reflection
Imports System.Security.Cryptography

Imports Griffith.Ari.General
Imports Griffith.Ari.Launcher
Imports Griffith.Ari.Launcher.View

<TestFixture(), Category("Unit")>
Public Class TestInternalConfigBridge

  Dim _configFilePath As String

  Dim _internalConfigBridge As InternalConfigBridge
  Dim _viewConfig As ILauncherConfig = New TestLauncherViewConfig

  <TestFixtureSetUp()>
  Public Sub Setup()

    _configFilePath =
      AssemblyUtiityCollection.UnpackResource(
        FileUtilityCollection.GetTempPath,
        _viewConfig.InternalConfigFile
      )

    _internalConfigBridge =
      New InternalConfigBridge(
        Assembly.GetExecutingAssembly,
        _configFilePath
      )

  End Sub

  <TestFixtureTearDown()>
  Public Sub TearDown()
    File.Delete(_configFilePath)
  End Sub

  <Test()>
  Public Sub testValidGetShortName()
    Assert.AreEqual(
      "About",
      _internalConfigBridge.GetShortName("About")
    )
  End Sub

  <Test()>
  Public Sub testInvalidGetShortName()
    Assert.AreEqual(
      _internalConfigBridge.GetShortName("Invalid"),
      ""
    )
  End Sub

  <Test()>
  Public Sub testValidGetDescription()
    Assert.AreEqual(
      _internalConfigBridge.GetDescription("About"),
      "About the Murrumbidgee Wetland Condition Indicator Tool"
    )
  End Sub

  <Test()>
  Public Sub testInvalidGetDescription()
    Assert.AreEqual(
      _internalConfigBridge.GetDescription("Invalid"),
      ""
    )
  End Sub

  <Test()>
  Public Sub testValidGetIcon()

    Dim bridgeIcon = _internalConfigBridge.GetIcon("About")
    Dim embeddedIcon = _viewConfig.DefaultIcon

    Assert.AreEqual(
      GetHashForIcon(bridgeIcon),
      GetHashForIcon(embeddedIcon)
    )
  End Sub

  <Test()>
  Public Sub testInvalidGetIcon()
    Assert.AreEqual(
      _internalConfigBridge.GetIcon("Invalid"),
      Nothing
    )
  End Sub
End Class

