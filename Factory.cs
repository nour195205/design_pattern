using System;

namespace DesignPatterns.Factory
{
    public interface IDocument
    {
        void Show();
    }

    public enum DocumentType
    {
        Pdf,
        Word,
        Excel,
        Ppt,
        Image
    }

    public class PdfDocument : IDocument
    {
        public void Show() => Console.WriteLine("Showing PDF document (C#)");
    }

    public class WordDocument : IDocument
    {
        public void Show() => Console.WriteLine("Showing Word document (C#)");
    }

    public class ExcelDocument : IDocument
    {
        public void Show() => Console.WriteLine("Showing Excel document (C#) - Spreadsheet is ready.");
    }

    public class PptDocument : IDocument
    {
        public void Show() => Console.WriteLine("Showing PowerPoint document (C#) - Presentation is ready.");
    }

    public class ImageDocument : IDocument
    {
        public void Show() => Console.WriteLine("Showing Image document (C#) - Image loaded successfully.");
    }

    public static class DocumentFactory
    {
        public static IDocument CreateDocument(DocumentType type)
        {
            switch (type)
            {
                case DocumentType.Pdf: return new PdfDocument();
                case DocumentType.Word: return new WordDocument();
                case DocumentType.Excel: return new ExcelDocument();
                case DocumentType.Ppt: return new PptDocument();
                case DocumentType.Image: return new ImageDocument();
                default: throw new ArgumentOutOfRangeException(nameof(type), "Unsupported document type in factory.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Factory test with multiple document types ---");

            var doc1 = DocumentFactory.CreateDocument(DocumentType.Pdf);
            doc1.Show();

            var doc2 = DocumentFactory.CreateDocument(DocumentType.Excel);
            doc2.Show();

            var doc3 = DocumentFactory.CreateDocument(DocumentType.Ppt);
            doc3.Show();

            var doc4 = DocumentFactory.CreateDocument(DocumentType.Image);
            doc4.Show();

            try {
                // Example of rejecting an unknown type: the error is a code-time safety check, not text-based.
                var invalidType = (DocumentType)999;
                var doc5 = DocumentFactory.CreateDocument(invalidType);
                doc5.Show();
            } catch (ArgumentOutOfRangeException ex) {
                Console.WriteLine($"\nExpected error: {ex.Message}");
            }
        }
    }
}
