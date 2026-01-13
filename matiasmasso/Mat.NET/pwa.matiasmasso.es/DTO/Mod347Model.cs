using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Mod347Model
    {
        public List<Item> Items { get; set; } = new();
        public List<Apunt> Apunts { get; set; } = new();
        public enum Modes
        {
            In,
            Out
        }

        public class Item
        {
            public Modes Mode { get; set; }
            public string? NIF { get; set; }
            public string? Nom { get; set; }
            public List<Quarter> Quarters { get; set; } = new();

            public string Clau() => Mode == Modes.In ? "A" : "B";
            public decimal Total() => Quarters.Sum(x => x.Eur);
            public decimal? Q(int qarterId) => Quarters.FirstOrDefault(x => x.Id == qarterId)?.Eur;

        }

        public class Quarter
        {
            public int Id { get; set; }
            public decimal Eur { get; set; }
        }

        public class Apunt
        {
            public Guid Cta { get; set; }
            public Guid Contact { get; set; }
            public DateOnly Fch { get; set; }
            public Guid Cca { get; set; }
            public string? Concept { get; set; }
            public decimal Eur { get; set; }
            public CcaModel.Item.DhEnum Dh { get; set; }
        }
    }
}
