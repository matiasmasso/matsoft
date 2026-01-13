using DTO;


namespace Web.Services
{
    public class ContactsService
    {
        public List<ContactModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;

        public ContactsService(AppStateService appstate)
        {
            this.appstate = appstate;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<ContactModel>>("Contacts") ?? new();
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
        public async Task<ContactModel?> FetchAsync(Guid? guid) => guid == null ? null : await appstate.GetAsync<ContactModel?>("Contact", guid.ToString()!);

        public ContactModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public List<ContactModel>? FromNif(string? nif)
        {
            List<ContactModel>? retval = null;
            if (!string.IsNullOrEmpty(nif))
                retval = Values?.Where(x => x.Nifs.Any(y => y.Value == nif)).ToList();
            return retval;
        }

        public async Task UpdateAsync(ContactModel? value)
        {
            if (value != null)
                await appstate.PostAsync<ContactModel, bool>(value, "contact");
        }
    }
}

