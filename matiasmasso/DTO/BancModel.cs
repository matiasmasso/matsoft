using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DTO
{
    public class BancModel : ContactModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public string? Abr { get; set; }
        public string? Ccc { get; set; }
        public enum Wellknowns
        {
            None,
            CaixaBank
        }

        public BancModel() : base() { }
        public BancModel(Guid guid) : base(guid) { }

        public static BancModel? Wellknown(Wellknowns id)
        {
            BancModel? retval = null;
            switch (id)
            {
                case Wellknowns.CaixaBank:
                    retval = new BancModel(new Guid("C52FA12B-BBA1-4BD8-BB97-334DDB2B12D4"));
                    break;
            }
            return retval;
        }

        public string FormatedCcc() => FormatedCcc(Ccc);
        public static string FormatedCcc(string? ccc) => string.IsNullOrEmpty(ccc) ? "" : Regex.Replace(ccc, ".{4}", "$0 ").Trim();
        public string Norma43Ccc() => string.IsNullOrEmpty(Ccc) ? "" : $"{Ccc.Truncate(4, 8)}{Ccc.Truncate(14, 10)}";

        public override string ToString()
        {
            return Abr ?? "?";
        }
        public class Saldo
        {
            public EmpModel.EmpIds Emp { get; set; }
            public Guid Guid { get; set; }
            public string Abr { get; set; }
            public string Ccc { get; set; }
            public decimal Eur { get; set; }
            public DateTime Fch { get; set; }

            public string LogoUrl() => Globals.ApiUrl("banc/logo", Guid.ToString());
            public string FormattedCcc() => BancModel.FormatedCcc(Ccc);

            public bool Matches(string searchTerm)
            {
                return Abr.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public class Transfer : BaseGuid,IModel
        {
            public Guid? Banc { get; set; }
            public bool Enabled { get; set; } = true;

            public CcaModel Cca { get; set; }
            public List<Item> Items { get; set; } = new();

            public Transfer() : base() { }
            public Transfer(Guid guid) : base(guid) { }

            public string Filename() => $"Sepa transfer {Emp().ToString()} {Fch():yyyy-MM-dd HH:mm}.xml";

            public Amt Total() => new Amt(Items.Where(x => x.Enabled).Sum(y => y.Amount?.Eur) ?? 0);
            public EmpModel.EmpIds Emp() => Cca!.Emp;
            public DateOnly? Fch() => Cca?.Fch;

            public string PropertyPageUrl()
            {
                throw new NotImplementedException();
            }

            public class Item : BaseGuid
            {
                public Guid Beneficiari { get; set; }
                public Amt? Amount { get; set; }
                public Guid? PgcCta { get; set; }
                public Guid? BankBranch { get; set; }
                public string? Ccc { get; set; }
                public string? Concept { get; set; }

                public bool Enabled { get; set; } = true;

                public Item() : base() { }
                public Item(Guid guid) : base(guid) { }

                public CcaModel.Item Ccb()
                {
                    return new CcaModel.Item(Guid)
                    {
                        Contact = Beneficiari,
                        Cta = (Guid)PgcCta!,
                        Dh = CcaModel.Item.DhEnum.Deb,
                        Eur = Amount?.Eur ?? 0
                    };
                }
            }
        }
    }
}

