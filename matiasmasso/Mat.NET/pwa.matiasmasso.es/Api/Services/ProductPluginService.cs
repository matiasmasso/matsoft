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

        public static List<ProductPluginDTO> All(Entities.MaxiContext db)
        {
            var entities = db.ProductPluginItems
                .Include(x => x.PluginNavigation)
                .ToList();

            var retval = new List<ProductPluginDTO>();
            var plugin = new ProductPluginDTO();
            foreach (var x in entities)
            {
                plugin = new ProductPluginDTO(x.Guid);
                retval.Add(plugin);
                foreach (var y in x.PluginNavigation.ProductPluginItems.OrderBy(x => x.Lin))
                {
                    plugin.AddItem(y.Product, y.NomEsp, y.NomCat, y.NomEng, y.NomPor);
                }
            }
            return retval;
        }

        //To Deprecate
        public static List<ProductPluginDTO> Find(List<Guid> guids)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entities = (from item in db.ProductPluginItems
                                where guids.Contains(item.Plugin)
                                orderby item.Plugin, item.Lin
                                select item).ToList();

                var retval = new List<ProductPluginDTO>();
                var plugin = new ProductPluginDTO();
                foreach (var entity in entities)
                {
                    if (!entity.Plugin.Equals(plugin.Guid))
                    {
                        plugin = new ProductPluginDTO(entity.Plugin);
                        retval.Add(plugin);
                    }
                    var item = new ProductPluginDTO.Item()
                    {
                        Product = entity.Product,
                        Caption = new LangTextModel(entity.NomEsp ?? "", entity.NomCat, entity.NomEng, entity.NomPor)
                    };
                    plugin.Items.Add(item);

                }
                var missingGuids = guids.Where(x => !retval.Any(y => y.Guid.Equals(x))).ToList();
                if (missingGuids.Count > 0)
                {
                    //since Guid does not match any product plugin,
                    //check if it matches any category collection plugin
                    var skus = (from item in db.VwSkuNoms
                                where guids.Contains(item.CategoryGuid)
                                orderby item.CategoryGuid, item.SkuNom
                                select item).ToList();

                    foreach (var sku in skus)
                    {
                        if (!sku.CategoryGuid.Equals(plugin.Guid))
                        {
                            plugin = new ProductPluginDTO(sku.CategoryGuid);
                            retval.Add(plugin);
                        }
                        var item = new ProductPluginDTO.Item()
                        {
                            Product = sku.SkuGuid,
                            Caption = new LangTextModel(sku.SkuNomEsp ?? "", sku.SkuNomCat, sku.SkuNomEng, sku.SkuNomPor)
                        };
                        plugin.Items.Add(item);

                    }

                }
                return retval;
            }
        }

    }
}
