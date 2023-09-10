# Regedit

> 可能用到的注册表键

```bash
HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\原神
HKEY_CURRENT_USER\Software\miHoYo\原神
```

```bash
C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup
C:\Users\ema\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup
```

```bash
shell:startup
shell:appsfolder
```

> 恢复原神注册表
>
> ※需要GBK编码

```ini
Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\原神]
"UUID"="8ae919d528ab45e39487f7b106b9ab451639403361704"
"DisplayIcon"="D:\\Program Files\\Genshin Impact\\launcher.exe"
"DisplayName"="原神"
"DisplayVersion"="2.23.0.0"
"Publisher"="miHoYo Co.,Ltd"
"UninstallString"="D:\\Program Files\\Genshin Impact\\uninstall.exe"
"InstallPath"="D:\\Program Files\\Genshin Impact"
"ExeName"="launcher.exe"
"URLInfoAbout"="https://ys.mihoyo.com/main/"
"EstimatedSize"=dword:00049535
```

