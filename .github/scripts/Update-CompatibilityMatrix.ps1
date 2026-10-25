param(
    [Parameter(Mandatory = $true)]
    [string]$VersionPrefix,

    [Parameter(Mandatory = $true)]
    [string]$SeleniumVersion,

    [string]$ReadmePath = "README.md"
)

$ErrorActionPreference = "Stop"

$resolvedPath = Resolve-Path $ReadmePath
$content = [System.IO.File]::ReadAllText($resolvedPath, [System.Text.Encoding]::UTF8)

$pattern = '(?m)(^\|Appium \.NET Client\|[^\r\n]*\r?\n\|[-|]+\|\r?\n)(\|`([^`]+)`\s*\|`([^`]+)`\s*\|([^\r\n]*))'
$match = [regex]::Match($content, $pattern)
if (-not $match.Success) {
    throw "Could not find Compatibility Matrix table in $ReadmePath"
}

$headerBlock = $match.Groups[1].Value
$topRow = $match.Groups[2].Value
$topAppiumVersion = $match.Groups[3].Value
$topSeleniumVersion = $match.Groups[4].Value
$dotnetColumn = $match.Groups[5].Value

if ([version]$VersionPrefix -lt [version]$topAppiumVersion) {
    Write-Host "Release version $VersionPrefix is older than current top matrix entry $topAppiumVersion; skipping update."
    return $false
}

$newRow = "|``$VersionPrefix`` |``$SeleniumVersion`` |$dotnetColumn"
if ($topRow -eq $newRow) {
    Write-Host "Compatibility matrix already up to date for $VersionPrefix (Selenium.WebDriver $SeleniumVersion)."
    return $false
}

$newline = if ($headerBlock.EndsWith("`r`n")) { "`r`n" } else { "`n" }
if ($topSeleniumVersion -eq $SeleniumVersion) {
    $replacement = "${headerBlock}${newRow}"
} else {
    $replacement = "${headerBlock}${newRow}${newline}${topRow}"
}

$updated = $content.Substring(0, $match.Index) + $replacement + $content.Substring($match.Index + $match.Length)
[System.IO.File]::WriteAllText($resolvedPath, $updated, [System.Text.UTF8Encoding]::new($false))
Write-Host "Updated $ReadmePath compatibility matrix to $VersionPrefix (Selenium.WebDriver $SeleniumVersion)."
return $true
