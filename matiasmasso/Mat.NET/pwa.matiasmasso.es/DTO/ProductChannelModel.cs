using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductChannelModel:BaseGuid
    {
        public Guid Channel { get; set; }
        public Guid Product { get; set; }
        public int Cod { get; set; }

        public enum Cods
        {
            Inclou,
            Exclou
        }

        public ProductChannelModel() : base() { }
        public ProductChannelModel(Guid guid) : base(guid) { }

    }
}
