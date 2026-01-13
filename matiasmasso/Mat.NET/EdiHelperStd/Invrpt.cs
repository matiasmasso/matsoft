using System;
using System.Collections.Generic;
using System.Linq;

namespace EdiHelperStd
{
    public class Invrpt
    {
        public string DocNum { get; set; }
        public DateTime Fch { get; set; }

        public List<Interlocutor> Interlocutors { get; set; }
        public List<Item> Items { get; set; }

        public Invrpt()
        {
            Items = new List<Item>();
        }

        public static Invrpt Factory(string src)
        {
            Invrpt retval = new Invrpt();
            List<string> lines = src.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None).ToList();
            Item item = null;
            string nom = "";
            foreach (string line in lines)
            {
                List<string> fields = line.Split('|').ToList();
                switch (fields.First())
                {
                    case "BGM":
                        retval.DocNum = fields[1];
                        break;
                    case "DTM":
                        string formatString = "yyyyMMddHHmm";
                        retval.Fch = DateTime.ParseExact(fields[1], formatString, null);
                        break;
                    case "NADMS":
                        nom = fields.Count <= 2 ? "" : fields[3];
                        retval.AddInterlocutor(Interlocutor.Qualifiers.Sender, fields[1], nom);
                        break;
                    case "NADMR":
                        nom = fields.Count <= 2 ? "" : fields[3];
                        retval.AddInterlocutor(Interlocutor.Qualifiers.Receiver, fields[1], nom);
                        break;
                    case "NADGY":
                        //Inventory reporting party
                        nom = fields.Count <= 2 ? "" : fields[3];
                        retval.AddInterlocutor(Interlocutor.Qualifiers.Reporting, fields[1], nom);
                        break;
                    case "LIN":
                        item = new Item();
                        item.AddSkuId(SkuId.Qualifiers.Ean, fields[1]);
                        retval.Items.Add(item);
                        break;
                    case "PIALIN":
                        string qualifier = fields[3];
                        if (qualifier == "IN")
                            item.AddSkuId(SkuId.Qualifiers.Customer, fields[2]);
                        else if (qualifier == "SA")
                            item.AddSkuId(SkuId.Qualifiers.Supplier, fields[2]);
                        else
                            item.AddSkuId(SkuId.Qualifiers.unknown, fields[2]);
                        break;
                    case "QTYLIN":
                        string calificador = fields[1]; //145: actual stock
                        item.Qty = Int32.Parse(fields[2]);
                        break;
                    default:
                        break;
                }
            }
            return retval;
        }

        public void AddInterlocutor(Interlocutor.Qualifiers qualifier, string gln, string nom)
        {
            Interlocutor value = new Interlocutor();
            value.Qualifier = qualifier;
            value.Gln = gln;
            value.Nom = nom;
            Interlocutors.Add(value);
        }


        public class Interlocutor
        {
            public string Gln { get; set; }
            public string Nom { get; set; }
            public Qualifiers Qualifier { get; set; }
            public enum Qualifiers
            {
                unknown,
                Crm, //Guid identifying the contact from our data base
                Sender,
                Receiver,
                Reporting
            }
        }

        public class Item
        {
            public int Qty { get; set; }

            public List<SkuId> SkuIds { get; set; }

            public void AddSkuId(SkuId.Qualifiers qualifier, string id)
            {
                SkuId value = new SkuId();
                value.Qualifier = qualifier;
                value.Id = id;
                SkuIds.Add(value);
            }
        }


        public class SkuId
        {
            public Qualifiers Qualifier { get; set; }
            public string Id { get; set; }

            public enum Qualifiers
            {
                unknown,
                Guid,
                Ean,
                Customer,
                Supplier
            }

        }
    }
}
