using System;

namespace DTO
{
    public class DTOPlantilla : DTOBaseGuid
    {
        public DTODocFile DocFile { get; set; }
        public enum Wellknowns
        {
            NotSet,
            BritaxSatRecallForm,
            BritaxSatRecallShippingLabel,
            ElCorteInglesModificacionDept053,
            ElCorteInglesModificacionDept068,
            ElCorteInglesModificacionDept934
        }

        public DTOPlantilla() : base() { }
        public DTOPlantilla(Guid guid) : base(guid) { }
        public static DTOPlantilla Wellknown(Wellknowns id)
        {
            DTOPlantilla retval = null;
            switch (id)
            {
                case Wellknowns.BritaxSatRecallForm:
                    retval = new DTOPlantilla(new Guid("9C005F1E-C8D3-4747-8402-2900C8592AE4"));
                    break;
                case Wellknowns.BritaxSatRecallShippingLabel:
                    retval = new DTOPlantilla(new Guid("3D1EE923-333C-4587-9844-9496446FADBA"));
                    break;
                case Wellknowns.ElCorteInglesModificacionDept053:
                    retval = new DTOPlantilla(new Guid("C10824C7-6AAC-4B00-AA1F-153DC993B1B9"));
                    break;
                case Wellknowns.ElCorteInglesModificacionDept068:
                    retval = new DTOPlantilla(new Guid("77495F37-3153-4618-9EFA-11304D2313B0"));
                    break;
                case Wellknowns.ElCorteInglesModificacionDept934:
                    retval = new DTOPlantilla(new Guid("FFCF812B-3B4A-4FD1-B9B4-6EC4A9644768"));
                    break;
            }
            return retval;
        }
    }
}
