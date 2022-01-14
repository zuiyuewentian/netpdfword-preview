using Spire.Doc;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;

namespace WpfApp3
{
    /// <summary>
    /// Main.xaml 的交互逻辑
    /// </summary>
    public partial class Main : Window
    {
        /// <summary>
        /// 转化临时显示文件
        /// </summary>
        public string tempPdfPreAddress = Environment.CurrentDirectory + "\\tempPdfPre\\";

        /// <summary>
        /// 统一读取
        /// </summary>
        XpsDocument readerDoc;

        public Main()
        {
            InitializeComponent();

            if (!Directory.Exists(tempPdfPreAddress))
            {
                Directory.CreateDirectory(tempPdfPreAddress);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string filePath = Environment.CurrentDirectory + "\\个人申请微信公众号教程.docx";
            ConvertWordToXPS2(filePath);
        }

        private void Button_Pdf_Click(object sender, RoutedEventArgs e)
        {
            string filePath = Environment.CurrentDirectory + "\\样板.pdf";
            ConvertPdfToXPS(filePath);
        }

        private void ConvertPdfToXPS(string pdfDocName)
        {
            if (readerDoc != null)
                readerDoc.Close();

            PdfDocument pdfDocument = new PdfDocument();
            pdfDocument.LoadFromFile(pdfDocName); 
            string name = tempPdfPreAddress + System.IO.Path.GetFileNameWithoutExtension(pdfDocName) + ".xps";
            pdfDocument.SaveToFile(name, Spire.Pdf.FileFormat.XPS);
            pdfDocument.Close();

            readerDoc = new XpsDocument(name, FileAccess.Read);
            docViewer.Document = readerDoc.GetFixedDocumentSequence();
            docViewer.FitToWidth();

        }

        private void ConvertWordToXPS2(string wordDocName)
        {
            if (readerDoc != null)
                readerDoc.Close();

            Document document = new Document(wordDocName);
            string name = tempPdfPreAddress + System.IO.Path.GetFileNameWithoutExtension(wordDocName) + ".xps";
            document.SaveToFile(name, Spire.Doc.FileFormat.XPS);
            document.Close();

            readerDoc = new XpsDocument(name, FileAccess.Read);
            docViewer.Document = readerDoc.GetFixedDocumentSequence();
            docViewer.FitToWidth();
        }

        //private XpsDocument ConvertWordToXPS(string wordDocName)
        //{
        //    FileInfo fi = new FileInfo(wordDocName);
        //    XpsDocument result = null;
        //    string xpsDocName = System.IO.Path.Combine(tempPdfPreAddress, fi.Name);
        //    xpsDocName = xpsDocName.Replace(".docx", ".xps").Replace(".doc", ".xps");
        //    Microsoft.Office.Interop.Word.Application wordApplication = new Microsoft.Office.Interop.Word.Application();
        //    try
        //    {
        //        if (!File.Exists(xpsDocName))
        //        {
        //            wordApplication.Documents.Add(wordDocName);
        //            Microsoft.Office.Interop.Word.Document doc = wordApplication.ActiveDocument;
        //            doc.ExportAsFixedFormat(xpsDocName, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatXPS, false, Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint, Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument, 0, 0, Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent, true, true, Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, Type.Missing);
        //            result = new XpsDocument(xpsDocName, System.IO.FileAccess.Read);
        //        }

        //        if (File.Exists(xpsDocName))
        //        {
        //            result = new XpsDocument(xpsDocName, FileAccess.Read);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message;
        //        wordApplication.Quit(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);
        //    }

        //    wordApplication.Quit(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);

        //    return result;
        //}

        private void Button_page_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show(docViewer.MasterPageNumber + "/" + docViewer.PageCount.ToString());
        }

        private void ZoomInButton_mouse_down(object sender, MouseButtonEventArgs e)
        {
            docViewer.Zoom += 10;
        }

        private void ZoomOutButton_mouse_down(object sender, MouseButtonEventArgs e)
        {
            docViewer.Zoom -= 10;
        }

        private void Back_mouse_down(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
