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

#+E::
Run, c:\Users\nirx\Documents
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

>^u::
Send, ü
Return

>^+u::
Send, Ü
Return

>^g::
Send, ğ
Return

>^+g::
Send, Ğ
Return

>^s::
Send, ş
Return

>^+s::
Send, Ş
Return

>^i::
Send, ı
Return

>^+i::
Send, İ
Return

>^c::
Send, ç
Return

>^+c:: 
Send, Ç
Return

>^o::
Send, ö
Return

>^+o::
Send, Ö
Return

>^a::
Send, ä
Return

>^+a::
Send, Ä
Return

#F11::
SoundSet -3
Return

#F12::
SoundSet +3
Return


F1::
Return
