using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTODeliveryTraspas : DTOBaseGuid
    {
        public DTOEmp Emp { get; set; }
        public int Id { get; set; }
        public DateTime Fch { get; set; }
        public DTOMgz MgzFrom { get; set; }
        public DTOMgz MgzTo { get; set; }
        public List<DTODeliveryItem> Items { get; set; }
        public DTOUsrLog UsrLog { get; set; }

        public DTODeliveryTraspas() : base()
        {
            Items = new List<DTODeliveryItem>();
        }

        public DTODeliveryTraspas(Guid oGuid) : base(oGuid)
        {
            Items = new List<DTODeliveryItem>();
        }

        public static DTODeliveryTraspas Factory(DTOUser oUser, DTOMgz oMgzFrom, DTOMgz oMgzTo, DateTime DtFch = default(DateTime))
        {
            if (DtFch == default(DateTime))
                DtFch = DTO.GlobalVariables.Today();
            DTODeliveryTraspas retval = new DTODeliveryTraspas();
            {
                var withBlock = retval;
                withBlock.Emp = oUser.Emp;
                withBlock.MgzFrom = oMgzFrom;
                withBlock.MgzTo = oMgzTo;
                withBlock.Fch = DtFch;
                withBlock.UsrLog = DTOUsrLog.Factory(oUser);
            }
            return retval;
        }
    }
}
