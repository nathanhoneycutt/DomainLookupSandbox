Clear-Host

Push-Location (Split-Path $MyInvocation.MyCommand.Path)

try
{
    $Env:DOMAINLOOKUP_IPSTACKKEY = "IPSTACK_API_KEY_GOES_HERE"

    $Env:DOMAINLOOKUP_GEOLOCATIONSERVICEURL = (Get-Content -Path .\GeolocationLookupService\Properties\launchSettings.json | ConvertFrom-Json).profiles.GeolocationLookupService.applicationUrl + "/geolocation"
    $Env:DOMAINLOOKUP_PINGSERVICEURL = (Get-Content -Path .\PingLookupService\Properties\launchSettings.json | ConvertFrom-Json).profiles.PingLookupService.applicationUrl + "/ping"
    $Env:DOMAINLOOKUP_REVERSEDNSSERVICEURL = (Get-Content -Path .\DnsLookupService\Properties\launchSettings.json | ConvertFrom-Json).profiles.DnsLookupService.applicationUrl + "/reverseDns"


    Start-Process -Verb Open "cmd.exe" @("/C"; "cd /D $(Resolve-Path -Path './DomainLookupApi')"; " && dotnet watch run")
    Start-Sleep -Milliseconds 500  #starting these too quickly can lead to locked files as each one tries to build, so we delay slightly between each one
    
    Start-Process -Verb Open "cmd.exe" @("/C"; "cd /D $(Resolve-Path -Path './DnsLookupService')"; " && dotnet watch run")
    Start-Sleep -Milliseconds 500

    Start-Process -Verb Open "cmd.exe" @("/C"; "cd /D $(Resolve-Path -Path './GeolocationLookupService')"; " && dotnet watch run")
    Start-Sleep -Milliseconds 500

    Start-Process -Verb Open "cmd.exe" @("/C"; "cd /D $(Resolve-Path -Path './PingLookupService')"; " && dotnet watch run")
}
finally
{
    Pop-Location
}
