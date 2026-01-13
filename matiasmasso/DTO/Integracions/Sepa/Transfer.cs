using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DTO.Helpers;

namespace DTO.Integracions.Sepa
{
    public class Transfer
    {
        /// <summary>
        /// formatted accounts registry number (Cca.FormattedId())
        /// </summary>
        public string? Id { get; set; }
        public string? RaoSocialEmisor { get; set; }
        public string? NifEmisor { get; set; }
        public string? IbanEmisor { get; set; }
        public string? BancEmisorSwift { get; set; }
        public DateTime Fch { get; set; }
        public string Currency { get; set; } = "EUR"; //ISO 4217
        public List<Item> Items { get; set; } = new();

        public decimal Total() => Items.Sum(x => x.Amount);
        public class Item : BaseGuid
        {
            public string? Beneficiari { get; set; }
            public string? Iban { get; set; }
            public string? Swift { get; set; }
            public string? Concept { get; set; }
            public decimal Amount { get; set; }

            public Item() : base() { }
            public Item(Guid guid) : base(guid) { }
        }

        public string XML_Deprecated()
        {
            //declarations
            var fchCreated = DateTime.Now;
            var msgId = string.Format("{0}.{1:yyyyMMddhhmmss}", Id, fchCreated);
            var invariantCulture = System.Globalization.CultureInfo.InvariantCulture;

            // Raiz
            var doc = new XmlDocumentModel(true);
            var rootSegment = doc.AddSegment("Document");
            rootSegment.AddAttributes("xmlns", "urn:iso:std:iso:20022:tech:xsd:pain.001.001.03", "xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            var mainSegment = rootSegment.AddSegment("CstmrCdtTrfInitn");

            // cabecera
            var oGrpHdr = mainSegment.AddSegment("GrpHdr");
            var oMsgId = oGrpHdr.AddSegment("MsgId", msgId);
            var oCreDtTm = oGrpHdr.AddSegment("CreDtTm", SepaHelper.Normalize(fchCreated));
            var oNbOfTxs = oGrpHdr.AddSegment("NbOfTxs", SepaHelper.Normalize(Items.Count()));
            var oCtrlSum = oGrpHdr.AddSegment("CtrlSum", SepaHelper.Normalize(Total())); // FormatSuma(transfer.Items))

            var oParteIniciadora = oGrpHdr.AddSegment("InitgPty");
            var oOrgNm = oParteIniciadora.AddSegment("Nm", SepaHelper.Normalize(RaoSocialEmisor));
            var oOrgId = oParteIniciadora.AddSegment("Id");
            var oOrgId2 = oOrgId.AddSegment("OrgId");
            var oOrgIdOthr = oOrgId2.AddSegment("Othr");
            // La Caixa nomes admet el NIF pelat aqui:
            // Dim oOrgIdOthrId As var = oOrgIdOthr.AddSegment("Id", IdentificacioPresentador(transfer))
            var oOrgIdOthrId = oOrgIdOthr.AddSegment("Id", SepaHelper.Normalize(NifEmisor));

            // información del pago
            var oPaymentInfo = mainSegment.AddSegment("PmtInf"); // Payment info
            var oPaymentInfoId = oPaymentInfo.AddSegment("PmtInfId", Id);
            var oPaymentMethod = oPaymentInfo.AddSegment("PmtMtd", "TRF"); // Direct Debit

            var oPaymentTypeInfo = oPaymentInfo.AddSegment("PmtTpInf");
            var oServiceLevel = oPaymentTypeInfo.AddSegment("SvcLvl");
            var oServiceLevelCode = oServiceLevel.AddSegment("Cd", "SEPA");


            var oFchExec = oPaymentInfo.AddSegment("ReqdExctnDt", SepaHelper.Normalize(Fch));
            var oDbtr = oPaymentInfo.AddSegment("Dbtr");
            var oMyNom = oDbtr.AddSegment("Nm", SepaHelper.Normalize(RaoSocialEmisor));
            var oDbtrAcct = oPaymentInfo.AddSegment("DbtrAcct");
            var oDbtrAcctId = oDbtrAcct.AddSegment("Id");
            var oDbtrIBAN = oDbtrAcctId.AddSegment("IBAN", IbanEmisor);
            var oDbtrAgt = oPaymentInfo.AddSegment("DbtrAgt");
            var oFinInstnId = oDbtrAgt.AddSegment("FinInstnId");
            var oDbtrAgtBIC = oFinInstnId.AddSegment("BIC", BancEmisorSwift);
            var oChrgBr = oPaymentInfo.AddSegment("ChrgBr", "SHAR");


            foreach (var item in Items)
            {
                var oCdtTrfTxInf = oPaymentInfo.AddSegment("CdtTrfTxInf");
                var oPmtId = oCdtTrfTxInf.AddSegment("PmtId");
                var oEndToEndId = oPmtId.AddSegment("EndToEndId", item.Guid.ToString("N"));

                var oAmt = oCdtTrfTxInf.AddSegment("Amt");
                var oInstdAmt = oAmt.AddSegment("InstdAmt", SepaHelper.Normalize(item.Amount));
                oInstdAmt.AddAttribute("Ccy", Currency);

                var oUltmtDbtr = oCdtTrfTxInf.AddSegment("UltmtDbtr");
                var oUltmtDbtrNm = oUltmtDbtr.AddSegment("Nm", RaoSocialEmisor);

                var oCdtrAgt = oCdtTrfTxInf.AddSegment("CdtrAgt");
                var oCdtrAgtFinInstnId = oCdtrAgt.AddSegment("FinInstnId");

                var oCdtrBIC = oCdtrAgtFinInstnId.AddSegment("BIC", item.Swift);

                var oCdtr = oCdtTrfTxInf.AddSegment("Cdtr");
                oCdtr.AddSegment("Nm", SepaHelper.Normalize(item.Beneficiari));

                var oCdtrAcct = oCdtTrfTxInf.AddSegment("CdtrAcct");
                var oCdtrAcctId = oCdtrAcct.AddSegment("Id");
                var oCdtrIBAN = oCdtrAcctId.AddSegment("IBAN", item.Iban);

                if (!string.IsNullOrEmpty(item.Concept))
                {
                    var oRmtInf = oCdtTrfTxInf.AddSegment("RmtInf");
                    var oUstrd = oRmtInf.AddSegment("Ustrd", SepaHelper.Normalize(item.Concept).TruncateWithEllipsis(140));
                }
            }

            string retval = doc.ToString();
            return retval;
        }



        public string XML()
        {
            //declarations
            var fchCreated = DateTime.Now;
            var msgId = string.Format("{0}.{1:yyyyMMddhhmmss}", Id, fchCreated);
            var invariantCulture = System.Globalization.CultureInfo.InvariantCulture;
            XNamespace ns = "urn:iso:std:iso:20022:tech:xsd:pain.001.001.03";
            XNamespace xsiNs = "http://www.w3.org/2001/XMLSchema-instance";


            List<XElement> nodes = new();
            foreach (var item in Items)
            {
                var node = new XElement(ns + "CdtTrfTxInf",
                                new XElement(ns + "PmtId",
                                    new XElement(ns + "EndToEndId", item.Guid.ToString("N"))
                                    ),
                                new XElement(ns + "Amt",
                                    new XElement(ns + "InstdAmt", SepaHelper.Normalize(item.Amount),
                                        new XAttribute("Ccy", Currency)
                                        )
                                    ),
                                new XElement(ns + "UltmtDbtr",
                                    new XElement(ns + "Nm", RaoSocialEmisor)
                                    ),
                                new XElement(ns + "CdtrAgt",
                                    new XElement(ns + "FinInstnId",
                                        new XElement(ns + "BIC", item.Swift)
                                        )
                                    ),
                                new XElement(ns + "Cdtr",
                                    new XElement(ns + "Nm", SepaHelper.Normalize(item.Beneficiari))
                                    ),
                                new XElement(ns + "CdtrAcct",
                                    new XElement(ns + "Id",
                                        new XElement(ns + "IBAN", item.Iban)
                                        )
                                    ),
                                new XElement(ns + "RmtInf",
                                    new XElement(ns + "Ustrd", SepaHelper.Normalize(item.Concept).TruncateWithEllipsis(140))
                                    )
                    );
                nodes.Add(node);
            }

            XDocument document = new XDocument
                (
                    //new XDeclaration("1.0", "utf-8", "yes"),
                    //new XDeclaration("1.0", "ISO-8859-1", "yes"),
                    //new XComment("XML from plain code"),

                    new XElement(ns + "Document",
                        new XAttribute("xmlns", "urn:iso:std:iso:20022:tech:xsd:pain.001.001.03"),
                        new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                        new XElement(ns + "CstmrCdtTrfInitn",
                            new XElement(ns + "GrpHdr",
                                new XElement(ns + "MsgId", msgId),
                                new XElement(ns + "CreDtTm", SepaHelper.Normalize(fchCreated)),
                                new XElement(ns + "NbOfTxs", SepaHelper.Normalize(Items.Count())),
                                new XElement(ns + "CtrlSum", SepaHelper.Normalize(Total())),
                                new XElement(ns + "InitgPty",
                                    new XElement(ns + "Nm", SepaHelper.Normalize(RaoSocialEmisor)),
                                    new XElement(ns + "Id",
                                        new XElement(ns + "OrgId",
                                            new XElement(ns + "Othr",
                                                new XElement(ns + "Id", SepaHelper.Normalize(NifEmisor))
                                    )
                                    )
                                    )
                                    )
                            ),

                        // información del pago
                        new XElement(ns + "PmtInf",
                            new XElement(ns + "PmtInfId", Id),
                            new XElement(ns + "PmtMtd", "TRF"),
                            new XElement(ns + "PmtTpInf",
                                new XElement(ns + "SvcLvl",
                                    new XElement(ns + "Cd", "SEPA")
                                    )
                                ),
                            new XElement(ns + "ReqdExctnDt", SepaHelper.Normalize(DateOnly.FromDateTime(Fch))),
                            new XElement(ns + "Dbtr",
                                new XElement(ns + "Nm", SepaHelper.Normalize(RaoSocialEmisor))
                                ),
                            new XElement(ns + "DbtrAcct",
                                new XElement(ns + "Id",
                                    new XElement(ns + "IBAN", IbanEmisor)
                                    )
                                ),
                            new XElement(ns + "DbtrAgt",
                                new XElement(ns + "FinInstnId",
                                    new XElement(ns + "BIC", BancEmisorSwift)
                                    )
                                ),
                            new XElement(ns + "ChrgBr", "SHAR"),

                            nodes
                        )
                     )
                 )
            );


            document.Declaration = new XDeclaration("1.0", "utf-8", null);
            StringWriter writer = new Utf8StringWriter();
            document.Save(writer, SaveOptions.None);
            return writer.ToString();
        }

        static void Main()
        {
            XDocument doc = XDocument.Load("test.xml",
                                           LoadOptions.PreserveWhitespace);
            doc.Declaration = new XDeclaration("1.0", "utf-8", null);
            StringWriter writer = new Utf8StringWriter();
            doc.Save(writer, SaveOptions.None);
            Console.WriteLine(writer);
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }

    }
}
