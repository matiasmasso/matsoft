namespace DTO
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
                    new Item{Nom="Persones", Value="/Persons"},
                    new Item{Nom="Enllaços", Value="/Enlaces"},
                    new Item{Nom="Documents", Value="/Docs"},
                    new Item{Nom="Fonts", Value="/DocSrcs"},
                    new Item{Nom="Noms de pila", Value="/Firstnoms"},
                    new Item{Nom="Cognoms", Value="/Cognoms"},
                    new Item{Nom="Poblacions", Value="/Locations"},
                    new Item{Nom="Codis de documents", Value="/DocCods"},
                    new Item{Nom="Relacions", Value="/DocRels"},
                    new Item{Nom="Professions", Value="/Professions"},
                    new Item{Nom="Publicacions", Value="/Pubs"},
                    new Item{Nom="Repositoris", Value="/Repos"},
                    new Item{Nom="Summary", Value="/"},
                    new Item{Nom="Album", Value="/Album"},
                    new Item{Nom="Importar fitxer",Value="/ImportMedia"},
                    new Item{Nom="Desenvolupador", Children =[
                        new Item{Nom = "Toggle local api",Value="ToggleLocalApi",Mode= Item.Modes.Action},
                        new Item{Nom="Test", Value="/Test"}
                ]}
            }
            };
        }
    }
}
