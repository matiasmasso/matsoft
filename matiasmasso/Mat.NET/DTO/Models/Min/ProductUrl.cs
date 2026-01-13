using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class ProductUrl : Minifiable
    {
        public enum Cods
        {
            Segment,
            ProductCod,
            Brand,
            Dept,
            Category,
            Sku,
            Lang,
            Canonical
        }
        public static Dictionary<string, Object> Factory(string segment, int productCod, object brand, object dept, object category, object sku, object lang, int canonical)
        {
            ProductUrl retval = new ProductUrl();
            retval.Add(Cods.Segment, segment);
            retval.Add(Cods.ProductCod, productCod);
            if (brand != System.DBNull.Value)
                retval.Add(Cods.Brand, brand);
            if (dept != System.DBNull.Value)
                retval.Add(Cods.Dept, dept);
            if (category != System.DBNull.Value)
                retval.Add(Cods.Category, category);
            if (sku != System.DBNull.Value)
                retval.Add(Cods.Sku, sku);
            if (lang != System.DBNull.Value)
            {
                switch (lang)
                {
                    case "CAT":
                        retval.Add(Cods.Lang, (int)DTOLang.CAT().id);
                        break;
                    case "ENG":
                        retval.Add(Cods.Lang, (int)DTOLang.ENG().id);
                        break;
                    case "POR":
                        retval.Add(Cods.Lang, (int)DTOLang.POR().id);
                        break;
                    default:
                        retval.Add(Cods.Lang, (int)DTOLang.ESP().id);
                        break;
                }
            }
            if (canonical == 1)
                retval.Add(Cods.Canonical, 1);

            return retval;
        }

        public void Add(Cods cod, Object value)
        {
            base.Add(((int)cod).ToString(), value);
        }



        public bool Matches(DTOProduct.SourceCods productCod, List<string> segments)
        {
            bool matchesSegment = false;
            bool matchesCod = false;
            foreach (KeyValuePair<string, Object> x in this)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.ProductCod)
                    matchesCod = (DTOProduct.SourceCods)(int)x.Value == productCod;
                else if (cod == Cods.Segment & segments.Count > 0)
                {
                    if (productCod == DTOProduct.SourceCods.Brand)
                    {
                        matchesSegment = String.Equals(x.Value.ToString(), segments[0], StringComparison.OrdinalIgnoreCase);
                    }
                    else if (productCod == DTOProduct.SourceCods.Dept & segments.Count > 1)
                        matchesSegment = String.Equals(x.Value.ToString(), segments[1], StringComparison.OrdinalIgnoreCase);
                    else if (productCod == DTOProduct.SourceCods.Category & segments.Count > 1)
                    {
                        matchesSegment = (x.Value.ToString() == segments[1]);
                        if (!matchesSegment && segments.Count > 2 && String.Equals(x.Value.ToString(), segments[2], StringComparison.OrdinalIgnoreCase))
                            matchesSegment = true;
                    }

                    else if (productCod == DTOProduct.SourceCods.Sku)
                    {
                        if (segments.Count > 2)
                        {
                            matchesSegment = String.Equals(x.Value.ToString(), segments[2], StringComparison.OrdinalIgnoreCase);
                            if (!matchesSegment && segments.Count > 3 && String.Equals(x.Value.ToString(), segments[3], StringComparison.OrdinalIgnoreCase))
                                matchesSegment = true;
                        }
                    }
                }

            }
            return matchesCod & matchesSegment;
        }

        public static int ProductCod(Dictionary<string, Object> value)
        {
            int retval = 0;
            foreach (KeyValuePair<string, Object> x in value)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.ProductCod)
                {
                    retval = Convert.ToInt32(x.Value);
                    break;
                }
            }
            return retval;
        }

        public static Guid ProductGuid(Dictionary<string, Object> value)
        {
            Guid retval = System.Guid.Empty;
            switch (ProductCod(value))
            {
                case (int)DTOProduct.SourceCods.Brand:
                    retval = Guid(value, (int)Cods.Brand);
                    break;
                case (int)DTOProduct.SourceCods.Dept:
                    retval = Guid(value, (int)Cods.Dept);
                    break;
                case (int)DTOProduct.SourceCods.Category:
                    retval = Guid(value, (int)Cods.Category);
                    break;
                case (int)DTOProduct.SourceCods.Sku:
                    retval = Guid(value, (int)Cods.Sku);
                    break;
            }
            return retval;
        }

        public static DTOLang lang(Dictionary<string, Object> value)
        {
            DTOLang retval = null;
            foreach (KeyValuePair<string, Object> x in value)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.Lang)
                {
                    switch (Convert.ToInt32(x.Value))
                    {
                        case (int)DTOLang.Ids.CAT:
                            retval = DTOLang.CAT();
                            break;
                        case (int)DTOLang.Ids.ENG:
                            retval = DTOLang.ENG();
                            break;
                        case (int)DTOLang.Ids.POR:
                            retval = DTOLang.POR();
                            break;
                        default:
                            retval = DTOLang.ESP();
                            break;
                    }
                }
            }
            return retval;
        }

        public bool IsCanonical(Dictionary<string, Object> value)
        {
            bool retval = false;
            foreach (KeyValuePair<string, Object> x in value)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.Canonical)
                {
                    retval = (int)x.Value == 1;
                    break;
                }
            }
            return retval;
        }

        public static string Segment(Dictionary<string, Object> value)
        {
            string retval = "";
            foreach (KeyValuePair<string, Object> x in value)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.Segment)
                {
                    retval = x.Value.ToString();
                    break;
                }
            }
            return retval;
        }

    }
}
