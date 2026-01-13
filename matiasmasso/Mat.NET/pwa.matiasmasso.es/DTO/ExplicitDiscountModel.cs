using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    //Explicit discount appears on invoices discount column
    //while implicit discount is used to calculate customer specific price list
    public class ExplicitDiscountModel
    {
        public Guid Customer { get; set; }
        public Guid Product { get; set; }
        public decimal Dto { get; set; }

        public override string ToString()
        {
            return $"{Customer.ToString()} {Product.ToString()} {Dto:N2}%";
        }
    }
}
