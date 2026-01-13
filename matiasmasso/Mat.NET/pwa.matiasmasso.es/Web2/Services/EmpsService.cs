using DocumentFormat.OpenXml.Bibliography;
using DTO;


namespace Web.Services
{
    public class EmpsService
    {
        public List<EmpModel>? Values { get; set; }
        public List<CertModel>? Certs { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;
        private ContactsService contactsService;

        public EmpsService(AppStateService appstate, ContactsService contactsService)
        {
            this.appstate = appstate;
            this.contactsService = contactsService;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<EmpModel>>("Emps") ?? new();

                    foreach(var value in Values.Where(x=>x.Cert != null))
                    {
                        value.Cert!.Data!.Data = await appstate.GetAsync<byte[]>("Emp/cert/Data", ((int)value.Id).ToString());
                        value.Cert!.Image!.Data = await appstate.GetAsync<byte[]>("Emp/cert/Image", ((int)value.Id).ToString());
                    }

                    State = DbState.StandBy;
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
            }
        }


        public EmpModel? GetValue(Guid guid) => Values?.FirstOrDefault(x => x.Guid == guid);
        public EmpModel? GetValue(EmpModel.EmpIds id) =>  Values?.FirstOrDefault(x => x.Id == id);

        public EmpModel? FromNif(string? nif)
        {
            EmpModel? retval = null;
            if (!string.IsNullOrEmpty(nif))
            {
                var contacts = contactsService.FromNif(nif);
                if (contacts != null)
                    retval = Values?.FirstOrDefault(x => contacts.Any(y => x.Org == y.Guid));
            }
            return retval;
        }

        public async Task<bool> UpdateAsync(EmpModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<EmpModel, bool>(value, "Emp");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(EmpModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("Emp/delete", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;

        }



        public byte[] SignPdf(byte[] src, int top, int left, int width)
        {
            return src;
        }
    }
}

