using System.Collections.Generic;
using System.Linq;

namespace DTO.Integracions.Worten
{
    public class Catalog
    {
        public List<Hierarchy> hierarchies { get; set; }

        public List<Hierarchy> childHierarchies(string parent_code, string locale = "es_ES")
        {
            List<Hierarchy> retval = hierarchies.Where(x => x.parent_code == parent_code).ToList();
            if (!string.IsNullOrEmpty(locale))
                retval = retval.OrderBy(x => x.Caption(locale)).ToList();

            return retval;
        }

    }

    public class Hierarchy
    {
        public string code { get; set; }
        public string parent_code { get; set; }
        public int level { get; set; }
        public List<Label_translation> label_translations { get; set; }

        public string Caption(string locale)
        {
            string retval = "";
            if (label_translations != null)
            {
                Label_translation label = label_translations.FirstOrDefault(x => x.locale.ToUpper() == locale.ToUpper());
                if (label == null)
                    label = label_translations.First();
                if (label != null)
                    retval = label.value;
            }
            return retval;
        }
    }

    public class Label_translation
    {
        public string locale { get; set; }
        public string value { get; set; }
    }


}
