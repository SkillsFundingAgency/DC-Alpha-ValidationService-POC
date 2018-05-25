Write-Host "Working Directory: $((Get-Item -Path '.\' -Verbose).FullName)"

# Regex to find references in .csproj file 
$privateSettings = "(?i)include=`"PrivateSettings`.config`"(?-i)"
$privateConnectionStrings = "(?i)include=`"PrivateConnectionStrings`.config`"(?-i)"

$privateSettingsXml = "(?i)include=`"PrivateSettings`.xml`"(?-i)"
$privateConnectionStringsXml = "(?i)include=`"PrivateConnectionStrings`.xml`"(?-i)"

$FileContentXml = '<?xml version="1.0" encoding="utf-8"?>'


# Get .csproj files recursively
$filesArr = Get-ChildItem -Filter *.csproj -Recurse -ErrorAction SilentlyContinue -Force

# Iterate each found .csproj file
For ($i=0; $i -lt $filesArr.Length; $i++) {
    Write-Host "Analysing Project: $($filesArr[$i].FullName)"

    $privateSettingsFound = 0
    $privateConnectionStringsFound = 0

    $privateSettingsFoundConfig = 0
    $privateConnectionStringsFoundConfig = 0
    $privateSettingsFoundXml = 0
    $privateConnectionStringsFoundXml = 0
	
    # Iterate each line in the .csproj file looking for included secrets
    foreach($line in Get-Content $filesArr[$i].FullName) {
        if ($privateSettingsFound -eq 0 -and $line -cmatch $privateSettings)
        {
            $privateSettingsFound = 1
			$privateSettingsFoundConfig = 1
        }
        if ($privateSettingsFoundXML -eq 0 -and $line -cmatch $privateSettingsXml)
        {
            $privateSettingsFound = 1
			$privateSettingsFoundXML = 1
        }
        if ($privateConnectionStringsFound -eq 0 -and $line -cmatch $privateConnectionStrings) {
            $privateConnectionStringsFound = 1
			$privateConnectionStringsFoundConfig = 1
        }
		if ($privateConnectionStringsFoundXml -eq 0 -and $line -cmatch $privateConnectionStringsXml) {
            $privateConnectionStringsFound = 1
			$privateConnectionStringsFoundXml = 1
        }		
        if ($privateSettingsFound -eq 1 -and $privateConnectionStringsFound -eq 1) {
            # Optimisation to quit early if we have found both references early in the .csproj file
            break
        }
    }

	## config ##	
    # If we need to create a privatesettings.config file then check it doesn't exist, and if not create
    if ($privateSettingsFoundConfig -eq 1) {
        $fileToCreate = "$($filesArr[$i].DirectoryName)\PrivateSettings.config"
        if (-not(Test-Path $fileToCreate)) {
            Write-Host "Creating: $($fileToCreate)"
            #New-Item $fileToCreate -type file
            Out-File -FilePath $fileToCreate -InputObject $FileContentXml -Force
        }
    }

    # If we need to create a privateconnectionstrings.config file then check it doesn't exist, and if not create
    if ($privateConnectionStringsFoundConfig -eq 1) {
        $fileToCreate = "$($filesArr[$i].DirectoryName)\PrivateConnectionStrings.config"
        if (-not(Test-Path $fileToCreate)) {
            Write-Host "Creating: $($fileToCreate)"
            #New-Item $fileToCreate -type file
            Out-File -FilePath $fileToCreate -InputObject $FileContentXml -Force
        }
    }    
	
	## XML ##	
	# If we need to create a privatesettings.xml file then check it doesn't exist, and if not create
    if ($privateSettingsFoundXML -eq 1) {
        $fileToCreate = "$($filesArr[$i].DirectoryName)\PrivateSettings.xml"
        if (-not(Test-Path $fileToCreate)) {
            Write-Host "Creating: $($fileToCreate)"
            #New-Item $fileToCreate -type file
            Out-File -FilePath $fileToCreate -InputObject $FileContentXml -Force
        }
    }

    # If we need to create a privateconnectionstrings.xml file then check it doesn't exist, and if not create
    if ($privateConnectionStringsFoundXML -eq 1) {
        $fileToCreate = "$($filesArr[$i].DirectoryName)\PrivateConnectionStrings.xml"
        if (-not(Test-Path $fileToCreate)) {
            Write-Host "Creating: $($fileToCreate)"
            #New-Item $fileToCreate -type file
            Out-File -FilePath $fileToCreate -InputObject $FileContentXml -Force
        }
    }    
}

