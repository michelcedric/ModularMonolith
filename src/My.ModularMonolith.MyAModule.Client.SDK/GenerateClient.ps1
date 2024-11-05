dotnet nswag run MyAModule.nswag

(Get-Content MyAModuleClient.cs) -replace "application/json;odata.metadata=minimal;odata.streaming=true", "text/json" | Set-Content -Path MyAModuleClient.cs