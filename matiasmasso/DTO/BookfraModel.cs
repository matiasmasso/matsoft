using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BookfraModel : IModel
    {
        public Guid? CcaGuid { get; set; }
        public Guid? ContactGuid { get; set; }
        public Guid? CtaGuid { get; set; }
        public string? FraNum { get; set; }
        public DateOnly? FraFch { get; set; }
        public string? RaoSocial { get; set; }
        public string? NIF { get; set; }

        public List<BaseQuotaModel> Ivas { get; set; } = new();
        public BaseQuotaModel? Irpf { get; set; } 

        public decimal TotalBases => Ivas.Sum(x => x.Base) ?? 0;
        public decimal TipusMig => TotalBases == 0 ? 0 : 100 * TotalQuotas / TotalBases;
        public decimal TotalQuotas => Ivas.Sum(x => x.Quota) ?? 0;
        public decimal TotalLiquid => (Ivas.Sum(x => x.Total) ?? 0) -(Irpf?.Total ?? 0);



        public Guid Guid
        {
            get => (Guid)CcaGuid!;
            set
            {
                //throw new NotImplementedException();
                //CcaGuid = value;
            }
        }

        public static BookfraModel FactoryFromFacturaProveidor(FacturaProveidorModel factura, CcaModel cca)
        {
            var retval = new BookfraModel
            {
                CcaGuid = cca.Guid,
                ContactGuid = factura.Proveidor,
                CtaGuid = factura.CtaDeb,
                FraNum = factura.FraNum,
                FraFch = factura.Fch,
                RaoSocial = factura.RaoSocial,
                NIF = factura.NIF,
                Ivas = factura.Ivas
            };
            return retval;
        }

        public bool Matches(string? searchTerm)
        {
            var retval = false;
            if (string.IsNullOrEmpty(searchTerm))
                retval = true;
            else if (!string.IsNullOrEmpty(RaoSocial))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = RaoSocial;
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public override string ToString()
        {
            return $"{FraFch:dd/MM/yy} Fra.{FraNum} de {RaoSocial}";
        }



    }
}
