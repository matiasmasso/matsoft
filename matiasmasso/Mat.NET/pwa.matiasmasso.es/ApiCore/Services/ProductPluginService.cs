using System.Collections.Generic;
using System.Linq;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ProductPluginService
    {
    }
    public class ProductPluginsService
    {

        public static List<ProductPluginModel> GetValues()
        {
            using(var db = new Entities.MaxiContext())
            {
                return GetValues(db);
            }
        }
        public static List<ProductPluginModel> GetValues(Entities.MaxiContext db)
        {
            var retval = new List<ProductPluginModel>();
            var plugin = new ProductPluginModel();
            foreach (var x in db.ProductPlugins.Include(x => x.ProductPluginItems))
            {
                plugin = new ProductPluginModel(x.Guid);
                retval.Add(plugin);
                foreach (var y in x.ProductPluginItems.OrderBy(x => x.Lin))
                {
                    plugin.AddItem(y.Product, y.NomEsp, y.NomCat, y.NomEng, y.NomPor);
                }
            }
            return retval;
        }

    }
}
