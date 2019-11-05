using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace MergePDF
{
    public class Helper
    {

        public Helper()
        {

        }


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
            string test = path;
            pdfDocument.Save(path);
        }
    }
}
