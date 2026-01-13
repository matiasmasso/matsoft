using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
    public class CcaSchedModel : BaseGuid
    {
        public EmpModel.EmpIds Emp { get; set; }
        public string? Concept { get; set; }

        public CcaModel.CcdEnum? Ccd { get; set; }

        public Guid? Projecte { get; set; }
        public DateOnly? FchFrom { get; set; }
        public DateOnly? FchTo { get; set; }
        public DateTime? LastTime { get; set; }
        public FreqMods? FreqMod { get; set; }
        public int? FreqDue { get; set; }

        public List<Item> Items { get; set; } = new();

        public enum FreqMods
        {
            Monthly,
            Weekly,
            Daily
        }

        public CcaSchedModel() { }
        public CcaSchedModel(Guid guid) : base(guid) { }

        public CcaModel Cca(UserModel user)
        {
            var retval = CcaModel.Factory(Emp, user, (CcaModel.CcdEnum)Ccd!);
            retval.Concept = Concept;
            retval.Projecte = Projecte;
            foreach (var item in Items)
            {
                retval.Items.Add(new CcaModel.Item
                {
                    Cta = item.Cta,
                    Contact = item.Contact,
                    Eur = item.Eur ?? 0,
                    Dh = item.Dh
                });
            }
            return retval;
        }
        public bool IsActive() => (FchFrom == null || FchFrom <= DateOnly.FromDateTime(DateTime.Today)) && (FchTo == null || FchTo >= DateOnly.FromDateTime(DateTime.Today));
        public bool IsDue()
        {
            var dueDate = DueDate();
            var retval = dueDate != null && !IsOver(dueDate);
            return retval;
        }

        public bool IsOver(DateOnly? fch) {
            var isFuture =  fch.HasValue && fch > DateOnly.FromDateTime(DateTime.Today);
            var hasPasedLimit = fch.HasValue && fch > FchTo;
            var retval = isFuture || hasPasedLimit;
            return retval;
        }

        public DateOnly? DueDate()
        {
            DateOnly? retval = null;
            if (FreqMod == FreqMods.Monthly && FreqDue != null)
            {
                var minFch = MinFch();
                var dueFch = new DateTime(minFch.Year, minFch.Month, (int)FreqDue).ToDateOnly();
                if (dueFch >= minFch)
                    retval = dueFch;
                else
                    retval = dueFch.ToDateTime(new TimeOnly()).AddMonths(1).ToDateOnly();
            }

            return retval;
        }

        public DateOnly MinFch()
        {
            DateOnly? retval;
            if (FchFrom == null && LastTime == null)
                retval = new DateTime(DateTime.Today.Year, 1, 1).ToDateOnly();
            else if (FchFrom == null)
                retval = LastTime.ToDateOnly();
            else if (LastTime == null)
                retval = FchFrom;
            else
                retval = FchFrom > LastTime.ToDateOnly() ? FchFrom : ((DateTime)LastTime!).AddDays(1).ToDateOnly();

            return (DateOnly)retval!;
        }

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Concept; // + " " + SearchKey;
                retval = searchTerms.All(x => searchTarget?.Contains(x, StringComparison.OrdinalIgnoreCase) ?? false);
            }
            return retval;
        }

        public override string ToString()
        {
            return Concept ?? "?";
        }

        public class Item : BaseGuid
        {
            public Guid Cta { get; set; }
            public Guid? Contact { get; set; }
            public CcaModel.Item.DhEnum Dh { get; set; }
            public decimal? Eur { get; set; }
            public int? Lin { get; set; }

            public Item() { }
            public Item(Guid guid) : base(guid) { }


            public decimal? Debit() => Dh == CcaModel.Item.DhEnum.Deb ? Eur : null;
            public decimal? Credit() => Dh == CcaModel.Item.DhEnum.Hab ? Eur : null;


        }
    }
}
