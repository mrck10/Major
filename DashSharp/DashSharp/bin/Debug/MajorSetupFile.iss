; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Major"
#define MyAppVersion "0.0"
#define MyAppPublisher "Mrck10"
#define MyAppURL "https://github.com/mrck10/Major"
#define MyAppExeName "Major.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{DB83D25E-FA26-436C-80FB-29328E5B41A2}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=E:\OneDrive\Major\DashSharp\DashSharp\bin\Debug\license.txt
OutputDir=E:\Desktop
OutputBaseFilename=Major Setup File
SetupIconFile=E:\Downloads\majoricon_OvS_icon.ico
Compression=lzma
SolidCompression=yes
ChangesAssociations = yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "E:\OneDrive\Major\DashSharp\DashSharp\bin\Debug\Major.exe"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Registry]

Root: HKCR; Subkey: ".major";                             ValueData: "Major";          Flags: uninsdeletevalue; ValueType: string;  ValueName: ""
Root: HKCR; Subkey: "Major";                     ValueData: "Program Major";  Flags: uninsdeletekey;   ValueType: string;  ValueName: ""
Root: HKCR; Subkey: "Major\DefaultIcon";             ValueData: "{app}\Major.exe,0";               ValueType: string;  ValueName: ""
Root: HKCR; Subkey: "Major\shell\open\command";  ValueData: """{app}\Major.exe"" ""%1""";  ValueType: string;  ValueName: ""
