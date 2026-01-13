using DocumentFormat.OpenXml.Bibliography;
using DTO;


namespace Web.Services
{
    public class TransmissionsService
    {
        private AppStateService appstate;

        public TransmissionsService(AppStateService appstate)
        {
            this.appstate = appstate;
        }

        public async Task<List<TransmissionModel>> GetValuesAsync(string year)
        {
            return await appstate.GetAsync<List<TransmissionModel>>("Transmissions", year) ?? new();
        }

        public async Task<List<DeliveryModel>> DeliveriesAsync(TransmissionModel value)
        {
            return await appstate.GetAsync<List<DeliveryModel>>("Transmission/deliveries", value.Guid.ToString()) ?? new();
        }

        public async Task<int> UpdateAsync(TransmissionModel value)
        {
             return await appstate.PostAsync<TransmissionModel, int>(value, "Transmission");
        }


    }
}

