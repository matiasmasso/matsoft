using DTO;
using System.Linq;

namespace Api.Services
{
    public class CacheService
    {
        public static List<DocfileModel> DocFiles { get; set; } = new();

        public static void AddDocFile(DocfileModel docfile)
        {
            if(!DocFiles.Any(x=>x.Hash == docfile.Hash))
            {
                //add file to cache in case user wants to save it
                //so it does not need to travel again
                DocFiles.Add(docfile);
            }

            //clean Cache from old files
            DocFiles.RemoveAll(x => x.FchCreated < DateTime.Now.AddHours(-24));
        }



    }
}
