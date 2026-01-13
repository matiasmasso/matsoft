using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Models
{
    public class MarginsModel
    {
        public List<Brand> Brands { get; set; }

        public enum Modes
        {
            Full,
            Customer,
            Ccx,
            Holding,
            Proveidor
        }

        public Decimal Purchases()
        {
            return Brands.Sum(x => x.Purchases());
        }
        public Decimal Sales()
        {
            return Brands.Sum(x => x.Sales());
        }

        public Decimal GrossProfit()
        {
            return Sales() - Purchases();
        }

        public decimal Margin()
        {
            return Math.Round(Sales() / Purchases() - 1, 2);
        }

        public class Brand
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public List<Category> Categories { get; set; }

            public Decimal Purchases()
            {
                return Categories.Sum(x => x.Purchases());
            }
            public Decimal Sales()
            {
                return Categories.Sum(x => x.Sales());
            }

            public Decimal GrossProfit()
            {
                return Sales() - Purchases();
            }

            public decimal Margin()
            {
                return Math.Round(Sales() / Purchases() - 1, 2);
            }

        }
        public class Category
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public List<Sku> Skus { get; set; }

            public Decimal Purchases()
            {
                return Skus.Sum(x => x.Purchases());
            }
            public Decimal Sales()
            {
                return Skus.Sum(x => x.Sales());
            }

            public Decimal GrossProfit()
            {
                return Sales() - Purchases();
            }

            public decimal Margin()
            {
                return Math.Round(Sales() / Purchases() - 1, 2);
            }

        }
        public class Sku
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public List<Item> Items { get; set; }

            public Decimal Purchases()
            {
                return Items.Sum(x => x.Purchases());
            }
            public Decimal Sales()
            {
                return Items.Sum(x => x.Sales());
            }

            public Decimal GrossProfit()
            {
                return Sales() - Purchases();
            }

            public decimal Margin()
            {
                return Math.Round(Sales() / Purchases() - 1, 2);
            }

            public int Qty()
            {
                return Items.Sum(x => x.Qty);
            }

            public decimal CostAverage()
            {
                int qty = Qty();
                decimal retval = qty == 0 ? 0 : Purchases() / qty;
                return retval;
            }

            public decimal SalePriceAverage()
            {
                int qty = Qty();
                decimal sumPrices = 0;
                foreach (Item item in Items)
                {
                    sumPrices += item.Qty * item.Eur;
                }
                decimal retval = qty == 0 ? 0 : sumPrices / qty;
                return retval;
            }
            public decimal DtoAverage()
            {
                int qty = Qty();
                decimal sumDtos = 0;
                foreach (Item item in Items)
                {
                    sumDtos += item.Qty * item.Dto;
                }
                decimal retval = qty == 0 ? 0 : sumDtos / qty;
                return retval;
            }

        }
        public class Item
        {
            public Guid AlbGuid { get; set; }
            public int Alb { get; set; }
            public DateTime Fch { get; set; }
            public int Qty { get; set; }
            public Decimal Pmc { get; set; }
            public Decimal Eur { get; set; }
            public Decimal Dto { get; set; }

            public Decimal Purchases()
            {
                return Qty * Pmc;
            }
            public Decimal Sales()
            {
                return Math.Round(Qty * Eur * (100 - Dto) / 100, 2);
            }

            public Decimal GrossProfit()
            {
                return Sales() - Purchases();
            }

            public decimal Margin()
            {
                return Math.Round(Sales() / Purchases() - 1, 2);
            }

        }
    }
}
