using DTO;


namespace Web.Services
{
    public class ChannelDtosOnRrppService
    {
        public List<ImplicitDiscountModel>? Values { get; set; }

        public event Action<Exception?>? OnChange;
        public DbState State { get; set; } = DbState.StandBy;
        private AppStateService appstate;

        public ChannelDtosOnRrppService(AppStateService appstate)
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
                    Values = await appstate.GetAsync<List<ImplicitDiscountModel>>("ChannelDtosOnRrpp") ?? new();
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
                State = DbState.StandBy;
            }
        }

        public ImplicitDiscountModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public List<ImplicitDiscountModel>? ActiveValues(Guid? channel, DateTime? fch = null)
        {
            if(fch == null) fch = DateTime.Now;
            return Values?.Where(x=> x.Target == channel && x.Fch <= fch).OrderByDescending(x=>x.Fch).ToList();
        }

        public async Task<bool> UpdateAsync(ImplicitDiscountModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<ImplicitDiscountModel, bool>(value, "ChannelDtosOnRrpp");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(ImplicitDiscountModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("ChannelDtoOnRrpp/delete", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;
        }
    }
}


