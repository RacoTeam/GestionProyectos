using iTextSharp.text;
using iTextSharp.text.pdf;
//using Microsoft.JSInterop;
//using System.IO;
//using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionProyectos.Client.Services
{
    public class PdfGenerator
    {

        //Muy cerca pero no funciono
        public static void GeneratePdf(string fileName, string title, string body)
        {
            //Create a new document
            Document document = new Document();

            //Create a PDF writer
            PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

            //Open the document
            document.Open();

            //Add a title
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            Paragraph titleParagraph = new Paragraph(title, titleFont);
            titleParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(titleParagraph);

            //Add some text
            Font bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            Paragraph bodyParagraph = new Paragraph(body, bodyFont);
            bodyParagraph.Alignment = Element.ALIGN_JUSTIFIED;
            document.Add(bodyParagraph);

            //Close the document
            document.Close();
        }

        //public int Id { get; set; }
        //public string Nombre { get; set; }

        //public void GenerarReporte(IJSRuntime js)
        //{
        //    List<PdfGenerator> cosas = new List<PdfGenerator>();
        //    for (int i = 1; i <= 9; i++)
        //    {
        //        cosas.Add(new PdfGenerator()
        //        {
        //            Id = i,
        //            Nombre = "hola"+i,
        //        }) ;

        //        js.InvokeAsync<PdfGenerator>("saveAsFile", "ejemplo.pdf"/*, Convert.ToBase64String()*/);
        //    }
        //}


    }
}