using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CustomerDeptModel:BaseGuid, IModel
    {
        public Guid Customer { get; set; } //customer owning the department
        public string? Cod { get; set; } //code assigned by the customer to his department
        public string? Nom { get; set; } //customer department name

        public List<Guid> Products { get; set; } = new(); //either categories or skus from our catalogue

        public CustomerDeptModel() : base() { }
        public CustomerDeptModel(Guid guid) : base(guid) { }

        public static CustomerDeptModel Factory(BaseGuid customer) => new CustomerDeptModel
        {
            Customer = customer.Guid,
            Cod="(codi del nou departament)",
            Nom="(Nom del nou departament)"
        };

        public string PropertyPageUrl()
        {
            throw new NotImplementedException();
        }

        public override bool Matches(string? searchTerm)
        {
            bool retval = true;
            if (!string.IsNullOrWhiteSpace(searchTerm))
                retval = (Nom?.Contains(searchTerm) ?? false) || (Cod?.Contains(searchTerm) ?? false);
            return retval;
        }

    }
}
