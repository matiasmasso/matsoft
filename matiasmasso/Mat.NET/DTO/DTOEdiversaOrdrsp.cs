using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOEdiversaOrdrsp : DTOBaseGuid
    {
        public DTOEdiversaOrder Order { get; set; }
        public DateTime Fch { get; set; }
        public DateTime FchCreated { get; set; }
        public List<DTOEdiversaOrdrspItem> Items { get; set; }


        public DTOEdiversaOrdrsp() : base()
        {
        }

        public DTOEdiversaOrdrsp(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOEdiversaOrdrsp Factory(DTOEdiversaOrder oEdiversaOrder)
        {
            DTOEdiversaOrdrsp retval = new DTOEdiversaOrdrsp();
            {
                var withBlock = retval;
                withBlock.Order = oEdiversaOrder;
                withBlock.Fch = DTO.GlobalVariables.Today();
                withBlock.Items = new List<DTOEdiversaOrdrspItem>();
            }

            foreach (DTOEdiversaOrderItem orderItem in oEdiversaOrder.Items)
            {
                DTOEdiversaOrdrspItem spItem = new DTOEdiversaOrdrspItem();
                {
                    var withBlock = spItem;
                    withBlock.OrderItem = orderItem;
                    withBlock.Qty = orderItem.Qty;
                }
                retval.Items.Add(spItem);
            }
            return retval;
        }

        public static DTOEdiversaFile EdiFile(DTOEdiversaOrdrsp src)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format("BGM|{0}|231|{1}", src.Order.DocNum, HasChangedCode(src)));
            sb.AppendLine(string.Format("DTM|{0:yyyyMMdd}", src.Fch));
            sb.AppendLine("NADMS"); // us
            sb.AppendLine("NADNR"); // order message sender MadMs
            sb.AppendLine("NADSU");
            sb.AppendLine("NADBY");
            sb.AppendLine("NADDP");
            sb.AppendLine("NADIV");
            sb.AppendLine("CUX");
            foreach (DTOEdiversaOrdrspItem item in src.Items)
            {
                sb.AppendLine("LIN");
                sb.AppendLine("PIALIN");
                sb.AppendLine("QTYLIN");
                sb.AppendLine("DMTLIN");
                sb.AppendLine("PRILIN");
                sb.AppendLine("TAXLIN");
            }
            sb.AppendLine("CNTRES");
            DTOEdiversaFile retval = new DTOEdiversaFile();
            retval.Stream = sb.ToString();
            return retval;
        }

        public static bool HasChanged(DTOEdiversaOrdrsp src)
        {
            bool retval = false;
            foreach (DTOEdiversaOrdrspItem item in src.Items)
            {
                if (item.Qty != item.OrderItem.Qty)
                {
                    retval = true;
                    break;
                }
            }
            return retval;
        }

        public static int HasChangedCode(DTOEdiversaOrdrsp src)
        {
            int retval = 29; // aceptado sin correccion
            if (HasChanged(src))
                retval = 4; // cambio
            return retval;
        }
    }


    public class DTOEdiversaOrdrspItem
    {
        public DTOEdiversaOrderItem OrderItem { get; set; }
        public int Qty { get; set; }

        public DTOEdiversaOrdrspItem() : base()
        {
        }
    }
}
