using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DistributionChannelModel:BaseGuid
    {
        public LangTextModel? Nom { get; set; }

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

        public DistributionChannelModel() : base() { }
        public DistributionChannelModel(Guid guid) : base(guid) { }

        public static DistributionChannelModel? Wellknown(DistributionChannelModel.Wellknowns value)
        {
            DistributionChannelModel retval = null;
            switch (value)
            {
                case Wellknowns.noChannel:
                    {
                        retval = new DistributionChannelModel(Guid.Empty){
                            Nom = new LangTextModel("(sin canal)", "(sense canal)", "(Channel less)")
                        };
                        break;
                    }

                case Wellknowns.botiga:
                    {
                        retval = new DistributionChannelModel(new Guid("EF72040D-8F5D-40C7-B4CE-AB069656858D"));
                        break;
                    }

                case Wellknowns.cadenas:
                    {
                        retval = new DistributionChannelModel(new Guid("E7261551-E49E-4263-B5F3-D14543D29434"));
                        break;
                    }

                case Wellknowns.farmacia:
                    {
                        retval = new DistributionChannelModel(new Guid("4C1B6866-B97C-4105-BB52-68E657B8682B"));
                        break;
                    }

                case Wellknowns.granDistribucio:
                    {
                        retval = new DistributionChannelModel(new Guid("3ED938C2-2466-4E9D-8CED-D73074885016"));
                        break;
                    }

                case Wellknowns.diversos:
                    {
                        retval = new DistributionChannelModel(new Guid("7E7560D4-D3BE-4CD8-A42C-EF2C54B0EC26"));
                        break;
                    }
            }
            return retval;
        }


        public static DistributionChannelModel Default()
        {
            return DistributionChannelModel.Wellknown(Wellknowns.noChannel)!;
        }
    }
}
