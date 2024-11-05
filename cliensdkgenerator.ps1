Clear-Host
$modules = @('MyAModule', 'MyBModule')
$url = "https://localhost:7226/swagger/v1/swagger.json"
$baseNameSpace = "My.ModularMonolith"

function Show-Menu {
    param (
        [string]$Title = 'Select Module'
    )
    Clear-Host
    Write-Host "================ $Title ================"
    Write-Host "0: Exit"
    for ($i = 0; $i -lt $modules.Length; $i++) {
        Write-Host "$($i + 1) - $($modules[$i])"
    }    
}

function ApiNotRunning{
    param(
        [Parameter(Mandatory=$true)]
        $job
    )
    Stop-Job -Job $job
    Remove-Job -Job $job
    Write-Host "API is not up and running. Exiting..."
    exit
}

Show-Menu
$timeout=40;
$moduleSelection = Read-Host "Please make a selection"
	if ($moduleSelection -eq '0') {
        exit
    } elseif ($moduleSelection -gt 0 -and $moduleSelection -le $modules.Length) {
        $argument = $modules[$moduleSelection - 1]
    } else {
        Write-Host "Invalid selection. Exiting..."
        exit
    }

    $stopwatch = [System.Diagnostics.Stopwatch]::StartNew()

$job = Start-Job -ScriptBlock {
    param($arg)
    pwsh ./start.ps1 -arg $arg
} -ArgumentList $argument

for ($i = 1; $i -le $timeout; $i++) {
    Clear-Host
    Write-Host ("Generating client SDK for "+ $argument + "." * ($i + 2))
    Start-Sleep -Seconds 1
    try {
        $response = Invoke-WebRequest -Uri $url -Method Get -UseBasicParsing
        if ($response.StatusCode -eq 200) {
            $stopwatch.Stop()
            Write-Host "API is up and running in" $stopwatch.Elapsed.TotalSeconds.ToString("N0") "seconds."
            break
        }else{
            if($i -eq $timeout){
                ApiNotRunning -job $job
            }
        }
    } catch {
        if($i -eq $timeout){
            ApiNotRunning -job $job
        }
    }
}

Set-Location ./src/$baseNameSpace.$argument.Client.SDK
pwsh GenerateClient.ps1
Stop-Job -Job $job
Remove-Job -Job $job
Set-Location ../..