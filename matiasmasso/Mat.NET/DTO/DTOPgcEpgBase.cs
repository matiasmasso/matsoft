using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOPgcEpgBase : DTOBaseGuid
    {
        public DTOPgcEpgBase Parent { get; set; }
        public DTOLangText Nom { get; set; }
        public string Ordinal { get; set; }
        public int Cod { get; set; }


        public enum Cods
        {
            NotSet,
            Epg0,
            Epg1,
            Epg2,
            Cta
        }

        public List<DTOPgcEpgBase> Children { get; set; }

        public DTOPgcEpgBase() : base()
        {
            Nom = new DTOLangText();
            Children = new List<DTOPgcEpgBase>();
        }

        public DTOPgcEpgBase(Guid oGuid) : base(oGuid)
        {
            Nom = new DTOLangText();
            Children = new List<DTOPgcEpgBase>();
        }
    }
}
