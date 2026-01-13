using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class VehicleDTO
    {
        public VehicleModel? Model { get; set; }
        public List<GuidNom> CarModels { get; set; } = new();
        public List<GuidNom> Contracts { get; set; } = new();
    }
    public class VehicleModel : BaseGuid,IModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public GuidNom? Model { get; set; }
        public string? Matricula { get; set; }
        public string? Bastidor { get; set; }
        public GuidNom? Conductor { get; set; }
        public GuidNom? Venedor { get; set; }
        public DateOnly? Alta { get; set; }
        public DateOnly? Baixa { get; set; }
        public GuidNom? Contract { get; set; }
        public GuidNom? Insurance { get; set; }
        public int? ImageMime { get; set; }
        public bool Privat { get; set; }

        public List<DownloadTargetModel> Docfiles { get; set; } = new();
        public List<MultaModel> Multas { get; set; } = new();

        public enum Wellknowns
        {
            None,
            Amg
        }

        public VehicleModel() : base() { }
        public VehicleModel(Guid guid) : base(guid) { }

        public VehicleModel? Wellknown(Wellknowns wellknown)
        {
            VehicleModel? retval = null;
            switch(wellknown)
            {
                case Wellknowns.Amg:
                    retval = new VehicleModel(new Guid("42822287-4F9A-4095-82E6-EA270F3271F5"));
                    break;
            }
            return retval;
        }

        public string Url()
        {
            return string.Format("vehicle/{0}", Guid.ToString());
        }

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.Split("+", StringSplitOptions.RemoveEmptyEntries);
                var searchTarget = ToString();
                retval = searchTerms.All(x => searchTarget.Contains(x, StringComparison.OrdinalIgnoreCase));
            }
            return retval;
        }

        public string PropertyPageUrl() => string.Format("PgVehicle/{0}", Guid.ToString());
        public string ImgUrl() => Globals.ApiUrl("vehicle/image", Guid.ToString());
        public string Caption() => string.Format("{0} {1} {2}", Matricula, Model?.Nom ?? "", Conductor?.Nom ?? "");
        public override string ToString() => Caption();




        public class Brand : BaseGuid
        {
            public string Nom { get; set; } = string.Empty;

            public Brand() : base() { }
            public Brand(Guid guid) : base(guid) { }

            public override string ToString()
            {
                return Nom ?? base.ToString() ?? "";
            }
        }

        public class CarModel : BaseGuid
        {
            public Brand Brand { get; set; }
            public string Nom { get; set; } = string.Empty;

            public CarModel() : base() { }
            public CarModel(Guid guid) : base(guid) { }

            public override string ToString()
            {
                return Nom ?? base.ToString() ?? "";
            }
        }
    }
}
