using Components;
using DTO;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Components
{
    public class SessionStateDeprecated
    {
        public Guid Guid { get; set; }
        public string Token { get; set; }
        public  List<string> BrowserHistory { get; set; } = new();

        public List<NavDTO.Model> MenuItems = new();
        public List<NavDTO.Model> SelectedMenuItems = new();
        public NavDTO Nav { get; set; } = new();
        public bool IsLoadingMenu;

        public SessionStateDeprecated(HttpClient http) { 
            Guid = Guid.NewGuid();
        }

    }
}
