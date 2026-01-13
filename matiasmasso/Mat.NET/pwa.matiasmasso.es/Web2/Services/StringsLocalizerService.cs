using DocumentFormat.OpenXml.EMMA;
using DTO;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Web.Services;

namespace Web.Services
{
    public class StringsLocalizerService
    {
        public List<StringLocalizerModel>? Values;
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;

        private AppStateService appstate;

        public StringsLocalizerService(AppStateService appstate)
        {
            this.appstate = appstate;
            Task task = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            State = DbState.IsLoading;
            try
            {
                Values = await appstate.GetAsync<List<StringLocalizerModel>>("StringsLocalizer") ?? new();
                State = DbState.StandBy;
                OnChange?.Invoke(null);
            }
            catch (Exception ex)
            {
                State = DbState.Failed;
                OnChange?.Invoke(ex);
            }
        }

        public string Localize(LangDTO lang, string stringKey, params object?[] parameters)
        {
            var retval = Values?
                .Where(x => x.StringKey.Equals(stringKey, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault()?.Tradueix(lang) ?? stringKey;
            if (parameters.Length > 0)
                retval = string.Format(retval, parameters);
            return retval;
        }

        public async Task UpdateAsync(StringLocalizerModel? value)
        {
            if (value != null)
            {
                try
                {
                    await appstate.PostAsync<StringLocalizerModel, bool>(value, "StringLocalizer");
                    if (value.IsNew)
                    {
                        Values?.Add(value);
                        value.IsNew = false;
                        Values = Values?.OrderBy(x => x.StringKey).ToList();
                        OnChange?.Invoke(null);
                    }

                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
            }
        }

        public async Task DeleteAsync(StringLocalizerModel? value)
        {
            if (value != null)
            {
                try
                {
                    await appstate.PostAsync<string, bool>(value.StringKey, "StringLocalizer/delete");
                    State = DbState.StandBy;
                    if (Values!.Remove(value))
                        OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }


            }
        }
    }
}
