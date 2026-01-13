Install-Package Microsoft.EntityFrameworkCore.Tools 

To rebuild Entity Framework models and dbcontext, in Visual Studio, 
select menu Tools -> NuGet Package Manger -> Package Manager Console
and run the following command:
 
Scaffold-DbContext "Data Source=sql.matiasmasso.es;TrustServerCertificate=true;Initial Catalog=MatGen;Persist Security Info=True;User ID=sa_cXJSQYte;Password=CC1SURJQXHyfem27Bc" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Force
Scaffold-DbContext "Data Source=10.74.52.10;TrustServerCertificate=true;Initial Catalog=MatGen;Persist Security Info=True;User ID=sa_cXJSQYte;Password=CC1SURJQXHyfem27Bc" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities  -Project Api -Force
