using DocumentFormat.OpenXml.Bibliography;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using static Web.Helpers.ByteArrayPdfSplitter;

namespace Web.Helpers
{
    class ByteArrayPdfSplitter : PdfSplitter
    {

        private MemoryStream currentOutputStream;
        public List<byte[]> Pages { get; private set; } = new();

        public ByteArrayPdfSplitter(PdfDocument pdfDocument) : base(pdfDocument)
        {
        }

        protected override PdfWriter GetNextPdfWriter(PageRange documentPageRange)
        {
            currentOutputStream = new MemoryStream();
            return new PdfWriter(currentOutputStream);
        }

        public MemoryStream CurrentMemoryStream
        {
            get { return currentOutputStream; }
        }

        public class DocumentReadyListener : IDocumentReadyListener
        {
            public List<byte[]> splitPdfs;

            private ByteArrayPdfSplitter splitter;

            public DocumentReadyListener(ByteArrayPdfSplitter splitter, List<byte[]> results)
            {
                this.splitter = splitter;
                this.splitPdfs = results;
            }

            public void DocumentReady(PdfDocument pdfDocument, PageRange pageRange)
            {
                pdfDocument.Close();
                byte[] contents = splitter.CurrentMemoryStream.ToArray();
                splitPdfs.Add(contents);
            }
        }

        //public class DocumentReadyListenderOld : IDocumentReadyListener
        //{

        //    private ByteArrayPdfSplitter splitter;

        //    public DocumentReadyListender(ByteArrayPdfSplitter splitter)
        //    {
        //        this.splitter = splitter;
        //    }

        //    public void DocumentReady(PdfDocument pdfDocument, PageRange pageRange)
        //    {
        //        pdfDocument.Close();
        //        byte[] contents = splitter.CurrentMemoryStream.ToArray();
        //        //Pages.Add(contents);
        //        String pageNumber = pageRange.ToString();
        //    }
        //}
    }
}
