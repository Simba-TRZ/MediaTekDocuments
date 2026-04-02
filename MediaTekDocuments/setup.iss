[Setup]
AppName=MediaTekDocuments
AppVersion=1.0
DefaultDirName={autopf}\MediaTekDocuments
DefaultGroupName=MediaTekDocuments
OutputDir=C:\Installeur
OutputBaseFilename=Setup_MediaTekDocuments
Compression=lzma
SolidCompression=yes

[Files]
Source: "C:\Users\aziz\MediaTekDocuments\MediaTekDocuments\bin\Release\*"; DestDir: "{app}"; Flags: recursesubdirs

[Icons]
Name: "{group}\MediaTekDocuments"; Filename: "{app}\MediaTekDocuments.exe"
Name: "{commondesktop}\MediaTekDocuments"; Filename: "{app}\MediaTekDocuments.exe"

[Run]
Filename: "{app}\MediaTekDocuments.exe"; Description: "Lancer MediaTekDocuments"; Flags: nowait postinstall skipifsilent