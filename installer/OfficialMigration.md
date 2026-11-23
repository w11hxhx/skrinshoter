# Official-to-fork installation migration

The installer detects the official application's own uninstall keys, not the
fork's `{7E133262-6DE0-4D2E-9157-AC1CD74B4C02}_is1` registration. Before invoking a
registered official uninstaller, it copies the current user's complete
`%LOCALAPPDATA%\Skrinshoter\settings.json` to a persistent sibling directory,
`%LOCALAPPDATA%\Skrinshoter-fork-migration`. It restores the original bytes after
uninstall and before the fork can start. A failed backup prevents uninstall;
failed/cancelled uninstall prevents installation. Backups remain available for
recovery if setup is interrupted.

System-wide uninstall registrations use the official uninstaller's UAC elevation;
the fork setup stays in per-user mode so settings belong to the initiating user.
Only the current user's settings are backed up. Other users' profiles are not
read or migrated by this installer.

## Shipped-artifact observations

Downloaded for static inspection from `https://skrinshoter.ru/SkrinshoterSetup.exe`:

- Product version: `4.88.2`.
- SHA-256: `980cc78d97f5c9dd5f861a97a0d0a3afe5132bc82a1cd1b68702f56629397067`.
- REA PE resource evidence:
  `ev_be7b95465a19e7c339594685aa94582651155ca5d96770ac39c4904d1f9162cd`.
- REA managed assembly identity evidence:
  `ev_f59602593e9e556f2e349309d1044167ac9db1e5af617d9a98e52e7158605399`.
- `innounp 2.71.4` extracted the embedded Inno 6.3.0 installer script without
  executing the vendor installer. It declares `AppId=Скриншотер`,
  `UninstallDisplayName=Скриншотер`, and `{commonpf}\Skrinshoter` as the default
  install directory. Its compiled script contains the settings path
  `{localappdata}\Skrinshoter\settings.json` and settings-deletion UI strings.
- The Inno registration is consequently `Скриншотер_is1`. The unsuffixed
  `Скриншотер` registration is also recognized for legacy NSIS installations.

These are static observations. The real vendor installer/uninstaller was not run
on the development computer. Full managed-member analysis exceeded REA's metadata
representation budget (`ev_4c3ee5cbd24c0de34c64aea1addc3778c5c2d7142b812fe20bfedf5a4e1e5bc0`);
it is not evidence that all historical configuration formats are supported.

## Verification

`tests/Verify-InstallerMigration.ps1` compiles a test-only variant of the actual
installer, with its own registry namespace, profile, payload and backup folder.
It invokes an inert fake uninstaller that deliberately deletes the fixture's
settings. It checks:

- Inno and legacy NSIS registration selection and removal;
- exact restoration of the original settings bytes and persistent backup copies;
- no uninstall when backup creation fails;
- aborted setup and restored settings on uninstall failure or cancellation;
- ordinary fork upgrades never invoke the official uninstaller.

`tests/Verify-ScreenshotSettings.ps1` separately deserializes the official JSON
field names through the production converter and normalizer, checking all six
non-default screenshot/recording shortcuts. These tests do not access real user
settings or remove installed applications. They do not replace a full end-to-end
vendor uninstall test on a disposable Windows machine, or verify historical
formats older than the compatible JSON settings schema.
