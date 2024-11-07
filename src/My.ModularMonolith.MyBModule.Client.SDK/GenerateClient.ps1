dotnet nswag run MyBModule.nswag

(Get-Content MyBModuleClient.cs) -replace "application/json;odata.metadata=minimal;odata.streaming=true", "text/json" | Set-Content -Path MyBModuleClient.cs