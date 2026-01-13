using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOLocalizedString : DTOBaseGuid
    {
        public string Key { get; set; }
        public List<Item> Items { get; set; }

        public DTOLocalizedString() : base()
        {
            Items = new List<DTOLocalizedString.Item>();
        }

        public DTOLocalizedString(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTOLocalizedString.Item>();
        }

        public Item FindOrAddItem(string locale)
        {
            var retval = Items.FirstOrDefault(x => x.Locale == locale);
            if (retval == null)
            {
                retval = new Item();
                retval.Locale = locale;
            }
            return retval;
        }

        public class Item
        {
            public string Locale { get; set; }
            public string Value { get; set; }
        }
    }
}
