using System;

namespace DTO
{

    public class DTOJsonSchema : DTOBaseGuid
    {
        public string Nom { get; set; }
        public string Json { get; set; }
        public DTOUsrLog UsrLog { get; set; }



        public enum Wellknowns
        {
            NotSet,
            Purchaseorder,
            WtbolLandingPages,
            WtbolStocks,
            Shipments,
            Tracking
        }

        public DTOJsonSchema() : base()
        {
        }


        public DTOJsonSchema(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOJsonSchema Factory(DTOUser oUser)
        {
            DTOJsonSchema retval = new DTOJsonSchema();
            retval.UsrLog = DTOUsrLog.Factory(oUser);
            return retval;
        }


        public static DTOJsonSchema Wellknown(DTOJsonSchema.Wellknowns id)
        {
            DTOJsonSchema retval = null;
            switch (id)
            {
                case DTOJsonSchema.Wellknowns.Purchaseorder:
                    {
                        retval = new DTOJsonSchema(new Guid("02856104-E738-4694-B970-58E62DA9B03C"));
                        break;
                    }

                case DTOJsonSchema.Wellknowns.WtbolLandingPages:
                    {
                        retval = new DTOJsonSchema(new Guid("FB4575AC-A047-4F1C-A86B-011422358012"));
                        break;
                    }

                case DTOJsonSchema.Wellknowns.WtbolStocks:
                    {
                        retval = new DTOJsonSchema(new Guid("969C6008-643A-4295-A69A-2B2D771B69BC"));
                        break;
                    }
                case DTOJsonSchema.Wellknowns.Shipments:
                    {
                        retval = new DTOJsonSchema(new Guid("18A82D88-674F-4ABE-AAC8-6AD5FE15782F"));
                        break;
                    }
                case Wellknowns.Tracking:
                    {
                        retval = new DTOJsonSchema(new Guid("844F982D-A1B4-4D2F-9483-F1291EB2557F"));
                        break;
                    }
            }
            return retval;
        }

    }


}
