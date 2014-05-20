[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)

#define MyShortAppName "MWCIT"
#define MyLongAppName "Murrumbidgee Wetlands Condition Indicator Tool"

#define MyAppPublisher "Australia Rivers Institute, Griffith University"
#define MyAppURL "http://www.griffith.edu.au/environment-planning-architecture/australian-rivers-institute"

#define INI_DataDirKey "DataDirectory"

#define BaseItemDir "..\..\"
#define DeployDir "\DeployData"

; MWCIT file defines

#define MWCIT_SourceBaseDir ".."
#define MWCIT_SourceBuildDir MWCIT_SourceBaseDir + "\source\MWCIT\bin\x86\Release"
#define MWCIT_Executable MyShortAppName + ".exe"
#define MWCIT_Executable_BuildPath MWCIT_SourceBuildDir + "\" + MWCIT_Executable

#define FullAppVersion GetFileVersion(MWCIT_Executable_BuildPath)
#define StripBuild(str VerStr) Copy(VerStr, 1, RPos(".", VerStr)-1)
#define MyAppVersion StripBuild(FullAppVersion)

; MWRD file defines

#define MWRD "MWRD"
#define MWRD_Tools MWRD + "Tools"

#define MWRD_SourceBaseDir BaseItemDir + MWRD
#define MWRD_SourceBuildDir MWRD_SourceBaseDir + "\source\" + MWRD_TOOLS + "\bin\Release"
#define MWRD_SourceDeployDataDir MWRD_SourceBaseDir + DeployDir

#define MWRD_AddonDll MWRD_Tools + ".dll"
#define MWRD_AddonTlb MWRD_Tools + ".esriAddIn"

; MWAD file defines

#define MWAD_SourceDeployDataDir BaseItemDir + "MWAD" + DeployDir

; MWRD addon registration requires admin priviliges.
PrivilegesRequired=admin     
AppID={{819FF14B-38B3-4937-850A-F91D08C840D5}}
AppName={#MyLongAppName}
AppVersion={#MyAppVersion}
LicenseFile=EULA.txt
AppCopyright=(c) 2013, Australian Rivers Institute
DiskSpanning=True
DiskSliceSize=1566000000
SlicesPerDisk=3
AppVerName={#MyLongAppName} version {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyShortAppName}
DefaultGroupName={#MyShortAppName}
OutputBaseFilename=Setup{#MyShortAppName}{#MyAppVersion}
Compression=lzma/Max
SolidCompression=true

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Types]
Name: "full"; Description: "Full installation"
Name: "custom"; Description: "Custom installation"; Flags: iscustom

[Components]
Name: MWCIT_Tools; Description: The MWCIT integrated toolset; Types: full custom; Flags: fixed
Name: MWRD_Data;  Description:  Murrumbidgee Wetland Relational Database;  Types: full
Name: MWAD_Data;  Description: Murrumbidgee Water Asset Database; Types: full

[Dirs]
Name: {code:GetDataDir}; Check: not DataDirExists; Flags: uninsneveruninstall; Permissions: users-modify

[Files]
Source: "{#MWCIT_SourceBuildDir}\{#MWCIT_Executable}"; DestDir: "{app}"; Components: MWCIT_Tools; Flags: ignoreversion

Source: "{#MWRD_SourceBuildDir}\{#MWRD_AddonDll}"; DestDir: "{app}"; Components: MWCIT_Tools; Flags: ignoreversion
Source: "{#MWRD_SourceBuildDir}\{#MWRD_AddonTlb}"; DestDir: "{app}"; Components: MWCIT_Tools; Flags: ignoreversion
Source: "{#MWRD_SourceDeployDataDir}\*"; DestDir: "{code:GetDataDir}\MWRD\"; Components: MWRD_Data; Flags: confirmoverwrite recursesubdirs uninsneveruninstall
Source: "{#MWAD_SourceDeployDataDir}\*"; DestDir: "{code:GetDataDir}\MWAD\"; Components: MWAD_Data; Flags: confirmoverwrite recursesubdirs uninsneveruninstall

[Tasks]
Name: desktopicon; Description: "Create a &desktop icon"; 

[Icons]
Name: "{group}\{#MyShortAppName}"; Filename: "{app}\{#MWCIT_Executable}"; Components: MWCIT_Tools;
Name: "{commondesktop}\{#MyShortAppName}"; Filename: "{app}\{#MWCIT_Executable}"; Components: MWCIT_Tools; Tasks: desktopicon;

[INI]
FileName: "{app}\{#MyShortAppName}.ini"; Section: "{#MyShortAppName}"; Key: "{#INI_DataDirKey}"; String: "{code:GetDataDir}"

[Run]
Filename: "{cf32}\ArcGIS\bin\esriRegasm.exe"; Parameters: " ""{app}\{#MWRD_AddonDll}"" /p:Desktop /s"; StatusMsg: "Registering MWRD addon component with ArcMap"; Flags: nowait skipifsilent

[UninstallRun]
Filename: "{cf32}\ArcGIS\bin\esriRegasm.exe"; Parameters: " ""{app}\{#MWRD_AddonDll} "" /p:Desktop /u /s";

[Code]
// global vars
var
  DataDirPage: TInputDirWizardPage;
  SampleDataPage: TInputOptionWizardPage;
  DataDirVal: String;

function GetDataDir(Param: String): String;
begin
  { Return the selected DataDir }
  Result := DataDirPage.Values[0];
end;

function GetDefaultDataDirectory() : String;
begin
  Result := ExpandConstant('{localappdata}\{#MyShortAppName}');
end;

function GetIniFilename() : String;
begin
    Result :=  WizardDirValue() + '\{#MyShortAppName}.ini';
end;
  
  // custom wizard page setup, for data dir.
procedure InitializeWizard; 
var
  myLocalAppData: String;
begin
  DataDirPage := CreateInputDirPage(
    wpSelectDir,
    '{#MyLongAppName} Data Directory', 
    '',
    'Please select a directory to install {#MyShortAppName} data to.',
    False, 
    '{#MyShortAppName}'
  );
  DataDirPage.Add('');

  DataDirPage.Values[0] := GetIniString('{#MyShortAppName}', '{#INI_DataDirKey}', GetDefaultDataDirectory(), GetIniFilename());
end;

function DataDirExists(): Boolean;
begin
  { Find out if data dir already exists }
  Result := DirExists(GetDataDir(''));
end;
