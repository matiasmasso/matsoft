using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCsv
    {
        public List<DTOCsvRow> Rows { get; set; }
        public string Title { get; set; }
        public string Filename { get; set; }

        public DTOCsv(string sTitle = "", string sFilename = "")
        {
            Rows = new List<DTOCsvRow>();
            Title = sTitle;
            if (sFilename == "")
                sFilename = sTitle;
            if (sFilename == "")
                sFilename = "M+O downloaded file.csv";
            if (sFilename.EndsWith(".csv"))
            {
            }
            else
                sFilename += ".csv";
            Filename = sFilename;
        }

        public static DTOCsv Factory(string src)
        {
            List<string> lines = src.toLinesList();
            DTOCsv retval = new DTOCsv();
            foreach (string sLine in lines)
            {
                DTOCsvRow oRow = new DTOCsvRow();
                oRow.Cells = new List<string>();

                foreach (string sCell in sLine.Split(';'))
                    oRow.Cells.Add(sCell);
                retval.Rows.Add(oRow);
            }
            return retval;
        }

        public DTOCsvRow AddRow()
        {
            DTOCsvRow oRow = new DTOCsvRow();
            oRow.Cells = new List<string>();
            Rows.Add(oRow);
            return oRow;
        }

        public new string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            var quotesNeeded = new[] { ";", ",", Environment.NewLine };
            var lines = new List<string>();
            foreach (DTOCsvRow oRow in Rows)
            {
                sb = new System.Text.StringBuilder();
                bool FirstField = true;
                foreach (string oField in oRow.Cells)
                {
                    if (FirstField)
                        FirstField = false;
                    else
                        sb.Append(";");

                    if (oField != null)
                    {
                        if (quotesNeeded.Any(oField.Contains))
                            sb.AppendFormat("'{0}'", oField);
                        else
                            sb.Append(oField);
                    }
                }
                lines.Add(sb.ToString());
            }
            string retval = String.Join(Environment.NewLine, lines.ToArray());
            return retval;
        }

        public byte[] toByteArray()
        {
            string src = this.ToString();
            byte[] retval = new byte[src.Length * 2 - 1 + 1];
            System.Buffer.BlockCopy(src.ToCharArray(), 0, retval, 0, retval.Length);
            return retval;
        }
    }

    public class DTOCsvRow
    {
        public List<string> Cells { get; set; }

        public DTOCsvRow()
        {
            Cells = new List<string>();
        }

        public void AddCell(string sField)
        {
            Cells.Add(sField);
        }

        public void AddCell(DTOAmt oAmt)
        {
            if (oAmt == null)
                Cells.Add("");
            else
                Cells.Add(oAmt.Eur.ToString());
        }
    }
}
