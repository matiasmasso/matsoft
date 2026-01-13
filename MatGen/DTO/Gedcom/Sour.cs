using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gedcom
{
    public class Sour
    {
        public Guid? Guid { get; set; }

        public int Id { get; set; }
        public string? Titl { get; set; }
        public int? Repo { get; set; }

        public Sour(Guid guid, int id)
        {
            Guid = guid;
            Id = id;
        }

        public string Result()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"0 @S{Id}@ SOUR");
            if (Titl != null) sb.AppendLine($"1 TITL {Titl}");
            if (Repo != null) sb.AppendLine($"1 REPO @R{Repo}@");
            return sb.ToString();
        }
    }
}
