!define PRODUCT_NAME "ConnectUO"
!define PRODUCT_VERSION "2.0"
!define PRODUCT_PUBLISHER "The RunUO Team"
!define PRODUCT_WEB_SITE "http://www.connectuo.com"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\ConnectUO.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define PRODUCT_STARTMENU_REGVAL "NSIS:StartMenuDir"

!include "MUI.nsh"
!include "Registry.nsh"
!include "LogicLib.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "cuoicon.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "license.txt"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Start menu page
var ICONS_GROUP
!define MUI_STARTMENUPAGE_NODISABLE
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "ConnectUO 2.0"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${PRODUCT_STARTMENU_REGVAL}"
!insertmacro MUI_PAGE_STARTMENU Application $ICONS_GROUP
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\ConnectUO.exe"
!define MUI_FINISHPAGE_SHOWREADME "$INSTDIR\readme.txt"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "ConnectUOLatest_x86.exe"
InstallDir "$PROGRAMFILES\ConnectUO 2.0"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Function IsNetfx40Installed
;Check is Net 4.0 is install
;Push 0 for true, Pop -1 for false
${registry::Read} "HKEY_LOCAL_MACHINE\Software\Microsoft\NET Framework Setup\NDP\v4.0\Full" "Install" $R0 $R1
${If} $R1 == ""
Push -1
${Else}
Push 0
${EndIf}
FunctionEnd

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer  
;Icon
  File "cuoicon.ico"
;Exe
  File "ConnectUO.exe"
  File "Update.exe"
;Dlls
  File "Loader.dll"
  File "ConnectUO.Framework.dll"
  File "Fds.PresentaionFramework.dll"
  File "EncPatcher.dll"
  File "SevenZipSharp.dll"
  File "7z.dll"
;Configs
  File "ConnectUO.exe.config"
  File "Update.exe.config"
;Docs
  File "docs\changelog.txt"
  File "docs\license.txt"
  File "docs\readme.txt"

; Shortcuts
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  CreateDirectory "$SMPROGRAMS\$ICONS_GROUP"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\ConnectUO 3.0.lnk" "$INSTDIR\ConnectUO.exe"
  CreateShortCut "$DESKTOP\ConnectUO 3.0.lnk" "$INSTDIR\ConnectUO.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -AdditionalIcons
  SetOutPath $INSTDIR
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Website.lnk" "$INSTDIR\${PRODUCT_NAME}.url"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk" "$INSTDIR\uninst.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\ConnectUO.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\ConnectUO.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Function .onInit
Call IsNetfx40Installed
Pop $R1
${If} $R1 == -1
  MessageBox MB_OK "ConnectUO 3.0 requires .Net 4.0 to be installed.  Please install .Net 4.0 and retry."
  Abort
${EndIf}
FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  !insertmacro MUI_STARTMENU_GETFOLDER "Application" $ICONS_GROUP
  Delete "$INSTDIR\${PRODUCT_NAME}.url"
  Delete "$INSTDIR\uninst.exe"
;Icon
  File "cuoicon.ico"
;Exe
  File "ConnectUO.exe"
  File "Update.exe"
;Dlls
  File "Loader.dll"
  File "ConnectUO.Framework.dll"
  File "Fds.PresentaionFramework.dll"
  File "EncPatcher.dll"
  File "SevenZipSharp.dll"
  File "7z.dll"
;Configs
  File "ConnectUO.exe.config"
  File "Update.exe.config"
;Docs
  File "docs\changelog.txt"
  File "docs\license.txt"
  File "docs\readme.txt"

  Delete "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\Website.lnk"
  Delete "$DESKTOP\ConnectUO 3.0.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\ConnectUO 3.0.lnk"

  RMDir "$SMPROGRAMS\$ICONS_GROUP"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd