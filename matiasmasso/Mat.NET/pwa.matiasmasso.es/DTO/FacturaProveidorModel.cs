using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FacturaProveidorModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public Guid? Proveidor { get; set; }
        public DateOnly? Fch { get; set; }
        public DateOnly? Vto { get; set; }
        public string? FraNum { get; set; }
        public string? Concept { get; set; }
        public decimal? BaseImponible { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Irpf { get; set; }
        public Guid? CtaDeb { get; set; }
        public Guid? CtaHab { get; set; }
        public string? Fpg { get; set; }
        public PndModel.Cfps Cfp { get; set; }

        public Guid? Projecte { get; set; }

        public DocfileModel? Docfile { get; set; }

        public static FacturaProveidorModel Factory(EmpModel.EmpIds emp, DocfileModel docfile)
        {
            return new FacturaProveidorModel
            {
                Emp = emp,
                Fch = DateOnly.FromDateTime(DateTime.Today),
                Docfile = docfile
            };
        }

        public CcaModel BuildCca(UserModel user, List<PgcCtaModel> ctas)
        {
            var retval = CcaModel.Factory(Emp, user, CcaModel.CcdEnum.FacturaProveidor, Fch);
            retval.Concept = Concept;
            retval.Docfile = Docfile;
            retval.Projecte = Projecte;
            retval.AddDebit(BaseImponible ?? 0, ctas.FirstOrDefault(x => x.Guid == CtaDeb)!, Proveidor);
            if (Iva != null && Iva != 0)
            {
                var ctaIva = ctas.FirstOrDefault(x => x.Cod == PgcCtaModel.Cods.IvaSoportatNacional);
                retval.AddDebit(Iva ?? 0, ctaIva!);
            }
            if (Irpf != null && Irpf != 0)
            {
                var ctaIva = ctas.FirstOrDefault(x => x.Cod == PgcCtaModel.Cods.Irpf);
                retval.AddCredit(Irpf ?? 0, ctaIva!);
            }
            retval.AddSaldo(ctas.FirstOrDefault(x => x.Guid == CtaHab)!, Proveidor);
            return retval;
        }

        public decimal Liquid() => (BaseImponible ?? 0) + (Iva ?? 0) - (Irpf ?? 0);

        public PndModel BuildPnd(CcaModel cca)
        {
            var retval = new PndModel()
            {
                Emp = Emp,
                Contact = (Guid)Proveidor!,
                Cca = cca.Guid,
                Cta = (Guid)CtaHab!,
                Fch = (DateOnly)Fch!,
                Vto = Vto ?? (DateOnly)Fch!,
                Eur = Liquid(),
                Fra = FraNum,
                Fpg = Fpg,
                Cfp = Cfp,
                AD = PndModel.ADs.Creditor,
                Status = PndModel.Statuses.pendent
            };
            return retval;
        }
    }
}
