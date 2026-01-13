using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class MgzModel:BaseGuid
    {
        public string? Abr { get; set; }

        public enum Wellknowns:int
        {
            vivaceLliça
        }

        public MgzModel():base() { }
        public MgzModel(Guid guid):base(guid) { }

        public static MgzModel? Default() => Wellknown(Wellknowns.vivaceLliça);

        public static MgzModel? Wellknown(MgzModel.Wellknowns id)
        {
            MgzModel? retval = null;
            switch (id)
            {
                case MgzModel.Wellknowns.vivaceLliça:
                    {
                        retval = new MgzModel(new Guid("41a81aca-1c01-44fc-bf57-2728b03f74d8"))
                        {
                            Abr = "Lliçà d'Amunt"
                        };
                        break;
                    }

            }

            return retval;
        }

    }
}
