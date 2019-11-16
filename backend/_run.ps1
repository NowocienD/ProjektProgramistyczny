
$scriptDirRun = Join-Path `
    -Path (Split-Path -Path $MyInvocation.MyCommand.Definition) `
    -ChildPath dist

Push-Location $scriptDirRun

try {
    & $scriptDirRun\GradeBook.API.exe
}
finally {
    Pop-Location
}