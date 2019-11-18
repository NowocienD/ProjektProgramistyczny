﻿dotnet restore .\GradeBook.API\GradeBook.API.csproj

$scriptDirBuild = Split-Path -Path $MyInvocation.MyCommand.Definition

If (Test-Path $scriptDirBuild\dist) {
    Remove-Item `
        -Path $scriptDirBuild\dist `
        -Force `
        -Recurse
}
dotnet publish .\GradeBook.API\GradeBook.API.csproj -o .\dist -v quiet -c Release -r win10-x64


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