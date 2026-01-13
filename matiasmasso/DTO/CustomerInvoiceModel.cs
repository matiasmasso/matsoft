using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using DTO.Integracions.Verifactu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CustomerInvoiceBaseModel : BaseGuid, IModel
    {
        public EmpModel.EmpIds? Emp { get; set; }
        public Guid? Customer { get; set; }
        public Guid? CtaDebit { get; set; }
        public Guid? CtaCredit { get; set; }
        public LangDTO? Lang { get; set; }
        public decimal? IVApct { get; set; }
        public decimal? IRPFpct { get; set; }
        public string? VfConcept { get; set; } // concepte per VeriFactu
        public string? VfTaxScheme { get; set; } // 01-regimen general, 11-locals de negoci
        public string? VfTaxType { get; set; } // S1-Sujeta No Exenta, N1-No Sujeta
        public string? VfTaxException { get; set; } // E1: Exenta por el artículo 20.El alquiler de vivienda habitual a particulares está exento de IVA según el artículo 20.1.23 de la Ley del IVA y el Real Decreto 1619/2012,

        public List<CustomerInvoiceModel.Item> Items { get; set; } = new();
        public List<CustomerInvoiceModel.Item> Notas { get; set; } = new();

        public CustomerInvoiceBaseModel() : base() { }
        public CustomerInvoiceBaseModel(Guid guid) : base(guid) { }

        public bool ShouldShowBaseImponible()
        {
            var retval = LiniesAmbImport > 1 && ((IVApct != null && IVApct != 0) || (IRPFpct != null && IRPFpct != 0));
            return retval;
        }

        public decimal BaseImponible => Items.Sum(x => x.Total ?? 0);
        public decimal IVA => Math.Round(BaseImponible * (IVApct ?? 0) / 100, 2);
        public decimal IRPF => Math.Round(BaseImponible * (IRPFpct ?? 0) / 100, 2);
        public decimal Total => BaseImponible + IVA - IRPF;

        public bool ShouldShowIVA() => IVApct != null && IVApct != 0 && BaseImponible != 0;
        public bool ShouldShowIRPF() => IRPFpct != null && IRPFpct != 0 && BaseImponible != 0;
        public bool ShouldShowTotal()
        {
            var retval = BaseImponible != Total;
            if (LiniesAmbImport > 1 && (IVApct == null || IVApct == 0) && (IRPFpct == null || IRPFpct == 0)) retval = true;
            return retval;
        }

        public int LiniesAmbImport => Items.Where(x => x.Total != null && x.Total != 0).Count();

        public bool Matches(decimal? searchterm)
        {
            var retval = false;

            if (searchterm == null)
                retval = true;
            else
                retval = BaseImponible == searchterm || Total == searchterm;
            return retval;
        }

    }
    public class CustomerInvoiceModel : CustomerInvoiceBaseModel, IModel
    {
        public string? RaoSocial { get; set; }
        public string? NIF { get; set; }
        public string? Address { get; set; }
        public ZipDTO? Zip { get; set; }
        public string? Serie { get; set; }
        public int FraNum { get; set; }
        public DateOnly Fch { get; set; }
        public DateOnly Vto { get; set; }
        public UserModel? User { get; set; }
        public CcaModel? Cca { get; set; }
        public DocfileModel? Docfile { get; set; }
        public string? VfCsv { get; set; } // Codi verificacio VeriFactu
        public DateTime? VfFch { get; set; } //Verifactu posting date
        public string? VfQr { get; set; }

        public CustomerInvoiceModel() : base() { }
        public CustomerInvoiceModel(Guid guid) : base(guid) { }

        public string VeriFactuQrUrl()
        {
            //var baseUrl = "https://verifactu.qr.facturae.es/?";

            var baseUrl = "https://prewww2.aeat.es/wlpl/TIKE-CONT/ValidarQR?";
            var numSerie = string.IsNullOrEmpty(Serie) || Serie == "0" ? FraNum.ToString() : EncodeParam(FraNum.ToString() + "&" + Serie);
            var parameters = new List<string>
            {
                $"nif={EncodeParam(NIF)}",
                $"numserie={numSerie}",
                $"fecha={Fch:dd-MM-yyyy}",
                $"importe={Total.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)}"
            };
            var queryString = string.Join("&", parameters);
            return baseUrl + queryString;
        }

        public VfInvoice ToVfInvoice()
        {
            var vfInvoice = new VfInvoice
            {
                Status = "POST",
                InvoiceType = VfInvoice.VfInvoiceTypes.F1.ToString(),
                InvoiceDate = Fch,
                InvoiceID = FraNum.ToString(),
                SellerID = Emp==EmpModel.EmpIds.Tatita ?  "B67246132":"37697236Y",
                CompanyName = Emp == EmpModel.EmpIds.Tatita ? "TATITA 84, S.L.":"Massó Cases, Matias",
                RelatedPartyID = NIF,
                RelatedPartyName = RaoSocial,
                Text = VfConcept,

                TaxItems = new List<VfTaxItem>
                {
                     new VfTaxItem { TaxRate = IVApct ?? 0, 
                         TaxBase = BaseImponible, 
                         TaxAmount = Math.Round( BaseImponible * (IVApct ?? 0)/100 ,2),
                            TaxScheme = VfTaxScheme,
                            TaxType = VfTaxType,
                            TaxException = VfTaxException
                      }
                }
            };
            return vfInvoice;

        }

        public static string EncodeParam(string? param)
        {
            return Uri.EscapeDataString(param ?? "");
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Serie))
            {
                return $"Fra.{FraNum} del {Fch:dd/MM/yy} a {RaoSocial} per {Total:N2} €";
            }
            else
            {
                return $"Fra.{Serie}-{FraNum} del {Fch:dd/MM/yy} a {RaoSocial} per {Total:N2} €";
            }
        }


        public class Item : BaseGuid, IModel
        {
            public string? Concept { get; set; }
            public decimal? Qty { get; set; }
            public decimal? Price { get; set; }
            public decimal? Total => Qty == null ? Price : Qty * Price;

            public Cods Cod { get; set; } = Cods.Linia;

            public Item() { }
            public Item(Cods cod)
            {
               Cod = cod;
            }

            public enum Cods
            {
                Linia, //surt abans del total
                Nota   //surt al final de la pagina desprès del total
            }

            public override string ToString()
            {
                return Concept ?? "{CustomerInvoiceModel.Item}";
                ;
            }
        }

        public class LastFraNum
        {
            public EmpModel.EmpIds Emp { get; set; }
            public int FraNum { get; set; }
            public string? Serial { get; set; }
            public DateOnly Fch { get; set; }
        }

        public class Template : CustomerInvoiceBaseModel, IModel
        {
            public string? Tag { get; set; }

            public Template() : base() { }
            public Template(Guid guid) : base(guid) { }

            public override string ToString() => Tag ?? "{Customer invoice template}";
        }



    }
}
