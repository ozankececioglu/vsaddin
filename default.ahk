#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

#+W::
Run, c:\ws
WinActivate
Return

#+Q::
Run, c:\Users\nirx\Downloads
WinActivate
Return

#+A::
Run D:\
WinActivate
Return

#+D::
Run, c:\Users\nirx\Desktop
WinActivate
Return

#+H::
Run, c:\Users\nirx
WinActivate
Return

!]::
Send, ü
Return

!+]::
Send, Ü
Return

![::
Send, ğ
Return

!+[::
Send, Ğ
Return

!;::
Send, ş
Return

!+;::
Send, Ş
Return

!'::
Send, ı
Return

!+'::
Send, İ
Return

!.::
Send, ç
Return

!+.:: 
Send, Ç
Return

!,::
Send, ö
Return

!+,::
Send, Ö
Return

!a::
Send, ä
Return

!+a::
Send, Ä
Return










