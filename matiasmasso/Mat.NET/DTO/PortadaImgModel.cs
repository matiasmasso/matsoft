using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PortadaImgModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string NavigateTo { get; set; }
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

        public string Src() {
            return Id == null ? null : MmoUrl.ApiUrl("PortadaImg/img", Id);
        }
    }
}
