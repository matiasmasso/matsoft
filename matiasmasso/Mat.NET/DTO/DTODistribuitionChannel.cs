using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTODistributionChannel : DTOBaseGuid
    {
        public DTOLangText LangText { get; set; }
        public int Ord { get; set; }
        public int ConsumerPriority { get; set; }
        public List<DTOContactClass> ContactClasses { get; set; }

        public enum Wellknowns
        {
            notSet,
            granDistribucio,
            farmacia,
            botiga,
            cadenas,
            diversos,
            noChannel
        }


        public DTODistributionChannel() : base()
        {
            LangText = new DTOLangText();
            ContactClasses = new List<DTOContactClass>();
        }

        public DTODistributionChannel(Guid oGuid) : base(oGuid)
        {
            LangText = new DTOLangText();
            ContactClasses = new List<DTOContactClass>();
        }


        public static DTODistributionChannel Wellknown(DTODistributionChannel.Wellknowns value)
        {
            DTODistributionChannel retval = null;
            switch (value)
            {
                case Wellknowns.noChannel:
                    {
                        retval = new DTODistributionChannel(Guid.Empty);
                        retval.LangText.Load("(sin canal)", "(sense canal)", "Channel less)");
                        break;
                    }

                case Wellknowns.botiga:
                    {
                        retval = new DTODistributionChannel(new Guid("EF72040D-8F5D-40C7-B4CE-AB069656858D"));
                        break;
                    }

                case Wellknowns.cadenas:
                    {
                        retval = new DTODistributionChannel(new Guid("E7261551-E49E-4263-B5F3-D14543D29434"));
                        break;
                    }

                case Wellknowns.farmacia:
                    {
                        retval = new DTODistributionChannel(new Guid("4C1B6866-B97C-4105-BB52-68E657B8682B"));
                        break;
                    }

                case Wellknowns.granDistribucio:
                    {
                        retval = new DTODistributionChannel(new Guid("3ED938C2-2466-4E9D-8CED-D73074885016"));
                        break;
                    }

                case Wellknowns.diversos:
                    {
                        retval = new DTODistributionChannel(new Guid("7E7560D4-D3BE-4CD8-A42C-EF2C54B0EC26"));
                        break;
                    }
            }
            return retval;
        }

        public static string Nom(DTODistributionChannel oDistributionChannel, DTOLang oLang)
        {
            string retval = oDistributionChannel.LangText.Tradueix(oLang);
            return retval;
        }

        public static string Caption(List<DTODistributionChannel> oChannels, DTOLang oLang)
        {
            string retval = "";
            switch (oChannels.Count)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        retval = oChannels.First().LangText.Tradueix(oLang);
                        break;
                    }

                case 2:
                    {
                        retval = string.Format("{0}, {1}", oChannels[0].LangText.Tradueix(oLang), oChannels[1].LangText.Tradueix(oLang));
                        break;
                    }

                default:
                    {
                        retval = "(diversos)";
                        break;
                    }
            }
            return retval;
        }
    }
}
