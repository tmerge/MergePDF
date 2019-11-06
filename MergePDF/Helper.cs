using System.Collections.Generic;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace MergePDF
{
    public class Helper
    {

        public Helper()
        {

        }


        /// <summary>
        ///     this method merge a list of pdf documents to one
        /// </summary>
        /// <param name="pdfDocuments"></param>
        /// <returns>merged pdf document</returns>
        public PdfDocument mergePdf(List<PdfDocument> pdfDocuments)
        {
            PdfDocument tmp = new PdfDocument();
            foreach(PdfDocument pdf in pdfDocuments)
            {
                foreach(PdfPage page in pdf.Pages)
                {
                    tmp.AddPage(page);
                }
            }
            return tmp;
        }

        /// <summary>
        ///     this method generates a list of pdf documents from list of paths
        /// </summary>
        /// <param name="paths"></param>
        /// <returns>list of pdf documents</returns>
        public List<PdfDocument> getPdfDocuments(List<string> paths)
        {
            List<PdfDocument> pdfDocuments = new List<PdfDocument>();
            PdfDocument tmp = new PdfDocument();
            foreach(string s in paths)
            {
                tmp = PdfReader.Open(s, PdfDocumentOpenMode.Import);
                pdfDocuments.Add(tmp);
            }
            return pdfDocuments;
        }

        public void savePdf(PdfDocument pdfDocument, string path)
        {
            pdfDocument.Save(path);
        }
    }
}
