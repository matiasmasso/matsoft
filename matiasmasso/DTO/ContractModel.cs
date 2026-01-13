using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{


    public class ContractModel:BaseGuid,IModel
    {
        public EmpModel.EmpIds? Emp { get; set; }
        public string? Nom { get; set; }
        public string? Num { get; set; }
        public DateOnly? FchFrom { get; set; }
        public DateOnly? FchTo { get; set; }
        public Guid? Codi { get; set; }
        public Guid? Contact { get; set; }
        public DocfileModel? Docfile { get; set; }

        public bool HasPdf { get; set; }

        public ContractModel() : base() { }
        public ContractModel(Guid guid) : base(guid) { }

        public string DownloadUrl()
        {
            return Globals.ApiUrl("contract/pdf", Guid.ToString());
        }

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = Nom + " " +  Num ;
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public bool IsOver()=>FchTo != null && FchTo < DateOnly.FromDateTime( DateTime.Today);

        public override string ToString()
        {
            return string.Format("{0:dd/MM/yy} {1}", FchFrom, Nom);
        }

        public class CodiClass:BaseGuid, IModel
        {
            public string? Nom { get; set; }
            public Guid? Cta { get; set; }
            public CodiClass() : base() { }
            public CodiClass(Guid guid) : base(guid) { }

            public enum Wellknowns
            {
                VehicleInsurances,
                VehicleLeasings
            }

            public static CodiClass? Wellknown(Wellknowns id)
            {
                CodiClass? retval = null;
                switch (id)
                {
                    case Wellknowns.VehicleInsurances:
                        retval = new CodiClass(new Guid("AD36AF34-C87A-4CC0-8CD5-E8E7CC02623B"));
                        //0AC913D6-3902-4EF7-AFF8-CAE71342371F
                        break;
                    case Wellknowns.VehicleLeasings:
                        retval = new CodiClass(new Guid("0AC913D6-3902-4EF7-AFF8-CAE71342371F"));
                        break;
                    default:
                        break;
                }
                return retval;
            }

            public override string ToString()
            {
                return Nom ?? base.ToString() ?? "?";
            }
        }
        public class ContactClass
        {
            public Guid? Guid { get; set; }
            public string? FullNom { get; set; }
        }


    }
}
