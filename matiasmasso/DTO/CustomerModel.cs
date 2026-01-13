using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CustomerModel:ContactModel
    {

        public enum Wellknowns
        {
            notSet,
            elCorteIngles,
            eciga,
            amazon,
            sonae,
            carrefour,
            prenatal,
            prenatalPortugal,
            prenatalTenerife,
            miFarma,
            bebitus,
            javierBayon,
            tradeInn,
            consumidor,
            worten,
            promofarma,
            wells,
            continente
        }

        public enum PortsCodes
        {
            notSet,
            pagats,
            deguts,
            reculliran,
            altres,
            entregatEnMa
        }

        public enum CashCodes
        {
            notSet,
            credit,
            reembols,
            transferenciaPrevia,
            visa,
            diposit
        }
        public CustomerModel() : base() { }
        public CustomerModel(Guid guid) : base(guid) { }

        public static CustomerModel? Wellknown(CustomerModel.Wellknowns id)
        {
            CustomerModel? retval = null;
            string sGuid = "";
            switch (id)
            {
                case Wellknowns.elCorteIngles:
                    {
                        sGuid = "1850CA50-B514-404E-BD5C-3C33B7A6D3BF";
                        break;
                    }

                case Wellknowns.eciga:
                    {
                        sGuid = "4A590843-E1E7-4550-9375-B42FCC917A24";
                        break;
                    }

                case Wellknowns.sonae:
                    {
                        sGuid = "EBCF8BC0-EE11-4875-8EB7-98A7078A6165"; // Fashion Inter.Trade, S.A.
                        break;
                    }
                case Wellknowns.amazon:
                    {
                        sGuid = "BDAC8F45-D3E7-47D7-8229-889FBA4543E1";
                        break;
                    }

                case Wellknowns.carrefour:
                    {
                        sGuid = "21DAC56A-F152-48CE-B357-6A8508520622";
                        break;
                    }

                case Wellknowns.prenatal:
                    {
                        sGuid = "44684614-0437-4FFB-B59E-D0B1392F819F";
                        break;
                    }

                case Wellknowns.prenatalPortugal:
                    {
                        sGuid = "E59C399A-A9BD-4D17-9729-ACA9FF88A7A4";
                        break;
                    }

                case Wellknowns.prenatalTenerife:
                    {
                        sGuid = "4779EE3D-5876-4065-B4FD-6D1F09D655AA";
                        break;
                    }

                case Wellknowns.miFarma:
                    {
                        sGuid = "35D515BA-585D-458A-9126-C713A5B26F58";
                        break;
                    }

                case Wellknowns.bebitus:
                    {
                        sGuid = "B6613C73-A857-401C-8F86-B6597378EA88";
                        break;
                    }

                case Wellknowns.javierBayon:
                    {
                        sGuid = "6901C741-9554-46BC-B4BA-8696929D2454";
                        break;
                    }
                case Wellknowns.tradeInn:
                    {
                        sGuid = "8E5FEB8F-3D6A-4630-978E-94EABF589EB5";
                        break;
                    }
                case Wellknowns.consumidor:
                    {
                        sGuid = "1925F462-D263-4BC9-BAEA-9186FD9AD111";
                        break;
                    }
                case Wellknowns.worten:
                    {
                        sGuid = "2BAF35DA-4D7A-411A-86F3-ABDBD6660967";
                        break;
                    }
                case Wellknowns.promofarma:
                    {
                        sGuid = "150136FD-BCAF-459F-954F-C92B5E9DF7B0";
                        break;
                    }
                case Wellknowns.wells:
                    {
                        sGuid = "5B377F5C-B51E-4E58-938B-97A51D7D0B62";
                        break;
                    }
                case Wellknowns.continente:
                    {
                        sGuid = "694fe077-265d-47ce-8caa-945c60f2a9c7";
                        break;
                    }

            }

            if (!string.IsNullOrEmpty(sGuid))
            {
                Guid oGuid = new Guid(sGuid);
                retval = new CustomerModel(oGuid);
            }
            return retval;
        }

    }
}
