~\miniconda3\shell\condabin\conda-hook.ps1

Import-Module posh-git
# Import-Module DockerCompletion

Set-Alias which get-command
Set-Alias -Name cd -Value pushd -Option AllScope
Set-Alias -Name zz -Value popd
Set-Alias -Name open start
Set-Alias -Name grep Select-String
Set-Alias -Name clip Set-Clipboard

Set-PSReadlineKeyHandler -Key Tab -Function MenuComplete
Set-PSReadLineOption -Colors @{ "Default"="DarkGray" }
$MaximumHistoryCount = 30000

$ws = (Get-Item "~\ws").FullName

# required
$builds = "$ws\builds" # Folder to keep all the build folders
$build = "$ws\build"   # This will be created as a softlink to $builds/<branch_name>
$nux = "$ws\nirxtools"
$nirsite = "$ws\nirsite"
$scout = "$ws\scout"

# for convenience
$nsp3 = "$nux\NSP3_APP"
$aurora = "$nux\nux\python\nux"
$nuxDeviceSetup = "$nux\nux\resources\scripts\device_setup"
$nuxInstaller = "$nux\nux\installer"
$nirsite_py = "$nirsite\python\nirsite"
$nirstar = "C:\NIRx"

if ($IsWindows) {
    $nirxDocs = "$home\Documents\NIRx"
    $nuxData = "$nirxDocs\Data"
    $nuxConfigs = "$nirxDocs\Configurations"
    $nuxLog = "$nuxdata\Logs\log.txt"
    $nirxLocal = "$home\AppData\Local\NIRx\"
    $nirxSetups = "$home\AppData\Local\Programs\NIRx"
    $nirstarData = "C:\NIRx\Data"
}
elseif ($IsLinux) {
    # TODO
}

# others
$bat = "$ws\batches"
$trash = "$ws\trash" # used to write temporary files
$repos = "D:\repos"
$rest = "$ws\rest"
$docs = "$ws\docs"
$jupiter = "\\jupiter\share\tmp\ozan"
$tigris = "$ws\\tigris"

function reload {
    Import-Module -Name $ws/rest/batches/nirx.psm1 -Force -Global
    Import-Module -Name $ws/rest/batches/utility.psm1 -Force -Global
    Import-Module -Name $ws/rest/batches/PSVirtualBox.psm1 -Force -Global
    Import-Module -Name $ws/scout/batches/scout.psm1 -Force -Global
}

reload

Set-Alias -Name n nux
Set-Alias -Name nsp nsp2

$GITHUB_PAT = "67e0045f2e00cd4d1018f8fcb541e76611f7ba65"

$HYPERION_IP = ""  # fill this in case hyperion can't be found by name resolution
$NSP2_PASS = "Wu7.10555B"
$NSP2_IP_LIST = @("10.10.20.1", "192.168.1.23")
$WIFI_NETWORKS = @("o2-SchoenhausSF5.OGli", "SUPERONLINE-WiFi_2503", "NIRx")

$PYCHARM = "C:\Program Files\JetBrains\PyCharm Community Edition 2021.3\bin\pycharm64.exe"
$WINMERGE = "C:\Program Files (x86)\WinMerge\WinMergeU.exe"
$WINDIRSTAT = "C:\Program Files (x86)\WinDirStat\windirstat.exe"
$QDESIGNER = "D:\Qt\5.15.2\mingw81_64\bin\designer.exe"

$env:AURORA_STARTUP_CMD = "C:\Users\ozan\ws\rest\hyperscan_aurora.bat"
$env:AURORA_STARTUP_DIR = $aurora
$env:SCOUT_ENABLE_CPP_STACK_TRACE=1
