using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TransmissionModel:BaseGuid, IModel
    {
        public EmpModel.EmpIds Emp { get; set; }
        public int Id { get; set; }
        public DateTime Fch { get; set; }

        public Guid Mgz { get; set; }
        public List<DeliveryModel> Deliveries { get; set; } = new();

        public int AlbsCount { get; set; }
        public int AlbsInvoicePending { get; set; }
        public int LinsCount { get; set; }
        public int UnitsCount { get; set; }
        public decimal Eur { get; set; }

        public TransmissionModel() : base() { }
        public TransmissionModel(Guid guid) : base(guid) { }

        public static TransmissionModel Factory(List<DeliveryModel> deliveries)
        {
            var retval = new TransmissionModel();
            retval.Fch = DateTime.Now;
            retval.Emp = EmpModel.EmpIds.MatiasMasso;
            retval.Mgz = MgzModel.Default()!.Guid;
            retval.Deliveries = deliveries;
            return retval;
        }

        public string PropertyPageUrl() => Globals.PageUrl("to do", Guid.ToString());
        public string Caption() => string.Format("Transm.{0}/{1}", Id, Fch.Year);

        public string Caption(LangDTO? lang = null)
        {
            if (lang == null)
                return $"Transm.{Id}/{Fch.Year}";
            else
                return $"{lang.Tradueix("Transmisión", "Transmisió", "Transmission")} {Id}/{Fch.Year}";
        }
        public override string ToString() => Caption();

        public override bool Matches(string? searchterm)
        {
            return string.IsNullOrEmpty(searchterm) ? false : Id.ToString().Contains(searchterm);
        }
    }
}
