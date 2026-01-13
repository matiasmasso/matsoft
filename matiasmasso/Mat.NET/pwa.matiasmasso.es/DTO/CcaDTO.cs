using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CcaDTO
    {
    }
    public class CcaModel : BaseGuid, IModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public int Id { get; set; }
        public DateOnly? Fch { get; set; }
        public string? Concept { get; set; }
        public CcdEnum? Ccd { get; set; }
        public int? Cdn { get; set; }

        public Guid? Projecte { get; set; }
        public DocfileModel? Docfile { get; set; }

        public decimal? Amount { get; set; }
        public GuidNom? UsrCreated { get; set; }

        public UsrLogModel? UsrLog { get; set; }

        public List<Item> Items { get; set; } = new();

        public enum CcdEnum
        {
            NotSet = 0,
            AperturaExercisi = 1,
            MigracioPlaComptable = 2,
            Unknown = 3,
            AlbaraBotiga = 5,
            FacturaNostre = 10,
            ReclamacioEfecte = 11,
            Venciment = 12,
            Reemborsament = 14,
            CobramentACompte = 15,
            XecNostre = 16,
            RemesaEfectes = 17,
            VisaCobros = 18,
            RemesaXecs = 19,
            Impagat = 20,
            Cobro = 21,
            IngresXecs = 22,
            XecRebut = 23,
            DespesesRemesa = 25,
            FacturaProveidor = 30,
            Transit = 30004,
            FacturaInsercionsPublicitaries = 31,
            TransferNorma34 = 34,
            Manual = 49,
            Pagament = 50,
            DipositIrrevocableCompra = 56,
            Nomina = 60,
            SegSocTc1 = 61,
            RepComisions = 62,
            IAE = 63,
            IBI = 64,
            InteresosNostreFavor = 70,
            IRPF = 80,
            IVA = 81,
            InventariMensual = 87,
            InventariMensualDesvaloritzacio = 88,
            InmovilitzatAlta = 89,
            Amortitzacions = 90,
            InmovilitzatBaixa = 91,
            TancamentComptes = 96,
            ImpostSocietats = 97,
            TancamentExplotacio = 98,
            TancamentBalanç = 99
        }


        public CcaModel() : base() { }
        public CcaModel(Guid guid) : base(guid) { }

        public static CcaModel Factory(EmpModel.EmpIds? emp, UserModel user, CcdEnum ccd, DateOnly? fch = null)
        {
            var retval = new CcaModel();
            if(emp != null) retval.Emp = (EmpModel.EmpIds)emp;
            retval.Fch = fch == null ? DateOnly.FromDateTime(DateTime.Now) : fch;
            retval.Ccd = ccd;
            retval.UsrLog = UsrLogModel.Factory(user);

            return retval;
        }

        public Item AddDebit(Decimal eur, PgcCtaModel? cta, Guid? contactGuid = null)
        {
            return AddItem(Item.DhEnum.Deb, eur, cta, contactGuid);
        }
        public Item AddCredit(Decimal eur, PgcCtaModel? cta, Guid? contactGuid = null)
        {
            return AddItem(Item.DhEnum.Hab, eur, cta, contactGuid);
        }
        public Item? AddSaldo(PgcCtaModel cta, Guid? contactGuid = null)
        {
            Item? retval = null;
            var deb = Items.Where(x => x.Dh == Item.DhEnum.Deb).Sum(x => x.Eur);
            var hab = Items.Where(x => x.Dh == Item.DhEnum.Hab).Sum(x => x.Eur);
            if (deb != hab)
            {
                var dh = deb > hab ? Item.DhEnum.Hab : Item.DhEnum.Deb;
                var eur = Math.Abs(deb - hab);
                retval = AddItem(dh, eur, cta, contactGuid);
            }
            return retval;
        }
        public Item AddItem(Item.DhEnum dh, Decimal eur, PgcCtaModel? cta, Guid? contactGuid = null)
        {
            var item = new Item
            {
                Dh = dh,
                Eur = eur,
                Cta = cta?.Guid,
                Contact = contactGuid
            };
            Items.Add(item);
            return item;

        }

        public string? FormattedId()=> Fch == null ? null : $"{((DateOnly)Fch).Year:0000}{Id:0000}";
        public string DownloadUrl() => Globals.ApiUrl("cca/pdf", Guid.ToString());
        public string ThumbnailUrl() => Globals.ApiUrl("cca/thumbnail", Guid.ToString());
        public string PropertyPageUrl() => Globals.PageUrl("cca/config", Guid.ToString());

        public bool Cuadra()
        {
            var retval = Items.All(x => x.Dh == Item.DhEnum.Deb || x.Dh == Item.DhEnum.Hab);
            if (retval)
            {
                var deb = Items.Where(x => x.Dh == Item.DhEnum.Deb).Sum(x => x.Eur);
                var hab = Items.Where(x => x.Dh == Item.DhEnum.Hab).Sum(x => x.Eur);
                retval = deb == hab;
            }
            return retval;
        }

        public override bool Matches(string? searchTerm)
        {
            var retval = false;
            if(string.IsNullOrEmpty(searchTerm)) 
                retval = true;
            else if (!string.IsNullOrEmpty(Concept) & Concept!.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase))
                retval = true;
            //todo check for Id and amount
            return retval;
        }


        public class Item : BaseGuid, IModel
        {
            public Guid? Cta { get; set; }
            public Guid? Contact { get; set; }
            public decimal Eur { get; set; }
            public DhEnum Dh { get; set; } = DhEnum.NotSet;

            public enum DhEnum
            {
                NotSet,
                Deb,
                Hab
            }

            public Item() : base() { }
            public Item(Guid guid) : base(guid) { }

            public decimal? Debit() => Dh == DhEnum.Deb ? Eur : null;
            public decimal? Credit() => Dh == DhEnum.Hab ? Eur : null;

            public decimal Amt(PgcCtaModel? cta)
            {
                if (cta?.IsDeutora() ?? false)
                    return Dh == DhEnum.Deb ? Eur : -Eur;
                else
                    return Dh == DhEnum.Hab ? Eur : -Eur;
            }

            public bool IsEmpty() => Eur == 0;
        }
    }
}
