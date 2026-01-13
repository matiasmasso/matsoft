using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class StaffDTO:StaffModel
    {
        public StaffModel? Properties { get; set; }
        public List<NominaModel> Nominas { get; set; } = new();
        public List<CertificatIrpfModel> CertificatsIrpf { get; set; } = new();
        public List<JornadaLaboralModel> JornadasLaborals { get; set; } = new();

    }

    public class StaffModel:ContactModel
    {
        public EmpModel.EmpIds? Emp { get; set; }
        public string? Nom { get; set; }
        public string? Abr { get; set; }
        public DateTime? FchFrom { get; set; }
        public DateTime? FchTo { get; set; }
        public DateTime? Birthday { get; set; }

        public string? NumSS { get; set; }
        public string? Nif { get; set; }

        public StaffPos? Position { get; set; }

        public AddressModel? Address { get; set; }
        public IbanModel? Iban { get; set; }
        public List<ContactModel.Telefon> Telefons { get; set; } = new();
        public List<UserModel> Emails { get; set; } = new();

        public StaffModel() : base() { }
        public StaffModel(Guid guid) : base(guid) { }

        public int? Age()
        {
            int? retval = null;
            if (Birthday != null)
            {
                TimeSpan span = DateTime.Today - (DateTime)Birthday!;
                // Because we start at year 1 for the Gregorian
                // calendar, we must subtract a year here.
                DateTime zeroTime = new DateTime(1, 1, 1);
                retval = (zeroTime + span).Year - 1;

            }
            return retval;
        }

        public int? Seniority()
        {
            int? retval = null;
            if (FchFrom != null)
            {
                var fchTo = FchTo ?? DateTime.Today;
                TimeSpan span = fchTo - (DateTime)FchFrom!;
                // Because we start at year 1 for the Gregorian
                // calendar, we must subtract a year here.
                DateTime zeroTime = new DateTime(1, 1, 1);
                retval = (zeroTime + span).Year - 1;

            }
            return retval;
        }

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Abr + " " + Nom;
                retval = searchTerms.All(x => searchTarget!.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public string AbrOrNom() => string.IsNullOrEmpty(Abr) ? Nom ?? "" : Abr;
        public override string ToString() => (string.IsNullOrEmpty(Abr) ? Nom : Abr) ?? "{Staff}";


        public class StaffPos:BaseGuid
        {
            public LangTextModel Nom { get; set; }

            public StaffPos() : base()
            {
                Nom = new LangTextModel(base.Guid, LangTextModel.Srcs.StaffPos);
            }
            public StaffPos(Guid guid, string? esp = null, string? cat = null, string? eng = null, string? por = null) : base(guid)
            {
                Nom = new LangTextModel(guid, LangTextModel.Srcs.StaffPos);
                Nom.Load(esp, cat, eng, por);
            }

            public override string ToString() => Nom?.Esp?.ToString() ?? "{StaffPos}";
        }

        public NavDTO ContextMenu()
        {
            var retval = new NavDTO();
            retval.AddItem(Globals.PageUrl("Staff", Guid.ToString()), "Ficha", "Fitxa", "Properties");
            retval.AddItem(Globals.PageUrl("Jornada laboral", Guid.ToString()), "Jornada laboral", "Jornada laboral", "Workday");
            retval.AddItem(Globals.PageUrl("Nominas", Guid.ToString()), "Nominas", "Nomines", "Payroll");
            retval.AddItem(Globals.PageUrl("CertificatsIrpf", Guid.ToString()), "Certs.Irpf", "Certs.Irpf", "Irpf certs.");
            return retval;
        }

        public string PropertyPageUrl() => Globals.PageUrl("Staff", Guid.ToString());
    }
}
