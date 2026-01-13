using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    //Implicit discount is used to calculate customer price list.
    //This discount is implicit since it does not appear anywhere
    //while Explicit discount appears on invoices discount column
    public class ImplicitDiscountModel:BaseGuid
    {
        public Guid Target { get; set; } //CustomerOrChannel
        public TargetCods TargetCod { get; set; }
        public DateTime Fch { get; set; }
        public Guid? Product { get; set; }
        public decimal Dto { get; set; }
        public string? Obs { get; set; }

        public enum TargetCods
        {
            notSet,
            canal,
            client
        }

        public ImplicitDiscountModel() : base() { }
        public ImplicitDiscountModel(Guid guid) : base(guid) { }


    }


}
