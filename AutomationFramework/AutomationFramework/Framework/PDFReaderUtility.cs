using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Framework
{
    public class PDFReaderUtility
    {
        public string GetTextFromPDFFile(string filepath)
        {
            StringBuilder text = new StringBuilder();
            if(File.Exists(filepath))
            {
                using (PdfReader reader = new PdfReader(filepath))
                {
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                    }
                }
            }
            
                return text.ToString();
        }

        public string GetColumnWiseTextFromPDFFile(string filepath)
        {
            StringBuilder text = new StringBuilder();
            if (File.Exists(filepath))
            {
                PdfReader reader = new PdfReader(filepath);

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                    string currenttext = PdfTextExtractor.GetTextFromPage(reader, i,strategy);
                    currenttext = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currenttext)));
                    text.Append(currenttext);
                }
                reader.Close();

            }

            return text.ToString();
        }

        public string GetLineWiseTextFromPDFFile(string filepath)
        {
            StringBuilder text = new StringBuilder();
            if (File.Exists(filepath))
            {
                //string[] lines;
                PdfReader reader = new PdfReader(filepath);

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();

                    string page = PdfTextExtractor.GetTextFromPage(reader, i, strategy);
                    string[]  lines = page.Split('\n');
                    for(int j=0; j<lines.Length;j++)
                    {
                        string line = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(lines[j]));
                        text.Append(line);
                    }
                    
                }
                reader.Close();

            }

            return text.ToString();
        }
    }
}
