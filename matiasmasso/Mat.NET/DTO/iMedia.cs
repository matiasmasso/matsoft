using MatHelperStd;
using Newtonsoft.Json;
using System;

namespace DTO
{
    public interface iMedia
    {
        string Hash { get; set; }
        MimeCods Mime { get; set; }
        double Length { get; set; }
        System.Drawing.Size Size { get; set; }
        int HRes { get; set; }
        int VRes { get; set; }
        int Pags { get; set; }
        [JsonIgnore]
        Byte[] Thumbnail { get; set; }
        DateTime Fch { get; set; }
    }
}
