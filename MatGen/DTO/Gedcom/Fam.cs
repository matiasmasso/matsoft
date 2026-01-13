using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gedcom
{
    public class Fam
    {
        public int Id { get;}
        public int? Husb { get; set; }
        public int? Wife { get; set; }
        public int HusbIdx { get; set; } = 0;
        public int WifeIdx { get; set; } = 0;
        public Fch? Fch { get; set; }

        public List<Indi> Children { get; set; } = new();
        public List<Citation> Citations { get; set; } = new();

        public Fam(int id)
        {
            Id = id;
        }

        public string Result()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"0 @F{Id}@ FAM");
            if (Husb != null)
                sb.AppendLine($"1 HUSB @I{Husb}@");
            if (Wife != null)
                sb.AppendLine($"1 WIFE @I{Wife}@");

            var children = Children.OrderBy(x => x.BirthDate?.Value).ToList();
            foreach (var child in children)
            {
                sb.AppendLine($"1 CHIL @I{child.Id}@");
            }

            if(Fch != null)
            {
                sb.AppendLine($"1 MARR");
                if (Fch?.Value != null)
                    sb.AppendLine($"2 DATE {Fch.DisplayValue}");
            }

            foreach(var citation in Citations)
            {
                sb.AppendLine(citation.Result());
            }

            return sb.ToString();
        }
    }
}
