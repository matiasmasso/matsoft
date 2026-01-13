using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOFilter : DTOBaseGuid
    {
        public DTOLangText LangText { get; set; }
        public int Ord { get; set; }
        public List<Item> Items { get; set; }

        public DTOFilter() : base()
        {
            Items = new List<Item>();
            LangText = new DTOLangText(base.Guid, DTOLangText.Srcs.Filter);
        }

        public DTOFilter(Guid oGuid) : base(oGuid)
        {
            Items = new List<Item>();
            LangText = new DTOLangText(base.Guid, DTOLangText.Srcs.Filter);
        }


        public class Item : DTOBaseGuid
        {
            public DTOLangText LangText { get; set; }
            public List<Guid> TargetGuids { get; set; }

            public Item() : base()
            {
                LangText = new DTOLangText(base.Guid, DTOLangText.Srcs.FilterItem);
                TargetGuids = new List<Guid>();
            }

            public Item(Guid oGuid) : base(oGuid)
            {
                LangText = new DTOLangText(base.Guid, DTOLangText.Srcs.FilterItem);
                TargetGuids = new List<Guid>();
            }

            public class Collection : List<Item>
            {
                public static Collection Factory(DTODept dept)
                {
                    List<Item> items = dept.Categories.SelectMany(x => x.FilterItems).ToList();
                    Collection retval = new Collection();
                    retval.AddRange(items);
                    return retval;
                }
                public static Collection Factory(string jsonFilterItemsArray)
                {
                    Collection retval = new Collection();
                    string trimmedFilters = jsonFilterItemsArray.Substring(1, jsonFilterItemsArray.Length - 2); //treu corchetes
                    List<string> sGuids = trimmedFilters.Split(',').ToList();
                    foreach (string sGuid in sGuids)
                    {
                        Guid guid = new Guid(sGuid);
                        DTOFilter.Item item = new DTOFilter.Item(guid);
                        retval.Add(item);
                    }
                    return retval;
                }
            }

        }


        public class Collection : List<DTOFilter>
        {
            public static Collection Tree(Collection filters, DTOFilter.Item.Collection items)
            {
                //monta un arbre amb els filters exclusivament dels items disponibles
                Collection retval = new Collection();
                for (int i = 0; i < filters.Count; i++)
                {
                    for (int j = 0; j < filters[i].Items.Count; j++)
                    {
                        if (items.Any(x => x.Equals(filters[i].Items[j])))
                        {
                            if (!retval.Contains(filters[i]))
                            {
                                DTOFilter oFilter = filters[i];
                                oFilter.Items.RemoveAll(x => !items.Any(y => y.Equals(x)));
                                retval.Add(oFilter);
                                break;
                            }
                        }
                    }

                }
                return retval;
            }

            public Collection WithItems(DTOFilter.Item.Collection filterItems)
            {
                Collection retval = new Collection();
                foreach (DTOFilter filter in this)
                {
                    foreach (DTOFilter.Item item in filter.Items)
                    {
                        var itemGuid = item.Guid;
                        if (filterItems.Any(x => x.Guid.Equals(itemGuid)))
                        {
                            if (!retval.Any(x => x.Guid.Equals(filter.Guid)))
                            {
                                DTOFilter oClon = new DTOFilter(filter.Guid);
                                oClon.LangText = filter.LangText;
                                retval.Add(oClon);
                            }
                            retval.Last().Items.Add(item);
                        }
                    }
                }
                return retval;
            }
        }

    }

}

