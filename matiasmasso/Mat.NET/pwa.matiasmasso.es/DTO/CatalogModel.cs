using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CatalogModel
    {
        public DateTime? Fch { get; set; }
        public bool IsLoading { get; set; } = true;

        public CatalogModel Request() => new CatalogModel { Fch = Fch };

    }
}
