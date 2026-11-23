// The official Inno installer uses AppId=Скриншотер (verified in 4.88.2).
// Older NSIS releases registered the same name without the _is1 suffix.
// Never target the fork's own GUID: fork upgrades remain ordinary Inno upgrades.
const
#ifdef MigrationTest
  OfficialUninstallRoot = '{#MigrationRegistryRoot}';
#else
  OfficialUninstallRoot = 'Software\Microsoft\Windows\CurrentVersion\Uninstall';
#endif

var
  MigrationBackup: String;
  MigrationSettings: String;
  MigrationStarted: Boolean;
  MigrationComplete: Boolean;

function MigrationProfileDirectory: String;
begin
#ifdef MigrationTest
  Result := '{#MigrationProfileDirectory}';
#else
  Result := ExpandConstant('{localappdata}\Skrinshoter');
#endif
end;

function OfficialKey(Index: Integer): String;
begin
  case Index of
    0: Result := 'Скриншотер_is1';
    1: Result := 'Скриншотер';
  else
    Result := '';
  end;
end;

function OfficialRoot(Index: Integer): Integer;
begin
  case Index of
    0: Result := HKCU32;
    1: Result := HKCU64;
    2: Result := HKLM32;
  else
    Result := HKLM64;
  end;
end;

function IsOfficialInstallation(Root: Integer; const Key: String): Boolean;
var
  Name: String;
begin
  Result := RegQueryStringValue(Root, Key, 'DisplayName', Name) and
    ((CompareText(Name, 'Скриншотер') = 0) or (CompareText(Name, 'Skrinshoter') = 0));
end;

function HasOfficialInstallation: Boolean;
var
  R, K: Integer;
begin
  Result := False;
  for R := 0 to 3 do
    for K := 0 to 1 do
      if IsOfficialInstallation(OfficialRoot(R), OfficialUninstallRoot + '\' + OfficialKey(K)) then
      begin
        Result := True;
        Exit;
      end;
end;

function BackupMigrationSettings: Boolean;
var
  BackupDirectory, BaseName: String;
  Suffix: Integer;
begin
  Result := True;
  if MigrationStarted then Exit;
  MigrationSettings := AddBackslash(MigrationProfileDirectory) + 'settings.json';
  if FileExists(MigrationSettings) then
  begin
#ifdef MigrationTest
    BackupDirectory := '{#MigrationBackupDirectory}';
#else
    BackupDirectory := ExpandConstant('{localappdata}\Skrinshoter-fork-migration');
#endif
    if not ForceDirectories(BackupDirectory) then
    begin
      Result := False;
      Exit;
    end;
    BaseName := AddBackslash(BackupDirectory) + 'settings-' + GetDateTimeString('yyyymmdd-hhnnss', '-', '-');
    Suffix := 0;
    repeat
      MigrationBackup := BaseName + '-' + IntToStr(Suffix) + '.json';
      Suffix := Suffix + 1;
    until not FileExists(MigrationBackup);
    Result := CopyFile(MigrationSettings, MigrationBackup, True);
    if not Result then
    begin
      MigrationBackup := '';
      Exit;
    end;
    Log('Preserved official settings: ' + MigrationBackup);
  end;
  MigrationStarted := True;
end;

function RestoreMigrationSettings: Boolean;
begin
  Result := True;
  if MigrationBackup = '' then Exit;
  Result := ForceDirectories(ExtractFileDir(MigrationSettings)) and
    CopyFile(MigrationBackup, MigrationSettings, False);
  if Result then Log('Restored original settings and hotkeys.');
end;

function UninstallerExecutable(const Command: String): String;
var
  Value: String;
  EndPosition: Integer;
begin
  Result := '';
  Value := Trim(Command);
  if Value = '' then Exit;
  if Value[1] = '"' then
  begin
    Delete(Value, 1, 1);
    EndPosition := Pos('"', Value);
    if EndPosition = 0 then Exit;
    Result := Copy(Value, 1, EndPosition - 1);
  end
  else
  begin
    EndPosition := Pos('.exe', Lowercase(Value));
    if EndPosition = 0 then Exit;
    Result := Copy(Value, 1, EndPosition + 3);
  end;
  if (ExtractFileDrive(Result) = '') or (CompareText(ExtractFileExt(Result), '.exe') <> 0) then
    Result := '';
end;

function PrepareToInstall(var NeedsRestart: Boolean): String;
var
  R, K, Root, ExitCode: Integer;
  Key, Command, Executable, Parameters, Verb: String;
  Ran, Restored: Boolean;
begin
  Result := '';
  for R := 0 to 3 do
    for K := 0 to 1 do
    begin
      Root := OfficialRoot(R);
      Key := OfficialUninstallRoot + '\' + OfficialKey(K);
      if IsOfficialInstallation(Root, Key) then
      begin
        if not RegQueryStringValue(Root, Key, 'UninstallString', Command) then
        begin
          Result := 'Не найдена команда удаления официального Скриншотера. Удалите его через параметры Windows и повторите установку.';
          Exit;
        end;
        Executable := UninstallerExecutable(Command);
        if (Executable = '') or not FileExists(Executable) then
        begin
          Result := 'Не найден деинсталлятор официального Скриншотера. Восстановите или удалите официальную версию и повторите установку.';
          Exit;
        end;
        if not BackupMigrationSettings then
        begin
          Result := 'Не удалось сохранить настройки и бинды. Удаление официальной версии не начато. Закройте Скриншотер и повторите попытку.';
          Exit;
        end;
        WizardForm.StatusLabel.Caption := 'Удаление официального Скриншотера с сохранением настроек и биндов…';
        if K = 0 then Parameters := '/VERYSILENT /SUPPRESSMSGBOXES /NORESTART'
        else Parameters := '/S';
        Verb := '';
        if R >= 2 then Verb := 'runas';
        Log('Removing registered official installation: ' + Key);
        Ran := ShellExec(Verb, Executable, Parameters, ExtractFileDir(Executable), SW_HIDE, ewWaitUntilTerminated, ExitCode);
        Restored := RestoreMigrationSettings;
        if not Restored then
        begin
          Result := 'Не удалось восстановить настройки. Резервная копия: ' + MigrationBackup + '. Закройте Скриншотер и повторите установку.';
          Exit;
        end;
        if not Ran or (ExitCode <> 0) or RegKeyExists(Root, Key) then
        begin
          if ExitCode = 3010 then NeedsRestart := True;
          Result := 'Удаление официального Скриншотера отменено или не завершено (код ' + IntToStr(ExitCode) + '). Настройки и бинды сохранены. Удалите официальную версию и повторите установку форка.';
          Exit;
        end;
      end;
    end;
end;

function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo,
  MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
begin
  Result := MemoDirInfo + NewLine + MemoGroupInfo + NewLine + MemoTasksInfo;
  if HasOfficialInstallation then
    Result := Result + NewLine + NewLine + 'Официальный Скриншотер будет удалён. Ваши настройки и бинды будут сохранены; резервная копия останется в папке Skrinshoter-fork-migration.';
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    if not RestoreMigrationSettings then
      RaiseException('Не удалось восстановить настройки. Резервная копия: ' + MigrationBackup);
    MigrationComplete := True;
  end;
end;

procedure DeinitializeSetup;
begin
  if MigrationStarted and not MigrationComplete then
    if not RestoreMigrationSettings then
      Log('Settings recovery required; original backup remains at ' + MigrationBackup);
end;
