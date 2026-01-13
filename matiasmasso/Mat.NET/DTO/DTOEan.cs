using MatHelperStd;
using System;
using Newtonsoft.Json;

namespace DTO
{
    public class DTOEan
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        public enum ValidationResults
        {
            NotSet,
            Ok,
            Empty,
            WrongLength,
            WrongCheckDigit
        }



        public DTOEan(string sDigits) : base()
        {
            Value = sDigits;
        }

        public DTOEan() : base()
        {
        }

        public static DTOEan Factory(object sDigits)
        {

            DTOEan retval = null;
            if (sDigits != null)
            {
                retval = new DTOEan();
                retval.Value = DTOEan.CleanDigits(sDigits.ToString());
            }
            return retval;
        }

        public static string eanValue(DTOEan oEan)
        {
            string retval = "";
            if (oEan != null)
                retval = oEan.Value;
            return retval;
        }

        public string RemoveControlDigit()
        {
            string retval = Value.Substring(0, 12);
            return retval;
        }

        public static string CleanDigits(string src)
        {
            string retval = TextHelper.RegexSuppress(src, "[^A-Za-z0-9]").ToUpper();
            return retval;
        }

        public new bool Equals(object oCandidate)
        {
            bool retval = false;
            if (oCandidate != null)
            {
                if (oCandidate.GetType() == typeof(DTOEan))
                {
                    DTOEan oCandidateEan = (DTOEan)oCandidate;
                    if (oCandidateEan.Value == Value)
                        retval = true;
                }
            }
            return retval;
        }

        public new string ToString()
        {
            string retval = "(empty Ean)";
            if (!string.IsNullOrEmpty(Value))
                retval = Value;
            return retval;
        }


        public static string ValidationResultString(DTOEan.ValidationResults oValidationResult, DTOLang oLang)
        {
            string s = "";
            switch (oValidationResult)
            {
                case DTOEan.ValidationResults.Empty:
                    {
                        s = oLang.Tradueix("casilla vacía", "casella buida", "empty textbox");
                        break;
                    }

                case DTOEan.ValidationResults.WrongLength:
                    {
                        s = oLang.Tradueix("longitud invalida", "longitud invalida", "wrong length");
                        break;
                    }

                case DTOEan.ValidationResults.WrongCheckDigit:
                    {
                        s = oLang.Tradueix("digito de control invalido", "digit de control invalid", "unvalid check digit");
                        break;
                    }

                case DTOEan.ValidationResults.Ok:
                    {
                        oLang.Tradueix("codigo validado", "codi validat", "EAN validated");
                        break;
                    }

                default:
                    {
                        oLang.Tradueix("error desconocido", "error desconegut", "unknown error");
                        break;
                    }
            }
            return s;
        }

        public static bool isValid(DTOEan oEan)
        {
            DTOEan.ValidationResults oValidationResult = validate(oEan);
            bool retval = (oValidationResult == DTOEan.ValidationResults.Ok);
            return retval;
        }

        public static bool isValid(Object src)
        {
            DTOEan oEan = null;
            if (src is string)
                oEan = DTOEan.Factory((string)src);
            else if (src is DTOEan)
                oEan = (DTOEan)src;

            DTOEan.ValidationResults oValidationResult = validate(oEan);
            bool retval = (oValidationResult == DTOEan.ValidationResults.Ok);
            return retval;
        }



        public static DTOEan.ValidationResults validate(DTOEan oEan)
        {
            DTOEan.ValidationResults retval = DTOEan.ValidationResults.NotSet;
            if (oEan == null)
                retval = DTOEan.ValidationResults.Empty;
            else
                switch (oEan.Value.Length)
                {
                    case 0:
                        {
                            retval = DTOEan.ValidationResults.Empty;
                            break;
                        }

                    case 12:
                        {
                            DTOEan tmp = DTOEan.Factory('0' + oEan.Value);
                            int iLastDigit = Int32.Parse(tmp.Value.right(1));
                            int iCheckDigit = DTOEan.CheckDigit(tmp);
                            if (iLastDigit == iCheckDigit)
                            {
                                oEan.Value = tmp.Value;
                                retval = DTOEan.ValidationResults.Ok;
                            }
                            else
                                retval = DTOEan.ValidationResults.WrongLength;
                            break;
                        }


                    case 13:
                        {
                            int iLastDigit = Int32.Parse(oEan.Value.right(1));
                            int iCheckDigit = DTOEan.CheckDigit(oEan);
                            if (iLastDigit == iCheckDigit)
                                retval = DTOEan.ValidationResults.Ok;
                            else
                                retval = DTOEan.ValidationResults.WrongCheckDigit;
                            break;
                        }

                    default:
                        {
                            retval = DTOEan.ValidationResults.WrongLength;
                            break;
                        }
                }

            return retval;
        }

        public static int CheckDigit(DTOEan oEan)
        {
            int iOddSum = 0;
            int iEvenSum = 0;
            int iCheckDigit = 0;
            string sDigits = oEan.Value;

            for (int i = 0; i <= 11; i++)
            {
                if (i % 2 == 0)
                {
                    iEvenSum += sDigits.Substring(i, 1).toInteger();
                }
                else
                {
                    iOddSum += sDigits.Substring(i, 1).toInteger();
                }
            }

            // check digit is some number + (iEvenSum + iOddSum * 3) = value evenly divisible by 10
            iCheckDigit = 10 - ((iEvenSum + 3 * iOddSum) % 10);
            if (iCheckDigit == 10)
                iCheckDigit = 0;

            return iCheckDigit;
        }
    }


}
