using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class InvoiceSentModel:BaseGuid, IModel
    {
        public int Id { get; set; }
        public Series Serie { get; set; }
        public DateTime Fch { get; set; }
        public Guid? Contact { get; set; }
        public Amt? Amt { get; set; }
        public string? Fpg { get; set; }
        public string? TipoFactura { get; set; }
        public string? SiiL9 { get; set; }
        public int SiiResult { get; set; }
        public string? RegimenEspecialOTrascendencia { get; set; }
        public int Concepte { get; set; }
        public int PrintMode { get; set; }

        public List<Item> Items { get; set; } = new();
        public List<DeliveryModel> Deliveries { get; set; } = new();

        public enum Series
        {
            Standard,
            Rectificativa,
            Simplificada,
            InversionSujetoPasivo
        }

        public InvoiceSentModel() : base() { }
        public InvoiceSentModel(Guid guid) : base(guid) { }

        public string SerieYNum()
        {
            string retval = Id.ToString();
            switch (Serie)
            {
                case Series.Rectificativa:
                    retval = $"R{Id}";
                    break;
                case Series.Simplificada:
                    retval = $"S{Id}";
                    break;
                case Series.InversionSujetoPasivo:
                    retval = $"Y{Id}";
                    break;
            }
            return retval;
        }
        public string FormattedId()
        {
            string retval = $"{Fch.Year}{Id:000000}";
            switch (Serie)
            {
                case Series.Rectificativa:
                    retval = $"R{retval}";
                    break;
                case Series.Simplificada:
                    retval = $"S{retval}";
                    break;
                case Series.InversionSujetoPasivo:
                    retval = $"Y{retval}";
                    break;
            }
            return retval;
        }

        public bool HasDiscount() => Items.Any(x => x.Dto != null && x.Dto != 0);
        public class Item:BaseGuid, IModel
        {
            public int? Lin { get; set; }
            public int? Qty { get; set; }
            public decimal? Price { get; set; }
            public decimal? Dto { get; set; }
            public decimal? Amt() => Math.Round((Qty ?? 0) * (Price ?? 0) * (100 - (Dto ?? 0)) / 100, 2);
            public Guid? Sku { get; set; }

            public GuidNom? PurchaseOrder { get; set; }
            public GuidNom? Delivery { get; set; }

            public InvoiceSentModel? Parent { get; set; }
            public Item() : base() { }
            public Item(Guid guid) : base(guid) { }

        }

    }


    public class InvoiceSentListDTO
    {
        public List<Item> Items { get; set; } = new();
        public List<IdEur> Years { get; set; } = new();



        public class Item
        {
            public Guid Guid { get; set; }
            public int Id { get; set; }
            public int Serie { get; set; }
            public DateTime Fch { get; set; }
            public GuidNom? Contact { get; set; }
            public Amt? Amt { get; set; }
            public string Fpg { get; set; }
            public string TipoFactura { get; set; }
            public string SiiL9 { get; set; }
            public int SiiResult { get; set; }
            public string RegimenEspecialOTrascendencia { get; set; }
            public int Concepte { get; set; }
            public int PrintMode { get; set; }


            public bool Matches(string? searchTerm)
            {
                bool retval = true;
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                    var searchTarget = Contact?.Nom ?? "" + " " + Id.ToString() ?? "";
                    retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
                }
                return retval;
            }


            public string PageUrl() => Globals.PageUrl("invoiceSent", Guid.ToString());
            public override string ToString() => string.Format("{InvoiceSentListDTO.Item: fra.{0:00000} del {1:dd/MM/yy}", Id, Fch);
        }
    }
}
