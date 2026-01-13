Create new ASP Core Web Api project

default namespace: project->properties->Application->General->Default namespace: Api

create wwwroot folder and wwwroot/index.html welcome page

Install-package Microsoft.EntityFrameworkCore
Install-package Microsoft.EntityFrameworkCore.Design
Install-package Microsoft.EntityFrameworkCore.SqlServer
Install-package Microsoft.AspNetCore.Authentication.JwtBearer
Install-package ClosedXML
Install-package Newtonsoft.Json
Install-package Microsoft.EntityFrameworkCore.Relational
Install-package Microsoft.EntityFrameworkCore.SqlServer.NetTopologySuite 
Install-package System.Drawing.Common
Microsoft.AspNetCore.Authorization?

Scaffold-DbContext "Data Source=sql.matiasmasso.es;TrustServerCertificate=true;Initial Catalog=Maxi;Persist Security Info=True;User ID=sa_cXJSQYte;Password=CC1SURJQXHyfem27Bc" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Project ApiCore -Force
Scaffold-DbContext "Data Source=10.74.52.10;TrustServerCertificate=true;Initial Catalog=Maxi;Persist Security Info=True;User ID=sa_cXJSQYte;Password=CC1SURJQXHyfem27Bc" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Project ApiCore -Force

Project properties Application General Default namespace
$(MSBuildProjectName.Replace(" ", "_"))
