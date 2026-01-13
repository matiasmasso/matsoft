using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class IncidenciaModel:BaseGuid, IModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public int Id { get; set; }

        public string Asin { get; set; }
        public DateTime Fch { get; set; }
        public Guid? Customer { get; set; }
        public Guid? Product { get; set; }
        public string? Obs { get; set; }

        public Guid? CodiTancament { get; set; }

        public Srcs Src { get; set; }
        public Guid Codi { get; set; }

        public ContactTypes ContactType { get; set; }
        public string? CustomerNameAddress { get; set; }
        public string? ContactPerson { get; set; }
        public string? EmailAdr { get; set; }
        public string? Tel { get; set; }
        public string? CustomerRef { get; set; }
        public Procedencias Procedencia { get; set; }
        public DateOnly? FchCompra { get; set; }
        public string? BoughtFrom { get; set; }
        public string? SerialNumber { get; set; }
        public string? ManufactureDate { get; set; }
        public string? Description { get; set; }

        public List<DocfileModel> DocFileImages { get; set; } = new();
        public bool ExistImages { get; set; }
        public List<DocfileModel> PurchaseTickets { get; set; } = new();
        public bool ExistTickets { get; set; }
        public List<DocfileModel> Videos { get; set; } = new();
        public bool ExistVideos { get; set; }

        public bool AcceptedConditions { get; set; }

        public Guid Spv { get; set; }
        public DateTime? FchClose { get; set; }
        //public DTOTracking.Collection Trackings { get; set; }
        public UsrLogModel? UsrLog { get; set; }


        public enum Srcs
        {
            notSet,
            Producte,
            transport
        }

        public enum ContactTypes
        {
            notSet,
            professional,
            consumidor
        }

        public enum AttachmentCods
        {
            ticket,
            imatge,
            video
        }

        public enum Procedencias
        {
            notSet,
            myShop,
            otherShops,
            expo
        }


        public IncidenciaModel() : base() {
            Asin = DTO.Helpers.CryptoHelper.RandomString(10);
        }
        public IncidenciaModel(Guid guid) : base(guid) {
            Asin = DTO.Helpers.CryptoHelper.RandomString(10);
        }


        public string PropertyPageUrl() => string.Format("/Incidencia/{0}", Guid.ToString());
        public string Caption() => string.Format("Incidencia {0} del {1:dd/MM/yy}", Guid.ToString(), Fch);
        public override string ToString() => string.Format("Incidencia {0} del {1:dd/MM/yy}", Guid.ToString(), Fch);

    }

}
