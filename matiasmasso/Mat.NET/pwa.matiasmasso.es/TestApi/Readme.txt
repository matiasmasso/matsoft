Program.cs insert:

app.UseHttpsRedirection();

//------------------to allow Index.html----------------------------
var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();
app.UseRouting();
//----------------------------------------------


//------------------CORS----------------------------
app.UseCors(); //before useAuthorisation
//------------------CORS----------------------------


app.UseAuthentication();

//---------------------

app.UseAuthorization();

-------------------------------------------------------------------------------
Add a wwwroot folder
Add inside a Index.html page
-------------------------------------------------------------------------------
Download publish profile from Azure
--------------------------------------------------------------------------------
nUGET Microsoft.EntityFrameworkCore.SqlServer