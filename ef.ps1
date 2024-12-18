$modules = @('MyAModule','MyBModule')
$baseNameSpace = "My.ModularMonolith"

function Show-Menu-Select-Context
{
	Write-Host ""
	Write-Host ...................................................
	Write-Host PRESS 1 or 2 to select your context, or 3 to EXIT.
	Write-Host ...................................................
    Write-Host ""
	Write-Host "0 - EXIT"     
	Write-Host "A - Update database for all Context"
    for ($i = 0; $i -lt $modules.Length; $i++) {
        Write-Host "$($i + 1) - $($modules[$i])Context"
    }
	Write-Host ""
}

function Show-Menu-Select-Operation
{
	Write-Host ""
	Write-Host ...................................................................
	Write-Host PRESS 1, 2 or 3 to select your operation on $context, or 4 to Menu.
	Write-Host ...................................................................
	Write-Host ""
	Write-Host "1 - Add Migration on $context"
	Write-Host "2 - Update database on $context"
	Write-Host "3 - Remove last migration on $context"
	Write-Host "4 - Update database on $context to a specific migration"
	Write-Host "5 - Menu"
	Write-Host "6 - EXIT"
	Write-Host ""

	$operation = Read-Host "Please make a selection"
	switch ($operation)
	{
		'1' { 
			Add-Migration
		}
		'2' { 
			Update-Database
		}
		'3' { 
			Remove-Last-Migration
		}
		'4' { 
			Update-Database-2-Migration
		}
		'5' { 
			Show-Menu-Select-Context
		}
		'6' { 
			exit 
		}
	}
}

function Add-Migration
{
	write-host "dotnet ef migrations add $name --context $context -p src/$baseNameSpace.$project.Infrastructure/$baseNameSpace.$project.Infrastructure.csproj -s src/$baseNameSpace.Api/$baseNameSpace.Api.csproj -o Data/Migrations"
	$name = Read-Host “Give a name to the migration then press ENTER”
	dotnet ef migrations add $name --context $context -p src/$baseNameSpace.$project.Infrastructure/$baseNameSpace.$project.Infrastructure.csproj -s src/$baseNameSpace.Api/$baseNameSpace.Api.csproj -o Data/Migrations
}

function Update-Database
{
	write-host "dotnet ef database update --context $context -p src/$baseNameSpace.$project.Infrastructure/$baseNameSpace.$project.Infrastructure.csproj -s src/$baseNameSpace.Api/$baseNameSpace.Api.csproj"
	dotnet ef database update --context $context -p src/$baseNameSpace.$project.Infrastructure/$baseNameSpace.$project.Infrastructure.csproj -s src/$baseNameSpace.Api/$baseNameSpace.Api.csproj
}

function Remove-Last-Migration
{
	write-host "dotnet ef migrations remove --context $context -p src/$baseNameSpace.$project.Infrastructure/$baseNameSpace.$project.Infrastructure.csproj -s src/$baseNameSpace.Api/$baseNameSpace.Api.csproj"
	dotnet ef migrations remove --context $context -p src/$baseNameSpace.$project.Infrastructure/$baseNameSpace.$project.Infrastructure.csproj -s src/$baseNameSpace.Api/$baseNameSpace.Api.csproj
}

function Update-Database-2-Migration
{
	$migration = Read-Host “Migration”
	write-host "dotnet ef database update --context $context -p src/$baseNameSpace.$project.Infrastructure/$baseNameSpace.$project.Infrastructure.csproj -s src/$baseNameSpace.Api/$baseNameSpace.Api.csproj $migration"
	dotnet ef database update --context $context -p src/$baseNameSpace.$project.Infrastructure/$baseNameSpace.$project.Infrastructure.csproj -s src/$baseNameSpace.Api/$baseNameSpace.Api.csproj $migration
}

Clear-Host
Write-Host "                _/\__                 "       
Write-Host "          ---==/    \\                "
Write-Host "     ___  ___  |.    \|\              "
Write-Host "    | __|| __| |     \\\              "   
Write-Host "    | _| | _ |  \_/|  //|\\           " 
Write-Host "    |___||_|       /   \\\/\\         "

do
{
	if($IsWindows)
	{	
    	set-variable ASPNETCORE_ENVIRONMENT=Local
	}
	elseIf($IsMacOS)
	{
    	launchctl setenv ASPNETCORE_ENVIRONMENT Local
	}
	else
	{
    	Write-Warning "OS not supported : Some environment variables may not be set"
	}
	Show-Menu-Select-Context
    $selectionContext = Read-Host "Please make a selection"
	$context = ""

    if ($selectionContext -eq '0') {
        return
    } elseif ($selectionContext -gt 0 -and $selectionContext -le $modules.Length) {
        $context = "$($modules[$selectionContext - 1])Context"
        $project = $modules[$selectionContext - 1]
        Show-Menu-Select-Operation
    } elseif ($selectionContext -eq 'A') {
		for ($i = 0; $i -lt $modules.Length; $i++) {			
			$context = "$($modules[$i])Context"
			$project = $modules[$i]
			Update-Database
		}
    }else{
		Write-Host "Invalid selection. Please try again."
	}
}
until ($selectionContext -eq '0')