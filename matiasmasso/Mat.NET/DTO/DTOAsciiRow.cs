using MatHelperStd;
using System;
using System.Collections;

namespace DTO
{
    public class DTOAsciiRow
    {
        private Exception mEx;
        private ArrayList mFields;

        public enum FchFormats
        {
            DDMMYY,
            YYYYMMDD,
            DDMMYYYY
        }

        public DTOAsciiRow() : base()
        {
            mFields = new ArrayList();
        }

        public ArrayList Fields
        {
            get
            {
                return mFields;
            }
            set
            {
                mFields = value;
            }
        }

        private void AddFld(string sTxt, int iLen)
        {
            switch (sTxt.Length)
            {
                case object _ when sTxt.Length < iLen:
                    {
                        sTxt = sTxt.PadRight(iLen);
                        break;
                    }

                case object _ when sTxt.Length > iLen:
                    {
                        mEx = new Exception("text fora de rango", new System.Exception());
                        break;
                    }
            }
            mFields.Add(sTxt);
        }

        public void AddTxt(string sSource, int iLen)
        {
            string sTxt = Depura(sSource);

            switch (sTxt.Length)
            {
                case object _ when sTxt.Length < iLen:
                    {
                        sTxt = sTxt.PadRight(iLen);
                        break;
                    }

                case object _ when sTxt.Length > iLen:
                    {
                        sTxt = TextHelper.VbLeft(sTxt, iLen);
                        break;
                    }
            }
            AddFld(sTxt, iLen);
        }

        public void AddPlaceHolder(int iLen)
        {
            AddFld("", iLen);
        }

        public void AddInt(int iInteger, int iLen)
        {
            string sTxt = System.Convert.ToString(iInteger);
            sTxt = sTxt.PadLeft(iLen, '0');
            AddFld(sTxt, iLen);
        }

        public void AddDec(decimal DcNum, int iEnteros, int iDecimals = 0)
        {
            string sTxt;
            var sWholeFormat = new String('0', iEnteros);
            var sFractionFormat = new String('0', iDecimals);
            var wholePart = Math.Truncate(DcNum);
            if (iDecimals == 0)
                sTxt = TextHelper.VbFormat(wholePart, sWholeFormat);
            else
            {
                var fractionPart = System.Convert.ToInt32(((double)DcNum - (double)wholePart) * Math.Pow(10, iDecimals));
                sTxt = TextHelper.VbFormat(wholePart, sWholeFormat) + TextHelper.VbFormat(fractionPart, sFractionFormat);
            }
            int iLen = iEnteros + iDecimals;
            AddFld(sTxt, iLen);
        }

        public void AddFch(DateTime DtFch, FchFormats oFormat = FchFormats.DDMMYY)
        {
            string sTxt = "";
            int iLen = 0;
            switch (oFormat)
            {
                case FchFormats.DDMMYY:
                    {
                        sTxt = TextHelper.VbFormat(DtFch, "ddMMyy");
                        iLen = 6;
                        break;
                    }

                case FchFormats.YYYYMMDD:
                    {
                        sTxt = TextHelper.VbFormat(DtFch, "yyyyMMdd");
                        iLen = 8;
                        break;
                    }

                case FchFormats.DDMMYYYY:
                    {
                        sTxt = TextHelper.VbFormat(DtFch, "ddMMyyyy");
                        iLen = 8;
                        break;
                    }
            }
            AddFld(sTxt, iLen);
        }

        public string FullText()
        {
            string sRetVal = "";
            foreach (string s in mFields)
                sRetVal += s;
            return sRetVal;
        }

        protected string Depura(string source)
        {
            if (source == null)
                return "";
            source = source.Replace("ñ", "n");
            source = source.Replace("Ñ", "N");
            source = source.Replace("Á", "A");
            source = source.Replace("É", "E");
            source = source.Replace("Í", "I");
            source = source.Replace("Ó", "O");
            source = source.Replace("Ú", "U");
            source = source.Replace("á", "a");
            source = source.Replace("é", "e");
            source = source.Replace("í", "i");
            source = source.Replace("ó", "o");
            source = source.Replace("ú", "u");
            source = source.Replace("ç", "c");
            source = source.Replace("Ç", "C");
            source = source.Replace("ä", "a");
            source = source.Replace("ë", "e");
            source = source.Replace("ï", "i");
            source = source.Replace("ö", "o");
            source = source.Replace("ü", "u");
            source = source.Replace("ª", "a");
            source = source.Replace("º", "o");
            source = source.Replace("à", "a");
            source = source.Replace("è", "e");
            source = source.Replace("ì", "i");
            source = source.Replace("ò", "o");
            source = source.Replace("ù", "u");
            source = source.Replace("`", "'");
            source = source.Replace("´", "'");
            source = source.Replace("&", "-");

            System.Text.ASCIIEncoding oAscii = new System.Text.ASCIIEncoding();
            byte[] EncodedBytes = oAscii.GetBytes(source);
            string sTxt = oAscii.GetString(EncodedBytes);

            return sTxt;
        }
    }
}
