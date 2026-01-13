using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Json;

namespace Spa.ViewModels
{
    public class ContractsViewModel
    {
        private Shared.AppState AppState;
        public string? SearchTerm;
        public List<ContractDTO> Model = new();
        private List<ContractDTO> items = new();
        public ContractDTO.CodiClass? SelectedCodi;

        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
        public delegate void StateChange();
        public event StateChange? OnStateChange;

        public ContractsViewModel(Shared.AppState appstate)
        {
            AppState = appstate;
        }

        public List<ContractDTO.CodiClass> Codis()
        {
            return items.GroupBy(x => x.Codi.Guid).
                Select(y => y.First().Codi).
                ToList();
        }


        public List<ContractDTO> Items()
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return items.Where(x => x.Codi.Guid.Equals(SelectedCodi!.Guid)).ToList();
            else
                 return items.Where(x => x.Matches(SearchTerm)).ToList();
        }

        public void SelectCodi(ContractDTO.CodiClass selectedCodi) 
        {
            SelectedCodi = selectedCodi;
        }

        public async void Fetch(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("Contracts");
                var response = await http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ContractDTO>>(responseText);
                    LoadStatus = Globals.LoadStatusEnum.Loaded;
                    OnStateChange!.Invoke();
                }
                else
                {
                    AppState.ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    LoadStatus = Globals.LoadStatusEnum.Failed;
                    OnStateChange!.Invoke();
                }
            }
            catch (Exception ex)
            {
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "Error on Contracts download",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }
        }
    }
}
