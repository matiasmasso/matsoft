using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{

    public class HomeDTO
    {
        public List<Box> RotatingBanners { get; set; } = new();
        public List<Box> News { get; set; } = new();
        public Box? LastPost { get; set; }
        public List<PortadaImgModel> PortadaImgs { get; set; } = new();
        public RaffleModel? CurrentOrNextRaffle { get; set; }

        public string PortadaImgSrc(PortadaImgModel.Ids id) => PortadaImgs.FirstOrDefault(x => x.Id == id.ToString())?.Src() ?? "";
        public string PortadaImgTitle(PortadaImgModel.Ids id) => PortadaImgs.FirstOrDefault(x => x.Id == id.ToString())?.Title ?? "";
        public string PortadaImgNavigateTo(PortadaImgModel.Ids id) => PortadaImgs.FirstOrDefault(x => x.Id == id.ToString())?.NavigateTo ?? "";
    }


}
