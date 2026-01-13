using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatHelper.Components
{
    public class CustomPdfSplitter : PdfSplitter
    {
        public List<string> PageFilenames { get; private set; }
        private string _sourceFilename;
        private int _pageNum;
        private string _destinationFolder;

        public static List<string> Split(string sourceFilename, string destinationFolder)
        {
            List<string> retval = new List<string>();
            using (var pdfDoc = new PdfDocument(new PdfReader(sourceFilename)))
            {
                var splitter = new CustomPdfSplitter(pdfDoc, sourceFilename, destinationFolder);
                var splittedDocs = splitter.SplitByPageCount(1);

                foreach (var splittedDoc in splittedDocs)
                {
                    splittedDoc.Close();
                }
                retval = splitter.PageFilenames;
            }

            return retval;
        }

        public CustomPdfSplitter(PdfDocument pdfDocument, string sourceFilename,  string destinationFolder = "") : base(pdfDocument)
        {
            _sourceFilename = sourceFilename;
            PageFilenames = new List<string>();
            SetDestinationFolder(destinationFolder);
            _pageNum = 1;
        }

        private void SetDestinationFolder(string destinationFolder)
        {
            var backslash = @"\";
            if (string.IsNullOrEmpty(destinationFolder))
            {
                var lastSlash = _sourceFilename.LastIndexOf(backslash);
                _destinationFolder = lastSlash > 0 ? _sourceFilename.Substring(0, lastSlash) : "";
            }
            _destinationFolder = _destinationFolder.EndsWith(backslash) ? _destinationFolder :  _destinationFolder+backslash;
        }

        protected override PdfWriter GetNextPdfWriter(PageRange documentPageRange)
        {
            var pagefilename = _sourceFilename.Replace(".pdf", string.Format(".Page {0}.pdf", _pageNum++));
            //var pagefilename = _sourceFilename + string.Format(".Page {0}.pdf", _pageNum++);
            var retval = new PdfWriter(pagefilename);
            PageFilenames.Add(pagefilename);
            return retval;
        }

    }
}
