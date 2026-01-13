using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Integracions.Edi
{
    public class Invrpt
    {

        public Guid Guid { get; set; }
        public Guid File { get; set; }
        public string DocNum { get; set; }
        public DateTime Fch { get; set; }

        public string NadMs { get; set; } //message sender party
        public string NadGy { get; set; } //stock holder party

        public List<Interlocutor> Interlocutors { get; set; }
        public List<Item> Items { get; set; }
        public List<DTO.Integracions.Edi.Exception> Exceptions { get; set; }

        public enum ExceptionCods
        {
            notset,
            system,
            missingFieldInSegment,
            skuNotFound,
            senderNotFound,
            receiverNotFound,
            reportingNotFound
        }

        public Invrpt()
        {
            Guid = Guid.NewGuid();
            Interlocutors = new List<Interlocutor>();
            Items = new List<Item>();
            Exceptions = new List<DTO.Integracions.Edi.Exception>();
        }

        public static Invrpt Factory(DTOEdiversaFile oFile)
        {
            Invrpt retval = new Invrpt();
            try
            {
                retval.File = oFile.Guid;
                string src = oFile.Stream;
                retval = Factory(src);
            }
            catch (System.Exception ex)
            {
                retval.AddException(ExceptionCods.system, ex.Message);
            }
            return retval;
        }


        public static Invrpt Factory(string src)
        {
            Invrpt retval = new Invrpt();
            try
            {
                List<string> lines = src.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None).
                    Where(x => !string.IsNullOrEmpty(x)).ToList();
                Item item = null;
                string nom = "";
                foreach (string line in lines)
                {
                    List<string> fields = line.Split('|').ToList();
                       switch (fields.First())
                        {
                            case "BGM":
                                if (fields.Count < 2)
                                    retval.AddException(ExceptionCods.missingFieldInSegment, "missing fields at BGM segment");
                                else
                                    retval.DocNum = fields[1];
                                break;
                            case "DTM":
                                if (fields.Count < 2)
                                    retval.AddException(ExceptionCods.missingFieldInSegment, "missing fields at DTM segment");
                                else
                                    retval.Fch = DateTime.ParseExact(fields[1], "yyyyMMddHHmm", null);
                                break;
                            case "NADMS":
                                if (fields.Count < 2)
                                    retval.AddException(ExceptionCods.missingFieldInSegment, "missing fields at NADMS segment");
                                else
                                {
                                    retval.NadMs = fields[1];
                                    nom = fields.Count == 2 ? "" : fields[2];
                                    retval.AddInterlocutor(Interlocutor.Qualifiers.Sender, fields[1], nom);
                                }
                                break;
                            case "NADMR":
                                if (fields.Count < 2)
                                    retval.AddException(ExceptionCods.missingFieldInSegment, "missing fields at NADMR segment");
                                else
                                {
                                    nom = fields.Count <= 2 ? "" : fields[2];
                                    retval.AddInterlocutor(Interlocutor.Qualifiers.Receiver, fields[1], nom);
                                }
                                break;
                            case "NADGY":
                                if (fields.Count < 2)
                                    retval.AddException(ExceptionCods.missingFieldInSegment, "missing fields at NADGY segment");
                                else
                                {
                                    //Inventory reporting party
                                    retval.NadGy = fields[1];
                                    nom = fields.Count <= 2 ? "" : fields[2];
                                    retval.AddInterlocutor(Interlocutor.Qualifiers.Reporting, fields[1], nom);
                                }
                                break;
                            case "LIN":
                                item = new Item();
                                retval.Items.Add(item);
                                if (fields.Count < 4)
                                    item.AddException(ExceptionCods.missingFieldInSegment, "missing fields at LIN segment");
                                else
                                    item.Lin = Int32.Parse(fields[3]);
                                item.AddSkuId(SkuId.Qualifiers.Ean, fields[1]);
                                break;
                            case "PIALIN":
                                if (fields.Count < 4)
                                    item.AddException(ExceptionCods.missingFieldInSegment, "missing fields at PIALIN segment");
                                else
                                {
                                    string qualifier = fields[3];
                                    if (qualifier == "IN")
                                        item.AddSkuId(SkuId.Qualifiers.Customer, fields[2]);
                                    else if (qualifier == "SA")
                                        item.AddSkuId(SkuId.Qualifiers.Supplier, fields[2]);
                                    else
                                        item.AddSkuId(SkuId.Qualifiers.unknown, fields[2]);
                                }
                                break;
                            case "QTYLIN":
                                if (fields.Count < 3)
                                    item.AddException(ExceptionCods.missingFieldInSegment, "missing fields at QTYLIN segment");
                                else
                                {
                                    string calificador = fields[1]; //145: actual stock
                                    var sQty = fields[2];
                                    if (sQty.IndexOf(".") >= 0)
                                    {
                                        var pos = sQty.IndexOf(".");
                                        sQty = sQty.Substring(0, pos);
                                    }
                                        item.Qty = Int32.Parse(sQty);
                                }
                                break;
                            default:
                                break;
                        }


                }
            }
            catch (System.Exception ex)
            {
                retval.AddException(ExceptionCods.system, ex.Message);
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
        public void AddException(ExceptionCods cod, string reason, DTOEdiversaException.TagCods tagCod = DTOEdiversaException.TagCods.NotSet, Object tag = null)
        {
            Exception value = new Exception();
            value.Cod = (int)cod;
            value.Msg = reason;
            value.TagCod = (int)tagCod;
            value.Tag = tag.ToString();
            Exceptions.Add(value);
        }

        public List<DTO.Integracions.Edi.Exception> AllExceptions()
        {
            List<DTO.Integracions.Edi.Exception> retval = Exceptions;
            retval.AddRange(Items.SelectMany(x => x.Exceptions));
            return retval;
        }

        public class Interlocutor
        {
            public Guid? Guid { get; set; }
            public string Gln { get; set; }
            public string Nom { get; set; }
            public Qualifiers Qualifier { get; set; }
            public enum Qualifiers
            {
                unknown,
                Sender,
                Receiver,
                Reporting
            }

        }

        public class Item
        {
            public Guid Guid { get; set; }
            public int Lin { get; set; }
            public string Ean { get; set; }
            public int Qty { get; set; }
            public int Retail { get; set; }

            public List<SkuId> SkuIds { get; set; }
            public List<DTO.Integracions.Edi.Exception> Exceptions { get; set; }

            public Item()
            {
                Guid = Guid.NewGuid();
                SkuIds = new List<SkuId>();
                Exceptions = new List<DTO.Integracions.Edi.Exception>();
            }

            public string SkuId(SkuId.Qualifiers qualifier)
            {
                var retval = string.Empty;
                var value = SkuIds.FirstOrDefault(x => x.Qualifier == qualifier);
                if (value != null) retval = value.Id;
                return retval;
            }


            public void AddSkuId(SkuId.Qualifiers qualifier, string id)
            {
                SkuId value = new SkuId();
                value.Qualifier = qualifier;
                value.Id = id;
                SkuIds.Add(value);
            }

            public void AddException(ExceptionCods cod, string reason, object tag = null)
            {
                DTO.Integracions.Edi.Exception value = new DTO.Integracions.Edi.Exception();
                value.Cod = (int)cod;
                value.Msg = reason;
                Exceptions.Add(value);
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
