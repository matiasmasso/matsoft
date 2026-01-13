using MatHelperStd;
using System.Collections.Generic;

namespace DTO
{
    public class DTOTxt
    {
        public Ids Id { get; set; }

        public DTOLangText LangText { get; set; }

        public List<string> ParamValues { get; set; }
        public bool IsLoaded { get; set; }


        public enum Ids
        {
            NotSet,
            MailSpv,
            EfacturaRecordatori,
            MailImpago,
            MailDescarregues,
            Privacitat,
            AvisoLegal,
            About,
            RecollidaMagatzem,
            LeadAccountActivation,
            MailPwd,
            LeadAccountBaixa,
            LeadAccountConfirmation,
            PasswordEmail,
            Sorteo_MailToWinner,
            Sorteo_MailToWinnerDistributor,
            PoliticaDeCookies,
            MailRecall,
            MailRecallToVivace,
            MailTrpLoad,
            MailStocks,
            SatRecallCustomer,
            SatRecallManufacturer
        }

        public DTOTxt(Ids oId) : base()
        {
            Id = oId;
            LangText = new DTOLangText();
            ParamValues = new List<string>();
        }

        public string ToHtml(DTOLang oLang, params string[] sParamValues)
        {
            string retVal = LangText.Tradueix(oLang);
            retVal = MatHelperStd.TextHelper.Html(retVal);
            if (sParamValues.Length == 0)
                sParamValues = ParamValues.ToArray();
            for (int i = 1; i <= sParamValues.Length; i++)
            {
                string sParamKey = "@" + TextHelper.VbFormat(i, "00");
                retVal = retVal.Replace(sParamKey, sParamValues[i - 1]);
            }
            return retVal;
        }

        public static string Tit(DTOTxt oTxt)
        {
            string sRetVal = "";
            switch (oTxt.Id)
            {
                case DTOTxt.Ids.MailSpv:
                    {
                        sRetVal = "email envio etiqueta per recullida reparacio";
                        break;
                    }

                case DTOTxt.Ids.EfacturaRecordatori:
                    {
                        sRetVal = "apagon factura electronica";
                        break;
                    }

                case DTOTxt.Ids.MailImpago:
                    {
                        sRetVal = "email impagat";
                        break;
                    }

                case DTOTxt.Ids.MailDescarregues:
                    {
                        sRetVal = "email enllaç a descarga fitxer";
                        break;
                    }

                case DTOTxt.Ids.About:
                    {
                        sRetVal = "acerca de";
                        break;
                    }

                case DTOTxt.Ids.AvisoLegal:
                    {
                        sRetVal = "avis legal a la web";
                        break;
                    }

                case DTOTxt.Ids.Privacitat:
                    {
                        sRetVal = "privacitat a la web";
                        break;
                    }

                case DTOTxt.Ids.RecollidaMagatzem:
                    {
                        sRetVal = "recollida cap al magatzem";
                        break;
                    }

                case DTOTxt.Ids.LeadAccountActivation:
                    {
                        sRetVal = "email activació lead";
                        break;
                    }

                case DTOTxt.Ids.MailPwd:
                    {
                        sRetVal = "password per email";
                        break;
                    }

                case DTOTxt.Ids.LeadAccountBaixa:
                    {
                        sRetVal = "email confirmació baixa";
                        break;
                    }

                case DTOTxt.Ids.LeadAccountConfirmation:
                    {
                        sRetVal = "email confirmació lead";
                        break;
                    }

                case DTOTxt.Ids.PasswordEmail:
                    {
                        sRetVal = "password per email (no)";
                        break;
                    }

                case DTOTxt.Ids.Sorteo_MailToWinner:
                    {
                        sRetVal = "email sorteig guanyador";
                        break;
                    }

                case DTOTxt.Ids.Sorteo_MailToWinnerDistributor:
                    {
                        sRetVal = "email sorteig distribuidor del guanyador";
                        break;
                    }

                case DTOTxt.Ids.MailTrpLoad:
                    {
                        sRetVal = "email Ordre de Càrrega i Transport";
                        break;
                    }

                default:
                    {
                        sRetVal = oTxt.Id.ToString();
                        break;
                    }
            }
            return sRetVal;
        }
    }
}
