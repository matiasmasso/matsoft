using DTO;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Shop4moms.Services
{
    public class StringsLocalizerService
    {
        public List<StringLocalizerModel>? Values;
        public DbState State { get; set; } = DbState.StandBy;
        public event Action? OnChange;

        private AppStateService appstate;

        public StringsLocalizerService(AppStateService appstate)
        {
            this.appstate = appstate;
            Task task = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            State = DbState.IsLoading;
            Values = await appstate.GetAsync<List<StringLocalizerModel>>("StringsLocalizer") ?? new();
            State = DbState.StandBy;
            OnChange?.Invoke();
        }

        public string Localize(LangDTO lang, string stringKey, params object?[] parameters)
        {
            var retval = Values?
                .Where(x => x.StringKey == stringKey)
                .FirstOrDefault()?.Tradueix(lang) ?? stringKey;
            if (parameters.Length > 0)
                retval = string.Format(retval, parameters);
            return retval;
        }

        public async Task UpdateAsync(StringLocalizerModel value)
        {
            await appstate.PostAsync<StringLocalizerModel, bool>(value, "StringLocalizer");
        }
    }
}
