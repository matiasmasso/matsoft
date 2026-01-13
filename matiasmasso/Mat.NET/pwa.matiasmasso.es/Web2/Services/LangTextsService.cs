using DocumentFormat.OpenXml.Presentation;
using DTO;
using Microsoft.AspNetCore.Components.Server;
using System;
using Web.LocalComponents;

namespace Web.Services
{
    public class LangTextsService
    {
        public List<LangTextModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;

        private AppStateService appstate;

        public LangTextsService(AppStateService appstate)
        {
            this.appstate = appstate;
            Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<LangTextModel>>("LangTexts") ?? new();
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

        public List<LangTextModel>? GetValuesFromContent(LangTextModel.Srcs src, LangDTO lang, string? content)
        {
            var searchTerm = content?.ToLower();
            var retval = Values?.Where(x => x.Src == src && x.Tradueix(lang)?.ToLower() == searchTerm).ToList();
            return retval;
        }

        public LangTextModel? GetValue(Guid? guid, LangTextModel.Srcs src)
        {
            var retval = Values?.FirstOrDefault(x => x.Guid == guid && x.Src == src);
            return retval;
        }

        public string? Segment(Guid? guid, LangDTO lang) => Values?
            .FirstOrDefault(x => x.Guid == guid && x.Src == LangTextModel.Srcs.ProductUrl)?
            .Tradueix(lang);

    }
}
