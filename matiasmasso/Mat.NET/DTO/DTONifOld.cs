using MatHelperStd;

namespace DTO
{
    public class DTONifOld
    {
        private const string _ISOpaisLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string _AllowedLetters = "TRWAGMYFPDXBNJZSQVHLCKE";
        private const string _AllowedNumbers = "0123456789";

        public string Value { get; set; }
        public DTOCountry Country { get; set; }
        public Modes Mode { get; set; }
        public NifTypes Type { get; set; }
        public string Dni { get; set; }
        public string Letra { get; set; }

        public enum Modes
        {
            _NotSet,
            NIF,
            RegistreTributari,
            RegistreComercial,
            NumeroDeContribuente
        }

        public enum Errors
        {
            Ok,
            Empty,
            WrongLength,
            WrongAlfaNum,
            MisDecimaltter,
            LetterUnvalid
        }

        public enum NifTypes
        {
            NotSet,
            Unvalid,
            Fisica,
            Juridica,
            Estranger,
            EstrangerResident
        }

        public static DTONifOld Factory(string src, DTOCountry oCountry = null/* TODO Change to default(_) if this is not a reference type */, DTONifOld.Modes oMode = DTONifOld.Modes._NotSet)
        {
            DTONifOld retval = new DTONifOld();
            {
                var withBlock = retval;
                withBlock.Value = DTONifOld.CleanNif(src);
                withBlock.Country = oCountry;
                withBlock.Mode = oMode;
            }
            return retval;
        }

        public static string Intl(DTOContact oContact)
        {
            string retval = "";
            if (oContact != null && oContact.PrimaryNifValue().isNotEmpty())
            {
                string sNif = oContact.PrimaryNifValue();
                var oCountry = DTOAddress.Country(oContact.Address);
                if (DTOCountry.IsEsp(oCountry) & !sNif.StartsWith("ES"))
                    retval = string.Format("ES{0}", sNif);
                else
                    retval = sNif;
            }
            return retval;
        }

        public static string FullText(DTOContact oContact)
        {
            string retval = string.Format(oContact.PrimaryNifQualifiedValue());
            return retval;
        }

        public static string FullText(DTOConsumerTicket consumerTicket)
        {
            string retval = string.Format("NIF:{0}", consumerTicket.Nif);
            if (DTOAddress.Country(consumerTicket.Address).Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Andorra)))
                retval = string.Format("Numero de Registre Tributari (NRT): {0:@-000000-@}", consumerTicket.Nif);
            else
            {
                string sNifLabel = consumerTicket.Lang.Tradueix("NIF", "NIF", "VAT", "NIF");
                retval = string.Format("{0}: {1}", sNifLabel, consumerTicket.Nif);
            }
            return retval;
        }



        public static void Load(ref DTONifOld oNif, ref DTONifOld.Errors oError)
        {
            {
                var withBlock = oNif;
                if (withBlock.Type == DTONifOld.NifTypes.NotSet)
                {
                    int iLen = withBlock.Value.Length;
                    if (iLen > 0)
                    {
                        string FirstDigit = withBlock.Value.Substring(0, 1);
                        string LastDigit = withBlock.Value.Substring(iLen - 1);
                        switch (iLen)
                        {
                            case 0:
                                {
                                    oError = DTONifOld.Errors.Empty;
                                    withBlock.Type = DTONifOld.NifTypes.Unvalid;
                                    return;
                                }

                            case object _ when iLen < 8:
                                {
                                    oError = DTONifOld.Errors.WrongLength;
                                    withBlock.Type = DTONifOld.NifTypes.Unvalid;
                                    return;
                                }

                            case 9:
                            case 10:
                            case 11:
                                {
                                    string SecondDigit = withBlock.Value.Substring(1, 1);
                                    if (_ISOpaisLetters.Contains(FirstDigit) & _ISOpaisLetters.Contains(SecondDigit))
                                    {
                                        if (withBlock.Value.Substring(0, 2) == "ES")
                                            withBlock.Value = withBlock.Value.Substring(2);
                                        else
                                        {
                                            withBlock.Type = DTONifOld.NifTypes.Estranger;
                                            oError = DTONifOld.Errors.Ok;
                                            return;
                                        }
                                    }

                                    break;
                                }

                            case object _ when iLen > 11:
                                {
                                    oError = DTONifOld.Errors.WrongLength;
                                    withBlock.Type = DTONifOld.NifTypes.Unvalid;
                                    return;
                                }
                        }


                        bool FirstDigitIsLetter = _AllowedLetters.Contains(FirstDigit);
                        bool LastDigitIsLetter = _AllowedLetters.Contains(LastDigit);
                        if (FirstDigitIsLetter)
                        {
                            withBlock.Dni = withBlock.Value.Substring(1);
                            withBlock.Letra = FirstDigit;
                            withBlock.Type = DTONifOld.NifTypes.Juridica;
                            if (CheckCIF(oNif.ToString()))
                                oError = DTONifOld.Errors.Ok;
                            else
                                oError = DTONifOld.Errors.LetterUnvalid;
                        }
                        else if (LastDigitIsLetter)
                        {
                            withBlock.Dni = withBlock.Value.Substring(0, iLen - 1);
                            if (VbUtilities.isNumeric(withBlock.Dni))
                            {
                                long iResto = withBlock.Dni.toBigInteger() % 23;
                                withBlock.Letra = _AllowedLetters.Substring((int)iResto, 1);
                                if (withBlock.Letra == LastDigit)
                                    withBlock.Type = DTONifOld.NifTypes.Fisica;
                                else
                                {
                                    oError = DTONifOld.Errors.LetterUnvalid;
                                    withBlock.Type = DTONifOld.NifTypes.Unvalid;
                                }
                            }
                            else
                            {
                                oError = DTONifOld.Errors.WrongAlfaNum;
                                withBlock.Type = DTONifOld.NifTypes.Unvalid;
                            }
                        }
                        else
                        {
                            oError = DTONifOld.Errors.MisDecimaltter;
                            withBlock.Type = DTONifOld.NifTypes.Unvalid;
                        }
                    }
                }
            }
        }

        public static bool CheckCIF(string sSource)
        {
            bool retVal = false;

            // neteja de signes de punctuació
            string sCleanSource = "";
            string sSignesDePuntuacio = @"-,. /\:;";
            string tmpChar = "";
            for (int i = 0; i <= sSource.Length - 1; i++)
            {
                tmpChar = sSource.Substring(i, 1);
                if (!sSignesDePuntuacio.Contains(tmpChar))
                    sCleanSource += tmpChar;
            }

            // filtre de longitud: el NIF ha de tenir 9 xifres
            int iLen = sSource.Length;
            if (iLen == 9)
            {
                // validem la primera lletra
                string sValidFirstLetter = "ABCDEFHJPQSKLMRUVWX";
                string sFirstLetter = sCleanSource.Substring(0, 1);
                if (sValidFirstLetter.Contains(sFirstLetter))
                {
                    // validem els estrangers residents
                    // canviem la X inicial per un 0 i validem com si fos persona fisica
                    bool BlExtrangerResident = sFirstLetter == "X";
                    if (BlExtrangerResident)
                    {
                        string sEquivalentNIF = "0" + sCleanSource.Substring(1);
                        retVal = CheckNIF(sEquivalentNIF);
                        // oNif.Type = DTONif.NifTypes.EstrangerResident
                        return retVal;
                    }

                    // descartem el primer digit (de classificació) i l'ultim (el de control)
                    string sNucli = sCleanSource.Substring(1, 7);
                    int[] iDigit = new int[8];
                    for (int i = 1; i <= 7; i++)
                        iDigit[i] = System.Convert.ToInt32(sNucli.Substring(i - 1, 1));

                    // sumem els digits parells
                    int iCalcParells = iDigit[2] + iDigit[4] + iDigit[6];

                    // multipliquem cada xifra impar per dos, i sumem els digits dels resultats
                    string s1 = (iDigit[1] * 2).ToString("00");
                    string s3 = (iDigit[3] * 2).ToString("00");
                    string s5 = (iDigit[5] * 2).ToString("00");
                    string s7 = (iDigit[7] * 2).ToString("00");
                    int i1 = System.Convert.ToInt32(s1.Substring(0, 1)) + System.Convert.ToInt32(s1.Substring(1, 1));
                    int i3 = System.Convert.ToInt32(s3.Substring(0, 1)) + System.Convert.ToInt32(s3.Substring(1, 1));
                    int i5 = System.Convert.ToInt32(s5.Substring(0, 1)) + System.Convert.ToInt32(s5.Substring(1, 1));
                    int i7 = System.Convert.ToInt32(s7.Substring(0, 1)) + System.Convert.ToInt32(s7.Substring(1, 1));
                    int iCalcImpars = i1 + i3 + i5 + i7;

                    // Sumem els calculs parell e imparell,
                    // i trobem la resta de dividir-ho per deu.
                    // el digit de control será la diferencia fins a deu (o zero si dona deu)
                    int iSuma = iCalcParells + iCalcImpars;
                    int iMod = iSuma % 10;
                    int iDigitControl = 10 - iMod;
                    if (iDigitControl == 10)
                        iDigitControl = 0;
                    string sDigitControl = iDigitControl.ToString();

                    // Les corporacions locals tenen una lletra com a digit de control
                    string sSourceDigitControl = sCleanSource.Substring(8, 1);
                    string sCorporacions = "PS";
                    if (sCorporacions.Contains(sSourceDigitControl))
                    {
                        int iChar = iDigitControl + 64;
                        char character = (char)iChar;
                        sDigitControl = character.ToString();
                    }


                    retVal = (sDigitControl == sSourceDigitControl);
                }
            }

            return retVal;
        }

        public static bool CheckNIF(string sSource)
        {
            bool retVal = false;
            string sCadenaDeReferencia = "TRWAGMYFPDXBNJZSQVHLCKE";

            // descarta la lletra final
            int iLen = sSource.Length;
            string sSourceDigitControl = sSource.Substring(iLen - 1, 1);
            string sNucli = sSource.Substring(0, iLen - 1);

            // descarta no numerics
            string tmpChar = "";
            for (int i = 0; i <= sNucli.Length - 1; i++)
            {
                tmpChar = sSource.Substring(i, 1);
                if (!VbUtilities.isNumeric(tmpChar))
                {
                    return retVal;
                }
            }

            // troba la resta de la divisió per 23 
            // que será la posició de la lletra dins la cadena de referencia
            long iResto = sNucli.toBigInteger() % 23;
            string sDigitControl = sCadenaDeReferencia.Substring((int)iResto, 1);

            retVal = (sDigitControl == sSourceDigitControl);
            return retVal;
        }

        public static DTONifOld.Errors ValidationResult(DTONifOld oNif)
        {
            DTONifOld.Errors retval = DTONifOld.Errors.Ok;
            if (oNif.Type == DTONifOld.NifTypes.NotSet)
                DTONifOld.Load(ref oNif, ref retval);
            return retval;
        }

        public static string ValidationResultString(DTONifOld oNif, DTOLang oLang)
        {
            string s = "";
            switch (ValidationResult(oNif))
            {
                case DTONifOld.Errors.Empty:
                    {
                        s = oLang.Tradueix("casilla vacía", "casella buida", "empty textbox");
                        break;
                    }

                case DTONifOld.Errors.MisDecimaltter:
                    {
                        s = oLang.Tradueix("falta la letra", "manca la lletra", "missing letter");
                        break;
                    }

                case DTONifOld.Errors.WrongAlfaNum:
                    {
                        s = oLang.Tradueix("combinación inválida de letras y numeros", "combinació de lletres i numeros incorrecte", "wrong mix of letters and digits");
                        break;
                    }

                case DTONifOld.Errors.WrongLength:
                    {
                        s = oLang.Tradueix("longitud invalida", "longitud invalida", "wrong length");
                        break;
                    }

                case DTONifOld.Errors.LetterUnvalid:
                    {
                        s = oLang.Tradueix("letra incorrecta", "lletra incorrecte", "wrong letter");
                        break;
                    }

                case DTONifOld.Errors.Ok:
                    {
                        oLang.Tradueix("NIF validado correctamente", "NIF validad correctament", "NIF validated");
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


        public new string ToString()
        {
            return Value;
        }

        public static string CleanNif(string sSource)
        {
            // deixa nomes digits i lletres i passa-les a majuscules
            string retval = TextHelper.RegexSuppress(sSource, "[^A-Za-z0-9]").ToUpper();
            return retval;
        }
    }
}
