#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

>!u::Send, ü
>!+u::Send, Ü

>!g::Send, ğ
>!+g::Send, Ğ

>!s::Send, ş
>!+s::Send, Ş
>!>^s::Send, ß

>!i::Send, ı
>!+i::Send, İ

>!c::Send, ç
>!+c:: Send, Ç

>!o::Send, ö
>!+o::Send, Ö

>!a::Send, ä
>!+a::Send, Ä

#F9::Media_Play_Pause
#F10::Volume_Mute
#F11::SoundSet -3
#F12::SoundSet +3

RAlt::Return
F1::Return


#+W::
Run, c:\Users\ozan\ws
WinActivate
Return

#+Q::
Run, c:\Users\ozan\Downloads
WinActivate
Return

#+E::
Run, c:\Users\ozan\Documents
WinActivate
Return

#+A::
Run D:\
WinActivate
Return

#+D::
Run, c:\Users\ozan\Desktop
WinActivate
Return

#+H::
Run, c:\Users\ozan
WinActivate
Return

