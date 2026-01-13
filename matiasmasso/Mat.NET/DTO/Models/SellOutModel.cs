using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public class SellOutModel
    {
        public List<Item> Items { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Country> Countries { get; set; }
        public List<Channel> Channels { get; set; }
        public List<Proveidor> Proveidors { get; set; }
        public List<Rep> Reps { get; set; }
        public List<Cnap> Cnaps { get; set; }

        public SellOutModel()
        {
            Items = new List<Item>();
            Brands = new List<Brand>();
            Countries = new List<Country>();
            Channels = new List<Channel>();
            Proveidors = new List<Proveidor>();
            Reps = new List<Rep>();
            Cnaps = new List<Cnap>();
        }
        public class Item
        {
            public List<Month> Months { get; set; }
            public Guid Customer { get; set; }
            public Guid Sku { get; set; }
        }
        public class Year
        {
            public int Id { get; set; }
            public List<Month> Months { get; set; }
        }

        public class Month
        {
            public int Id { get; set; }
            public int Qty { get; set; }
            public decimal Amt { get; set; }
        }

        public class Cnap : GuidNom
        {
        }
        public class Rep : GuidNom
        {
        }
        public class Proveidor : GuidNom
        {
        }
        public class Brand : GuidNom
        {
            public List<Category> Categories { get; set; }
            public Brand()
            {
                Categories = new List<Category>();
            }

        }

        public class Category : GuidNom
        {
            public List<Sku> Skus { get; set; }
            public Category()
            {
                Skus = new List<Sku>();
            }

        }

        public class Sku : GuidNom
        {
            public string Ean { get; set; }
            public string Ref { get; set; }
        }
        public class Channel : GuidNom
        {
        }
        public class Country : GuidNom
        {
            public List<Zona> Zonas { get; set; }

            public Country()
            {
                Zonas = new List<Zona>();
            }
        }
        public class Zona : GuidNom
        {
            public List<Location> Locations { get; set; }
            public Zona()
            {
                Locations = new List<Location>();
            }
        }
        public class Location : GuidNom
        {
            public List<Customer> Customers { get; set; }
            public Location()
            {
                Customers = new List<Customer>();
            }

        }
        public class Customer : GuidNom
        {
            public Guid Channel { get; set; }


        }

        public class GuidNom
        {
            public Guid Guid { get; set; }
            public String Nom { get; set; }

        }

    }
}
