using MatHelperStd;

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DTO
{
    public class DTOProductPlugin : DTOBaseGuid
    {
        public const int ItemWidth = 148;
        public const int ItemHeight = 170;

        public string Nom { get; set; }
        public DTOProduct Product { get; set; }
        public List<Item> Items { get; set; }
        public DTOUsrLog UsrLog { get; set; }

        public enum Modes
        {
            Custom,
            Collection,
            Accessories
        }

        public enum Props
        {
            Guid,
            Nom,
            Product,
            Items,
            Usrlog
        }

        public DTOProductPlugin() : base()
        {
            Items = new List<Item>();
            UsrLog = new DTOUsrLog();
        }

        public DTOProductPlugin(Guid oGuid) : base(oGuid)
        {
            Items = new List<Item>();
            UsrLog = new DTOUsrLog();
        }

        public static DTOProductPlugin Factory(DTOProduct oProduct, DTOUser oUser = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOProductPlugin retval = new DTOProductPlugin();
            {
                var withBlock = retval;
                withBlock.Product = oProduct;
                withBlock.UsrLog = DTOUsrLog.Factory(oUser);
            }
            return retval;
        }

        public DTOProductPlugin.Item AddItem(DTOProduct oProduct)
        {
            var retval = DTOProductPlugin.Item.Factory(this, oProduct);
            Items.Add(retval);
            return retval;
        }

        public SpriteHelper.Sprite Sprite(DTOLang oLang)
        {
            var spriteUrl = MmoUrl.ApiUrl("ProductPlugin/sprite", base.Guid.ToString());

            var retval = SpriteHelper.Factory(spriteUrl, DTOProductPlugin.ItemWidth, DTOProductPlugin.ItemHeight);
            foreach (var item in Items)
            {
                var sCaption = item.LangNom.Tradueix(oLang);
                retval.addItem(sCaption, item.Product.GetUrl(oLang));
            }
            return retval;
        }

        public string Snippet()
        {
            return Snippet(base.Guid.ToString(), Modes.Custom);
        }

        public static HashSet<Guid>PluginGuids(DTOLangText src)
        {
            HashSet<Guid> retval = new HashSet<Guid>();
            var pattern = SnippetRegex();
            MatchCollection matches = Regex.Matches(src.Esp, pattern);
            foreach (Match match in matches)
                retval.Add(new Guid(match.Groups["Guid"].Value));
            return retval;
        }

        public static string Snippet(string id)
        {
            //data-mods:
            //1. (default) data-ProductPlugin fa referencia a la tabla on s'emmagatzema una llista arbitraria de skus
            //2.data-ProductPlugin fa referencia a la categoria, y els items son els seus skus 
            return string.Format("<div data-ProductPlugin='{0}'></div>", id);
            //return string.Format("<div data-ProductPlugin='{0}' data-PluginMode='{1}'></div>", id, mode.ToString());
        }

        public static string Snippet(string id, Modes mode)
        {
            return string.Format("<div data-ProductPlugin='{0}' data-ProductPluginMode='{1}'></div>", id, mode.ToString());
        }

        public static string SnippetRegex() //, Modes mode)
        {
            return string.Format(@"<div data-ProductPlugin='{0}'.*?><\/div>", GuidHelper.RegexPattern());
        }

        public Dictionary<string,Object> Minified()
        {
            var minifiedItems = new List<Dictionary<string, Object>>();
            foreach (Item item in Items)
                minifiedItems.Add(item.Minified());

            Dictionary<string, Object> retval  = new Dictionary<string, Object>();
            retval.Add(((int)Props.Guid).ToString(), Guid.ToString());
            if(!string.IsNullOrEmpty(Nom))
                retval.Add(((int)Props.Nom).ToString(), Nom);
            if(Product != null)
                retval.Add(((int)Props.Product).ToString(), Product.Guid.ToString());
            if(Items != null && Items.Count > 0)
                retval.Add(((int)Props.Items).ToString(), (Object)minifiedItems);
            return retval;
        }

        public static List<DTOProductPlugin> ExpandPlugins(Object jObject)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            List<Dictionary<string, Object>> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, Object>>>(json);

            var retval = new List<DTOProductPlugin>();
            foreach (Dictionary<string, Object> x in baseObj)
            {
                var plugin =  DTOProductPlugin.Expand(x);
                retval.Add(plugin);
            }

            return retval;
        }
        private static DTOProductPlugin Expand(Object jObject)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            Dictionary<string, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);

            var retval = new DTOProductPlugin();
            foreach (KeyValuePair<string, Object> x in baseObj)
            {
                var prop = (Props)x.Key.toInteger();
                if (prop == Props.Guid)
                    retval.Guid = new Guid(x.Value.ToString());
                if (prop == Props.Nom)
                    retval.Nom = x.Value.ToString();
                else if (prop == Props.Product)
                    retval.Product = new DTOProduct(new Guid(x.Value.ToString()));
                else if (prop == Props.Items)
                    retval.Items = ExpandItems((Object)x.Value);
            }

            return retval;
        }

        private static List<DTOProductPlugin.Item> ExpandItems(Object jObject)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            List<Dictionary<string, Object>> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, Object>>>(json);

            var retval = new List<DTOProductPlugin.Item>();
            foreach (Dictionary<string, Object> kvp in baseObj)
            {
                var item = new DTOProductPlugin.Item();
                foreach (KeyValuePair<string, Object> x in kvp)
                {
                    var prop = (Item.Props)x.Key.toInteger();
                    if (prop == Item.Props.Guid)
                        item.Guid = new Guid(x.Value.ToString());
                    else if (prop == Item.Props.Sku)
                        item.Product = new DTOProductSku(new Guid(x.Value.ToString()));
                    else if (prop == Item.Props.Langnom)
                        item.LangNom = DTOLangText.Expand(x.Value);
                    else if (prop == Item.Props.Obsolet && item.Product != null)
                        item.Product.obsoleto = true;
                }
                retval.Add(item);
            }

            return retval;
        }



        public class Item : DTOBaseGuid
        {
            public DTOProductPlugin Plugin { get; set; }
            public int Lin { get; set; }
            public DTOProduct Product { get; set; }
            public Byte[] Thumbnail { get; set; }

            public DTOLangText LangNom { get; set; }

            public const int Width = 148;
            public const int Height = 170;

            public enum Props
            {
                Guid,
                Sku,
                Langnom,
                Obsolet
            }

            public Item() : base()
            {
                LangNom = new DTOLangText();
            }

            public Item(Guid oGuid) : base(oGuid)
            {
                LangNom = new DTOLangText();
            }

            public static Item Factory(DTOProductPlugin oPlugin, DTOProduct oProduct = null/* TODO Change to default(_) if this is not a reference type */)
            {
                Item retval = new Item();
                retval.Plugin = oPlugin;
                retval.Product = oProduct;
                return retval;
            }

            public Dictionary<string, Object> Minified()
            {
                Dictionary<string, Object> retval = new Dictionary<string, Object>();
                retval.Add(((int)Props.Guid).ToString(), Guid.ToString());
                retval.Add(((int)Props.Sku).ToString(), Product.Guid.ToString());
                retval.Add(((int)Props.Langnom).ToString(), (Object)LangNom.Minified());
                if(Product.obsoleto)
                retval.Add(((int)Props.Obsolet).ToString(), "1");
                return retval;
            }


        }


    }
}
