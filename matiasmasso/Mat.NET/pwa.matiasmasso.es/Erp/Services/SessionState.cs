using Components;
using DTO;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Erp.Services
{
    public class SessionState
    {
        public Guid Guid { get; set; }
        public List<string> BrowserHistory { get; set; } = new();

        public List<NavDTO.Model> MenuItems = new();
        public List<NavDTO.Model> SelectedMenuItems = new();
        public NavDTO Nav { get; set; } = new();
        public bool IsLoadingMenu;

        public SessionState(HttpClient http)
        {
            Guid = Guid.NewGuid();
        }

    }
}
