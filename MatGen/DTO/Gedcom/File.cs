using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gedcom
{
    public class File
    {
        public List<Indi> Indis { get; set; } = new();
        public List<Fam> Fams { get; set; } = new();
        public List<Sour> Sours { get; set; } = new();
        public List<Repo> Repos { get; set; } = new();

        public Indi AddIndi(Guid guid)
        {
            var retval = new Indi(guid, Indis.Count + 1);
            Indis.Add(retval);
            return retval;
        }
        public Fam AddFam(Indi? husb, Indi? wife, int? husbIdx, int? wifeIdx)
        {
            var retval = new Fam(Fams.Count + 1);
            retval.Husb = husb?.Id;
            retval.Wife = wife?.Id;
            retval.HusbIdx = husbIdx ?? 0;
            retval.WifeIdx = wifeIdx ?? 0;
            husb?.Fams.Add(retval);
            wife?.Fams.Add(retval);
            Fams.Add(retval);
            return retval;
        }


        public Sour AddSour(Guid guid)
        {
            var retval = new DTO.Gedcom.Sour(guid, Sours.Count + 1);
            Sours.Add(retval);
            return retval;
        }

        public Repo AddRepo(Guid guid)
        {
            var retval = new Repo(guid, Repos.Count + 1);
            Repos.Add(retval);
            return retval;
        }

        public bool IsMissingFam(Indi? husb, Indi? wife) => !Fams.Any(x => x.Husb == husb?.Id && x.Wife == wife?.Id);

        public Fam? Fam(Indi? husb, Indi? wife) => Fams.FirstOrDefault(x=> x.Husb == husb?.Id && x.Wife == wife?.Id);
        
        public byte[] Bytes()
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine(Header());

            foreach (var indi in Indis)
            {
                sb.Append(indi.Result());
            }

            foreach (var fam in Fams)
            {
                sb.Append(fam.Result());
            }

            foreach (var sour in Sours)
            {
                sb.Append(sour.Result());
            }

            foreach (var repo in Repos)
            {
                sb.Append(repo.Result());
            }


            sb.AppendLine(Trailer());
            var retval = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return retval;
        }

        private string Header()
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("0 HEAD");
            sb.AppendLine("1 GEDC");
            sb.AppendLine("2 VERS 5.5.5");
            sb.AppendLine("2 FORM LINEAGE-LINKED");
            sb.AppendLine("3 VERS 5.5.5");
            sb.AppendLine("1 CHAR UTF-8");
            sb.AppendLine("1 SOUR MatGen");
            sb.AppendLine($"1 DATE {DateTime.Today:d MMM yyyy}");
            sb.AppendLine($"1 TIME {DateTime.Now:HH:mm:ss}");
            sb.AppendLine("1 LANG Catalan");
            sb.AppendLine("1 SUBM @U1@");
            sb.AppendLine("0 @U1@ SUBM");
            sb.AppendLine("1 NAME Matias Massó");
            sb.AppendLine("2 CITY Barcelona");
            sb.AppendLine("2 STAE Barcelona");
            sb.AppendLine("2 CTRY Spain");
            return sb.ToString();
        }

        private string Trailer() => "0 TRLR";

    }
}
