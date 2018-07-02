 param (
        [string] $ProjectPath = $(throw "ProjectPath is a required parameter"),
        [string] $ConfigurationName = $(throw "ConfigurationName is a required parameter")
    )

function Add-XmlNode {
    param (
        [xml] $doc,
        [string] $xpath = $(throw "xpath is a required parameter"),
        [string] $nodeName = $(throw "nodeName is a required parameter"),
        [string] $nodeValue = $(throw "nodeValue is a required parameter")
    )

    $nodes = $doc.SelectNodes($xpath)
    $count = $nodes.Count

    #if ($count -eq 0) { Write-Host "Found no such nodes with path '$xpath'" }

    foreach ($node in $nodes) {
        if ($node -ne $null) {
            if ($node.NodeType -eq "Element")
            {
                $node.InnerXml = "{0}<{1}>{2}</{1}>" -f $node.InnerXml, $nodeName, $nodeValue
            }
        }
    }
}
function Edit-XmlNodes {
    param (
        [xml] $doc,
        [string] $xpath = $(throw "xpath is a required parameter"),
        [string] $value = $(throw "value is a required parameter")
    )

    $nodes = $doc.SelectNodes($xpath)
    $count = $nodes.Count

    #Write-Host "Found $count nodes with path '$xpath'"

    foreach ($node in $nodes) {
        if ($node -ne $null) {
            if ($node.NodeType -eq "Element")
            {
                $node.InnerXml = $value
            }
            else
            {
                $node.Value = $value
            }
        }
    }
}
function Get-XmlNode-Value {
    param (
        [xml] $doc,
        [string] $xpath = $(throw "xpath is a required parameter")
    )

    $nodes = $doc.SelectNodes($xpath)
    $count = $nodes.Count

    #Write-Host "Found $count nodes with path '$xpath'"

    foreach ($node in $nodes) {
        if ($node -ne $null) {
            if ($node.NodeType -eq "Element")
            {
                return $node.InnerXml
            }
            else
            {
                return $node.Value
            }
        }
    }
}
function Show-Version{
    Write-Host ("[Update-Version] AssemblyVersion: {0} => {1}" -f $oldAssemblyVer, $assemblyVer)
    Write-Host ("[Update-Version] FileVersion    : {0} => {1}" -f $oldFileVer, $fileVer)
    Write-Host ("[Update-Version] Version        : {0} => {1}" -f $oldPkgVer, $pkgVer)
}
function Show-Version-And-Exit {
    Show-Version
    Exit-Script
}
function Exit-Script {
    Write-Host ("[Update-Version] Exits!")
    Break
}
function Set-Should-Yes{
    [Environment]::SetEnvironmentVariable("Should","Yes",[System.EnvironmentVariableTarget]::User)
    Write-Host "[Update-Version] Set '<Should>' = Yes"
}
function Set-Should-No{
    [Environment]::SetEnvironmentVariable("Should","No",[System.EnvironmentVariableTarget]::User)
    Write-Host "[Update-Version] Set '<Should>' = No"
}
function Get-Should{
    return [Environment]::GetEnvironmentVariable("Should",[System.EnvironmentVariableTarget]::User)
}
    Write-Host ("[Update-Version] Starts...")

    #get .csproj file
    try{
        $xml = [xml](Get-Content $ProjectPath -ErrorAction Stop)
    }
    catch{
        Write-Host ("[Update-Version] Exit! Not Found Project File at: '{0}'" -f $ProjectPath) -ForegroundColor Red
        break
    }

    #get FileVersion
    [string]$fileVer = Get-XmlNode-Value -doc $xml -xpath "/Project/PropertyGroup/FileVersion"
    if ($fileVer -eq $null -or $fileVer -eq ""){
        Write-Host "[Update-Version] Add '<FileVersion>'"
        Add-XmlNode -doc $xml -xpath "/Project/PropertyGroup" -nodeName "FileVersion" -nodeValue ""
        $fileVer =  "0.1.0.0"
        $oldFileVer = "<null>"
    }
    else{
        $oldFileVer = $fileVer
    }

    #split versions
    $vers = $fileVer.Split('.')
    [int]$majorVer = $vers[0]
    [int]$minorVer = $vers[1]
    [int]$fixVer = $vers[2]
    [int]$buildVer = $vers[3]

    #get AssemblyVersion or add default one
    [string]$assemblyVer = Get-XmlNode-Value -doc $xml -xpath "/Project/PropertyGroup/AssemblyVersion"
    if ($assemblyVer -eq $null -or $assemblyVer -eq ""){
        Write-Host "[Update-Version] Add '<AssemblyVersion>'"
        Add-XmlNode -doc $xml -xpath "/Project/PropertyGroup" -nodeName "AssemblyVersion" -nodeValue ""
        $oldAssemblyVer = "<null>"
    }
    else{
        $oldAssemblyVer = $assemblyVer
    }

    #get PackageVersion or add default one
    [string]$pkgVer = Get-XmlNode-Value -doc $xml -xpath "/Project/PropertyGroup/Version"
    if ($pkgVer -eq $null -or $pkgVer -eq ""){
        $pkgVer =  "{0}.{1}.{2}-alpha-build{3:00000}" -f $majorVer, $minorVer, $fixVer, $buildVer
        Add-XmlNode -doc $xml -xpath "/Project/PropertyGroup" -nodeName "Version" -nodeValue $pkgVer
        $oldPkgVer = "<null>"
    }
    else{
        $oldPkgVer = $pkgVer
    }

    #get preRelease and preRelease version
    $pkgVers = $pkgVer.Replace("-",".").Split('.')
    if ($pkgVers.Length -gt 3){
        [string]$preRelease = $pkgVers[3]
        [string]$preReleaseVerString = $preRelease.Replace("alpha","").Replace("beta","").Replace("rc","")
        if ($preReleaseVerString -eq ""){
            [int]$preReleaseVer = 0
        }
        else{
            [int]$preReleaseVer = $preReleaseVerString
        }
        if ($preRelease.Contains("rc")) {$preRelease = "rc"}
        if ($preRelease.Contains("beta")) {$preRelease = "beta"}
        if ($preRelease.Contains("alpha")) {$preRelease = "alpha"}
    }
    else
    {
        [string]$preRelease = ""
    }

    #set versions by Configurations
    if ($ConfigurationName -eq "Debug"){
        $buildVer++
    }
    elseif ($ConfigurationName -eq "NewMajor"){
        if ($oldPkgVer -ne "<null>") { $majorVer++ }
        $minorVer = 0
        $fixVer = 0
        $preRelease = "alpha"
        $preReleaseVer = 0
        $buildVer = 1
    }
    elseif ($ConfigurationName -eq "NewMinor"){
        if ($oldPkgVer -ne "<null>") { $minorVer++ }
        $fixVer = 0
        $preRelease = "alpha"
        $preReleaseVer = 0
        $buildVer = 1
    }
    elseif ($ConfigurationName -eq "NewFix"){
        $fixVer++
        $preRelease = "alpha"
        $preReleaseVer = 0
        $buildVer = 1
    } 
    elseif ($ConfigurationName -eq "NewAlpha"){
        if ($preRelease -eq "alpha"){
            $preReleaseVer++
        }
        else{
            Write-Host ("[Update-Version] Cannot create an alpha from: {0}. Use NewFix/NewMinor/NewMajor to create an alpha version!" -f $pkgVer) -f Red
            Show-Version-And-Exit
        }
        $buildVer++
    }
    elseif ($ConfigurationName -eq "NewBeta"){
        if ($preRelease -eq "alpha"){
            $preRelease = "beta"
            $preReleaseVer = 0
        }
        elseif ($preRelease -eq "beta"){
            $preReleaseVer++
        }
        else{
            Write-Host ("[Update-Version] Cannot create a Beta version from: {0}. Only can create Beta from Alpha!" -f $pkgVer) -f Red
            Show-Version-And-Exit
        } 
        $buildVer++
    } 
    elseif ($ConfigurationName -eq "NewRC" -or $ConfigurationName -eq "NewCandidate"){
        if ($preRelease -eq "alpha" -or $preRelease -eq "beta" ){
            $preRelease = "rc"
            $preReleaseVer = 0
        }if ($preRelease -eq "rc"){
            $preReleaseVer++
        }
        else{
            Write-Host ("[Update-Version] Cannot create a RC version from: {0}. Only can create RC from Alpha/Beta!" -f $pkgVer) -f Red
            Show-Version-And-Exit
        } 
        $buildVer++
    }
    elseif ($ConfigurationName -eq "Release"){
        $preRelease = ""
        $buildVer++
    }
    else{
        Write-Host ("[Update-Version] Exits! Unknown configuration: '{0}'. Should be Debug/Release/NewFix/NewMinor/NewMajor/NewAlpha/NewBeta/NewRC" -f $ConfigurationName) -f Red
        Break
    }

    #update FileVersion
    $fileVer = "{0}.{1}.{2}.{3}" -f $majorVer, $minorVer, $fixVer, $buildVer
    Edit-XmlNodes -doc $xml -xpath "/Project/PropertyGroup/FileVersion" -value $fileVer

    #update AssemblyVersion
    $assemblyVer = "{0}.{1}.{2}" -f $majorVer, $minorVer, $fixVer
    Edit-XmlNodes -doc $xml -xpath "/Project/PropertyGroup/AssemblyVersion" -value $assemblyVer

    #update PackageVersions  $pkgVer =  "{0}.{1}.{2}-{3}-build{4}" or "{0}.{1}.{2}-{3}{4}-build{5}"
    $pkgVer =  "{0}.{1}.{2}" -f $majorVer, $minorVer, $fixVer
    if ($preRelease.Trim() -ne ""){
        $pkgVer = $pkgVer + "-" + $preRelease
        if($preReleaseVer -ne 0){
            $pkgVer = $pkgVer + $preReleaseVer
        }
        $pkgVer =  "{0}-build{1:00000}" -f $pkgVer, $buildVer
    }
    Edit-XmlNodes -doc $xml -xpath "/Project/PropertyGroup/Version" -value $pkgVer

    Show-Version
    
    #Save
    $xml.save($ProjectPath)
    Write-Host ("[Update-Version] Project Updated: '{0}'" -f $ProjectPath)

    Exit-Script