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

>!q::Return
>!w::Return
>!e::Return
>!r::Return
>!t::Return
>!y::Return
>!p::Return
>!d::Return
>!f::Return
>!h::Return
>!j::Return
>!k::Return
>!l::Return
>!z::Return
>!x::Return
>!v::Return
>!b::Return
>!n::Return
>!m::Return


#F9::Media_Play_Pause
#F10::Volume_Mute
#F11::SoundSet -3
#F12::SoundSet +3

RAlt::Return
F1::Return

