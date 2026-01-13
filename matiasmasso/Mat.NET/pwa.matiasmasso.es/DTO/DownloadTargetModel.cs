using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DownloadTargetModel
    {
        public Guid Target { get; set; }
        public int Cod { get; set; }
        public Srcs Src { get; set; }
        public LangDTO.Set? Langset { get; set; }

        public bool Obsoleto { get; set; }
        public DocfileModel? DocFile { get; set; }
        public bool PublicarAlConsumidor { get; set; }
        public bool PublicarAlDistribuidor { get; set; }


        public enum Srcs
        {
            notSet,
            instrucciones,
            catalogos,
            compatibilidad,
            despiece,
            imatge_Alta_Resolucio,
            certificat_Homologacio,
            publicacions,
            seguro,
            justificant,
            documentacio,
            proforma
        }

        public string PageUrl()=>Globals.PageUrl("TargetDownload",Target.ToString());
    }
}
