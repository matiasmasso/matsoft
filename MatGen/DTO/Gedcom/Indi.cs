using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gedcom
{
    public class Indi
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Givn { get; set; }
        public string? Surn { get; set; }
        public Sexs Sex { get; set; } = Sexs.NotSet;

        public string? Occupation { get; set; }

        public Fch? BirthDate { get; set; }
        public Fch? DeathDate { get; set; }
        public Fch? BurialDate { get; set; }

        public List<Fam> Fams { get; set; } = new();
        public List<Fam> ChildOf { get; set; } = new();
        public List<Event> Events { get; set; } = new();

        public enum Sexs
        {
            NotSet,
            Male,
            Female
        }
        public Indi(Guid guid, int id)
        {
            Guid = guid;
            Id = id;
        }

        public string Result()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"0 @I{Id}@ INDI");
            sb.AppendLine($"1 NAME {Name}");
            if (Surn != null)
                sb.AppendLine($"1 SURN {Surn}");
            if (Givn != null)
                sb.AppendLine($"1 GIVN {Givn}");
            if (Sex !=  Sexs.NotSet)
                sb.AppendLine($"1 SEX {(Sex == Sexs.Male ? "M" : "F")}");
            if(Occupation !=null)
                sb.AppendLine($"1 OCCU {Occupation}");


            if (BirthDate?.Value != null)
            {
                sb.AppendLine($"1 BIRT");
                sb.AppendLine($"2 DATE {BirthDate.DisplayValue}");
                if(BirthDate.Location != null) sb.AppendLine($"2 PLAC {BirthDate.Location}");
                foreach (var citation in BirthDate.Citations)
                {
                    sb.AppendLine(citation.Result());
                }
            }
            if (DeathDate?.Value != null)
            {
                sb.AppendLine($"1 DEAT");
                sb.AppendLine($"2 DATE {DeathDate.DisplayValue}");
                if (DeathDate.Location != null) sb.AppendLine($"2 PLAC {DeathDate.Location}");
                foreach (var citation in DeathDate.Citations)
                {
                    sb.AppendLine(citation.Result());
                }
            }
            if (BurialDate?.Value != null)
            {
                sb.AppendLine($"1 BURI");
                sb.AppendLine($"2 DATE {BurialDate.DisplayValue}");
                if (BurialDate.Location != null) sb.AppendLine($"2 PLAC {BurialDate.Location}");
                foreach (var citation in BurialDate.Citations)
                {
                    sb.AppendLine(citation.Result());
                }
            }

            foreach (var evento in Events)
            {
                sb.AppendLine($"1 EVEN");
                sb.AppendLine($"2 TYPE {evento.Type}");
                if (evento.Fch != null)
                sb.AppendLine($"2 DATE {evento.Fch.DisplayValue}");
                foreach (var citation in evento.Citations)
                {
                    sb.AppendLine(citation.Result());
                }
            }


            var fams = Sex == Sexs.Male ? Fams.OrderBy(x => x.HusbIdx).ToList() : Fams.OrderBy(x => x.WifeIdx).ToList();
            foreach (var fam in fams)
            {
                sb.AppendLine($"1 FAMS @F{fam.Id}@");
            }
            foreach (var fam in ChildOf)
            {
                sb.AppendLine($"1 FAMC @F{fam.Id}@");
            }
            return sb.ToString();
        }


    }
}
