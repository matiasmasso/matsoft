using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Name, excerpt and description content by product
    /// </summary>
    public partial class VwProductText
    {
        public Guid Guid { get; set; }
        public string? NomEsp { get; set; }
        public string? NomCat { get; set; }
        public string? NomEng { get; set; }
        public string? NomPor { get; set; }
        public string? NomLlargEsp { get; set; }
        public string? NomLlargCat { get; set; }
        public string? NomLlargEng { get; set; }
        public string? NomLlargPor { get; set; }
        public string? ExcerptEsp { get; set; }
        public string? ExcerptCat { get; set; }
        public string? ExcerptEng { get; set; }
        public string? ExcerptPor { get; set; }
        public string? ContentEsp { get; set; }
        public string? ContentCat { get; set; }
        public string? ContentEng { get; set; }
        public string? ContentPor { get; set; }
        public string? UrlEsp { get; set; }
        public string? UrlCat { get; set; }
        public string? UrlEng { get; set; }
        public string? UrlPor { get; set; }
    }
}
