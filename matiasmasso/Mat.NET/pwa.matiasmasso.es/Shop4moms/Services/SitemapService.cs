
using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Text;
using DTO;
using DTO.Helpers;
using Shop4moms.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;
using System;
using DocumentFormat.OpenXml.Presentation;
using static System.Net.WebRequestMethods;
using DocumentFormat.OpenXml.Office2013.ExcelAc;

namespace Shop4moms.Services
{
    public class SitemapService
    {

        public static List<string> SitemapPaths() => new List<string>()
            {
                "pages.xml",
                "categories.xml",
                "skus.xml",
                "videos.xml",
                "images.xml"
            };

        public static List<LangTextModel> Pages() => new List<LangTextModel>() {
            new LangTextModel(),
            new LangTextModel("videos"),
            new LangTextModel("galeria","galeria","gallery","galeria"),
            new LangTextModel("contacto","contacte","contact","contato")
            };


    }
}
