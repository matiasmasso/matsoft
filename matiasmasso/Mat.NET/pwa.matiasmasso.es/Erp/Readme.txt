signatura:
Tools -> Command Line -> Developer Power Shell

C:\Users\matia\Source\Workspaces\MMO\pwa.matiasmasso.es\erp> 
New-SelfSignedCertificate -Type Custom `
>>                           -Subject "CN=MATIAS MASSO, S.A." `
>>                           -KeyUsage DigitalSignature `
>>                           -FriendlyName "Matsoft developer certificate" `
>>                           -CertStoreLocation "Cert:\CurrentUser\My" `
>>                           -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3", "2.5.29.19={text}")

copiar thumbprint i enganxar-la a Erp.csproj, propietat PackageCertificateThumbprint de Windows i de Android

Llista tots els certificats del mmc store:
Get-ChildItem -Path Cert:LocalMachine\MY | Select-Object FriendlyName, Thumbprint, Subject, NotBefore, NotAfter

Thumbprint al portatil Asus Zenbook: '2A7B9A3EE2546515530775CBB06ABE37A432D355'
Es pujat a Personal i a 

incrementar la versió a Erp->Propietats->Android->Manifest->Application version number

Tools -> Command Line -> Developer Command Prompt
-> cd Erp (navigate to Erp project folder)
dotnet publish -f:net6.0-android -c:Release /p:AndroidSigningKeyPass=yfem27Bc /p:AndroidSigningStorePass=yfem27Bc

per windows:
incrementar l aversió a Erp->Properties-> Maui shared -> Application Version
dotnet publish -f net7.0-windows10.0.19041.0 -c Release /p:RuntimeIdentifierOverride=win10-x64

https://play.google.com/console/u/0/developers/8698406063761977578/app/4976132129298031161/app-dashboard?timespan=thirtyDays