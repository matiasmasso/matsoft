using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Gedcom
{
    public class Repo
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public string? Adr1 { get; set; }
        public string? City { get; set; }
        public string? Stae { get; set; }
        public string? Post { get; set; }
        public string? Ctry { get; set; }

        public Repo(Guid guid, int id)
        {
            Guid = guid;
            Id = id;
        }

        //        0 @R1@ REPO
        //1 NAME Family History Library
        //1 ADDR
        //2 ADR1 35 N West Temple Street
        //2 CITY Salt Lake City
        //2 STAE Utah
        //2 POST 84150
        //2 CTRY United States of America
        public string Result()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"0 @R{Id}@ REPO");
            if (Name != null) sb.AppendLine($"1 NAME {Name}");
            if (!AllNull(Adr1, City, Stae, Post, Ctry))
            {
                sb.AppendLine($"1 ADDR");
                if (Adr1 != null) sb.AppendLine($"2 ADR1 {Adr1}");
                if (City != null) sb.AppendLine($"2 CITY {City}");
                if (Stae != null) sb.AppendLine($"2 STAE {Stae}");
                if (Post != null) sb.AppendLine($"2 POST {Post}");
                if (Ctry != null) sb.AppendLine($"2 CTRY {Ctry}");
            }

            return sb.ToString();
        }

        private static bool AllNull(params object?[] objects)
        {
            return objects.All(s => s == null);
        }


    }
}
