using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BalanceModel
    {
        public EmpModel Emp { get; set; }
        public Modes Mode { get; set; }
        public DateTime Fch { get; set; }
        public List<Item> Items { get; set; } = new();

        public enum Modes
        {
            Assets, // actiu
            Liabilities, // passiu
            PL // compte d'explotacio
        }

        public BalanceModel(EmpModel emp, DateTime fch, Modes mode)
        {
            Emp = emp;
            Fch = fch;
            Mode = mode;
        }

        public Item? GetItem(PgcClassModel.Cods epigrafCod) => Items.FirstOrDefault(x => x.Tag is PgcClassModel && ((PgcClassModel)x.Tag).Cod == epigrafCod);

        public decimal Saldo(Item item) {
            decimal retval = 0;
            switch (Mode)
            {
                case Modes.Assets:
                    retval = item.Deb - item.Hab;
                    break;
                case Modes.Liabilities:
                case Modes.PL:
                    retval = item.Hab - item.Deb;
                    break;
            }
            return retval;
        }

        public class Item
        {
            public object? Tag { get; set; }
            public Item? Parent { get; set; }
            public LangTextModel? Nom { get; set; }
            public decimal Deb { get; set; }
            public decimal Hab { get; set; }
            public int Level { get; set; }

            public bool Visible { get; set; } = true;

            public bool IsEmpty() => Deb == Hab;

            public bool HideFigures()=> Tag is PgcClassModel && ((PgcClassModel)Tag).HideFigures;

            public string ExcelPadding() => new string(' ', 10 * Level);

            public override string ToString()
            {
                return Nom?.Esp ?? "?";
            }

        }
        public enum Tabs
        {
            Assets,
            Liabilities,
            PL
        }




    }
}
