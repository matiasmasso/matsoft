using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gedcom
{
    public class Citation
    {
        public Sour? Sour { get; set; }
        public string? Page { get; set; }
        public string? Title { get; set; }
        public string? Role { get; set; }
        public string? Url { get; set; }

        public string Result()
        {
            var sb = new StringBuilder();
            if (Sour != null) sb.AppendLine($"2 SOUR @S{Sour.Id}@");
            var segments = new List<string?> { Page, Title, Url }.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (segments.Count > 0)
            {
                var content = string.Join(", ", segments);
                sb.AppendLine($"3 PAGE {content}");
                if(!string.IsNullOrEmpty(Role)) sb.AppendLine($"3 ROLE {Role}");
            }
            return sb.ToString();
        }

    }
}
