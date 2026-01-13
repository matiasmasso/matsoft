
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DTO
{
    public class DTODoc
    {
        public List<string> dest { get; set; }
        public DateTime fch { get; set; }
        public string num { get; set; }
        public List<string> obs { get; set; }
        public List<DTODocItm> itms { get; set; }
        public DTOCur cur { get; set; }
        public DTOLang lang { get; set; }
        public decimal dto { get; set; }
        public int puntsQty { get; set; }
        public decimal puntsTipus { get; set; }
        public DTOAmt puntsBase { get; set; }
        public decimal dpp { get; set; }

        public bool valorat { get; set; }
        public List<DTOTaxBaseQuota> ivaBaseQuotas { get; set; }
        public bool recarrecEquivalencia { get; set; }
        public List<string> coletillas { get; set; }

        public bool displayTotalLogistic { get; set; }
        public bool displayPunts { get; set; }
        public bool writeTemplate { get; set; }

        public DTOInvoice.ExportCods ExportCod { get; set; }

        public DTOIncoterm incoterm { get; set; }
        public bool BOC { get; set; } // Ensobradora: Begining of Collection
        public bool EOC { get; set; } // Ensobradora: End of Collection
        public bool selFeed1 { get; set; } // Ensobradora: End of Collection
        public Estilos estilo { get; set; }

        public List<string> customLines { get; set; }

        public SideLabels sideLabel { get; set; }

        public enum Estilos
        {
            comanda,
            albara,
            factura,
            proforma
        }

        public enum SumConcepts
        {
            sumaDeImportes,
            baseImponible,
            sumaAnterior,
            sumaParcial,
            sumaySigue,
            total
        }

        public enum SideLabels
        {
            none,
            export,
            facturaRectificativa
        }

        public enum FontStyles
        {
            regular,
            bold,
            italic,
            underline
        }


        public DTODoc(DTODoc.Estilos oEstilo, DTOLang oLang, DTOCur oCur, bool BlWriteTemplate = true) : base()
        {
            estilo = oEstilo;
            lang = oLang;
            cur = oCur;
            writeTemplate = BlWriteTemplate;
            dest = new List<string>();
            obs = new List<string>();
            itms = new List<DTODocItm>();
            ivaBaseQuotas = new List<DTOTaxBaseQuota>();
        }

        public Color BackColor()
        {
            Color retval = Color.Black;
            switch (estilo)
            {
                case Estilos.comanda:
                    {
                        retval = Color.Yellow;
                        break;
                    }

                case Estilos.albara:
                    {
                        retval = Color.LightSalmon;
                        break;
                    }

                case Estilos.factura:
                    {
                        retval = Color.SkyBlue;
                        break;
                    }

                case Estilos.proforma:
                    {
                        retval = Color.LightGreen;
                        break;
                    }
            }
            return retval;
        }

        public bool DtoColumnDisplay()
        {
            bool retVal = itms.Exists(x => x.Dto != 0);
            return retVal;
        }

        public bool Descomptes()
        {
            return (dto != 0); // Or _Dpp <> 0 Or _PuntsTipus <> 0)
        }

        public bool Impostos()
        {
            return ivaBaseQuotas.Count > 0;
        }
    }

    public class DTODocItm
    {
        public string Ref { get; set; }
        public string Text { get; set; }
        public int Qty { get; set; }
        public DTOAmt Preu { get; set; }
        public decimal Dto { get; set; }
        public decimal Punts { get; set; }
        public int Boxes { get; set; }
        public decimal m3 { get; set; }
        public int Kg { get; set; }
        public string Hyperlink { get; set; }

        public DTODoc.FontStyles FontStyle { get; set; }
        public int MinLinesBeforeEndPage { get; set; }
        public int LeftPadChars { get; set; }

        public DTODocItm(string sText = "", DTODoc.FontStyles oFontStyle = default(DTODoc.FontStyles), int IntQty = 0, DTOAmt oPreu = null/* TODO Change to default(_) if this is not a reference type */, decimal SngDto = 0, decimal SngPunts = 0, int IntLeftPadChars = 0, int IntMinLinesBeforeEndPage = 0, string sRef = "", string Hyperlink = "") : base()
        {
            Ref = sRef;
            Text = sText;
            if (oFontStyle == default(int))
                FontStyle = DTODoc.FontStyles.regular; // oFontStyle
            else
                FontStyle = oFontStyle;
            Qty = IntQty;
            Preu = oPreu;
            Dto = SngDto;
            Punts = SngPunts;
            LeftPadChars = IntLeftPadChars;
            MinLinesBeforeEndPage = IntMinLinesBeforeEndPage;
        }

        public DTOAmt Import()
        {
            DTOAmt retval = DTOAmt.FromQtyPriceDto(Qty, Preu, Dto);
            return retval;
        }
    }
}
