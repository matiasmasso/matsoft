using DocumentFormat.OpenXml.Spreadsheet;
using DTO.Models.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DTO
{
    public class DTOEdiversaRecadv : DTOBaseGuid
    {
        public DTOGuidNom Customer { get; set; }
        /// <value>
        /// The <c>BGM</c> property represents the document number assigned by the customer
        /// </value>
        public string Bgm { get; set; }

        /// <value>
        /// The <c>DTM</c> property represents the date the goods were received
        /// </value>
        public DateTime Dtm { get; set; }

        /// <value>
        /// The <c>RFF_ON</c> property represents the customer purchase order number
        /// </value>
        public string RffOn { get; set; }

        /// <value>
        /// The <c>RFDQ</c> property represents the delivery note number
        /// </value>
        public string RffDq { get; set; }

        /// <value>
        /// The <c>NADMS</c> property represents the GLN number of the buyer
        /// </value>
        public string NadBy { get; set; }

        /// <value>
        /// The <c>NADSU</c> property represents the GLN number of the supplier
        /// </value>
        public string NadSu { get; set; }

        /// <value>
        /// The <c>Items</c> property represents the list of lines checked for discrepancies.
        /// </value>
        public List<Item> Items { get; set; }

        /// <value>
        /// The <c>Exceptions</c> property represents the errors found on reading the file.
        /// </value>
        public List<Exception> Exceptions { get; set; }


        public DTOEdiversaRecadv() : base()
        {
            Items = new List<Item>();
            Exceptions = new List<Exception>();
        }
        public DTOEdiversaRecadv(Guid guid) : base(guid)
        {
            Items = new List<Item>();
            Exceptions = new List<Exception>();
        }
        public static DTOEdiversaRecadv Factory(DTOEdiversaFile file)
        {
            var retval = new DTOEdiversaRecadv(file.Guid);
            var lines = Regex.Split(file.Stream, "\r\n|\r|\n").ToList();
            foreach (var line in lines)
            {
                var segments = line.Split('|').ToList();
                if (segments.Count > 0)
                {
                    switch (segments.First())
                    {
                        case "BGM":
                            if (segments.Count > 1) retval.Bgm = segments[1];
                            break;
                        case "DTM":
                            if (segments.Count > 2) retval.Dtm =DTOEdiversaFile.ParseFch( segments[2], new List<DTOEdiversaException>());
                            break;
                        case "RFF":
                            if (segments.Count > 2)
                            {
                                if (segments[1] == "ON") retval.RffOn = segments[2];
                                else if (segments[1] == "DQ") retval.RffDq = segments[2];
                            }
                            break;
                        case "NADBY":
                            if (segments.Count > 1) retval.NadBy = segments[1];
                            break;
                        case "NADSU":
                            if (segments.Count > 1) retval.NadSu = segments[1];
                            break;
                        case "LIN":
                            if (segments.Count > 3) retval.Items.Add(new Item { Lin = segments[3] });
                            if (segments.Count > 1) retval.Items.Last().Ean = segments[1];
                            break;
                        case "PIALIN":
                            if (segments.Count > 1 && retval.Items.Count > 0)
                                retval.Items.Last().PiaLin = segments[1];
                            break;
                        case "QTYLIN":
                            if (segments.Count > 1 && retval.Items.Count > 0)
                                retval.Items.Last().QtyLin = segments[1];
                            break;
                        case "CNTRES":
                            if (segments.Count > 1)
                                retval.Items.Last().QtyLin = segments[1];
                            else
                                retval.Exceptions.Add(new Exception("discrepancia entre el numero de linies al fitxer i el numero de linies declarat", Exception.Cods.WrongCntres));
                            break;
                    }
                }
            }
            return retval;
        }

        public class Exception : System.Exception
        {
            public Cods Cod { get; set; }

            public enum Cods
            {
                NotSet,
                ItemsCountDiscrepancy,
                WrongCntres
            }

            public Exception(string msg, Cods? cod = Cods.NotSet) : base(msg)
            {
                Cod = Cod;
            }

        }


        public class Item
        {

            public DTOGuidNom Sku { get; set; }

            /// <value>
            /// The <c>LIN</c> property represents the Ean 13 code of the product.
            /// </value>
            public string Lin { get; set; }
            /// <value>
            /// The <c>LIN</c> property represents the Ean 13 code of the product.
            /// </value>
            public string Ean { get; set; }

            /// <value>
            /// The <c>PIALIN</c> property represents the customer product ref.
            /// </value>
            public string PiaLin { get; set; }

            /// <value>
            /// The <c>QTYLIN</c> property represents the units received and accepted.
            /// </value>
            public string QtyLin { get; set; }

            /// <value>
            /// The <c>Exceptions</c> property represents the errors found on reading the item.
            /// </value>
            public List<Exception> Exceptions { get; set; }

            public Item()
            {
                Exceptions = new List<Exception>();
            }
        }



    }



}
