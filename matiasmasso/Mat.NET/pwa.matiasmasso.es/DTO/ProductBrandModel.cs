using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ProductBrandModel:ProductModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public int Ord { get; set; }
        public Guid? Proveidor { get; set; }
        public Guid? Cnap { get; set; }
        public Guid? MadeIn { get; set; }
        public string? CodiMercancia { get; set; }
        public int CodDist { get; set; }
        public bool IncludeDeptOnUrl { get; set; }

        public enum Wellknowns
        {
            BritaxRoemer,
            TommeeTippee,
            fourMoms
        }

        public ProductBrandModel() : base( SourceCods.Brand) { }
        public ProductBrandModel(Guid guid) : base(guid, SourceCods.Brand) { }

        public static ProductBrandModel? Wellknown(Wellknowns id)
        {
            ProductBrandModel? retval = null;
            switch (id)
            {
                case Wellknowns.BritaxRoemer: retval = new ProductBrandModel(new Guid("D4C2BC59-046D-42D3-86E3-BDCA91FB473F")); break;
                case Wellknowns.TommeeTippee: retval = new ProductBrandModel(new Guid("B55B006D-3322-4E41-8CF7-9A02C3503A09")); break;
                case Wellknowns.fourMoms: retval = new ProductBrandModel(new Guid("67058F90-1FD6-4AE6-82ED-78447779B358")); break;
            }
            return retval;
        }

        public bool IsBritax() => Guid == Wellknown(Wellknowns.BritaxRoemer)!.Guid;
        public bool Is4moms() => Guid ==  Wellknown(Wellknowns.fourMoms)!.Guid;
        public bool IsTommeeTippee() => Guid == Wellknown(Wellknowns.TommeeTippee)!.Guid;


        public override string ToString()
        {
            return String.Format("Brand: {0}", Nom.Esp ?? "?");
        }

        public NavDTO ContextMenu()
        {
            NavDTO retval = new NavDTO();
            retval.AddItem("/brand/"+Guid.ToString(), "ficha", "fitxa", "properties");
            retval.AddItem("", "sellout");
            return retval;
        }

        public override bool Matches(string? searchTerm) => Nom?.Contains(searchTerm) ?? false;

    }
}
