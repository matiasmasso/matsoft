using DocumentFormat.OpenXml.Office.CoverPageProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AeatModel:BaseGuid, IModel
    {
        public string? Id { get; set; }
        public string? Dsc { get; set; }
        public AeatModel(Guid guid):base(guid) { }
        public AeatModel() { }

        public List<Item> Items { get; set; } = new();

        public override string ToString()
        {
            return $"{Id} {Dsc}";
        }

        public class Item:BaseGuid, IModel
        {
            public Guid? Parent { get; set; }
            public EmpModel.EmpIds Emp { get; set; }
            public DateOnly Fch { get; set; }
            public DocfileModel? Docfile { get; set; }
            public Item(Guid guid) : base(guid) { }
            public Item() { }
        }
    }
}
