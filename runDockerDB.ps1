docker stop sql1
docker rm sql1
docker run -e ACCEPT_EULA=Y -e MSSQL_SA_PASSWORD=Bonjour1234@@!! -p 1433:1433 --name sql1 --hostname sql1 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2022-latest

if($IsWindows)
{
    [Environment]::SetEnvironmentVariable('ConnectionStrings__ModuleAConnection', 'Data Source=localhost;Initial Catalog=ModularMonolith;Persist Security Info=True;User ID=sa;Password=Bonjour1234@@!!;Trust Server Certificate=True', 'Machine')
}
elseIf($IsMacOS)
{
    launchctl setenv ConnectionStrings__ModuleAConnection 'Data Source=localhost;Initial Catalog=ModularMonolith;Persist Security Info=True;User ID=sa;Password=Bonjour1234@@!!;Trust Server Certificate=True'
}
else
{
    Write-Warning "OS not supported : Some environment variables may not be set"
}
