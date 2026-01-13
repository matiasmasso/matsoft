using DocumentFormat.OpenXml.Wordprocessing;
using DTO.Helpers;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PortadaImgModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? NavigateTo { get; set; }
        public int Mime { get; set; }


        public enum Ids
        {
            V1,
            V2,
            V3,
            H1,
            H2,
            H3
        }

        public Box Box(LangDTO lang) => new Box
        {
            Caption = Title ?? "?",
            Url = UrlHelper.RelativeUrl(lang, NavigateTo),
            ImageUrl = Globals.ApiUrl("PortadaImg/img", Id ?? "?")
        };
        public string? Src() => Id == null ? null : Globals.ApiUrl("PortadaImg/img", Id);
    }
}
