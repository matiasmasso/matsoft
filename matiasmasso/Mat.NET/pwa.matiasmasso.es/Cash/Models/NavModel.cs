using Microsoft.Win32;

namespace Cash.Models
{
    public class NavModel
    {

        public List<Item> Items { get; set; } = new();

        public class Item
        {
            public Item? Parent { get; set; }
            public string? Nom { get; set; }
            public string? Value { get; set; }
            public Modes Mode { get; set; } = Modes.Navigate;
            public List<Item> Children { get; set; } = new();
            public enum Modes
            {
                Navigate,
                Action
            }
        }

        public static NavModel Factory()
        {
            return new NavModel()
            {
                Items = new List<Item> {
                    new Item{Nom="Perfil", Children=[
                    new Item{Nom="Credencials", Value="/Credencials"}
                    ]
            },
            new Item{Nom="Administració", Children=[
                new Item{Nom="Comptes", Value ="/Sumasysaldos"},
                new Item{Nom="SumasySaldos2", Value ="/Sumasysaldos2"},
                new Item{Nom="Targes de crèdit", Value = "/VisaCards"},
                new Item{Nom="Norma 43", Value="/Norma43"},
                new Item{Nom="Actius financers", Value="/Actius"},
                new Item{Nom="Apertura d'any", Value="/Exercici"},
                new Item{Nom="Assentaments programats", Value="/CcaScheds"},
                ]
            },
            new Item{Nom="Contactes", Value="/Contacts"},
            new Item{Nom="Bancs", Value="/Bancs"},
            new Item{Nom="Inventari", Children=[
                new Item{Nom="Immobles",Value="/Immobles"},
                new Item{Nom="Vehicles",Value="/Vehicles"}
                ]
            },
            new Item{Nom="Arxiu", Children=[
                new Item{Nom="Empreses",Value="/Emps"},
                new Item{Nom="Escriptures",Value="/Escripturas"},
                new Item{Nom="Contractes",Value="/Contracts"},
                new Item{Nom="Models Hisenda", Value="/AeatMods"},
                new Item{Nom="Atlas", Value="/Atlas"}
                ]
            },
            new Item{Nom="Importar fitxer",Value="/ImportMedia"},
            new Item{Nom="Desenvolupador", Children =[
                 new Item{Nom = "Toggle local api",Value="ToggleLocalApi",Mode= Item.Modes.Action}
                ]}
            }
            };
        }
    }
}
