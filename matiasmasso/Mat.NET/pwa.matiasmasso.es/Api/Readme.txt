To rebuild Entity Framework models and dbcontext, in Visual Studio, select menu Tools -> NuGet Package Manger -> Package Manager Console and run the following command:
Scaffold-DbContext "Data Source=sql.matiasmasso.es;TrustServerCertificate=true;Initial Catalog=Maxi;Persist Security Info=True;User ID=sa_cXJSQYte;Password=CC1SURJQXHyfem27Bc" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Force


To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.