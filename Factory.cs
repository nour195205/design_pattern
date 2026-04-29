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
        public void Show() => Console.WriteLine("عرض ملف PDF (C#)");
    }

    public class WordDocument : IDocument
    {
        public void Show() => Console.WriteLine("عرض ملف Word (C#)");
    }

    public class ExcelDocument : IDocument
    {
        public void Show() => Console.WriteLine("عرض ملف Excel (C#) - جداول البيانات جاهزة.");
    }

    public class PptDocument : IDocument
    {
        public void Show() => Console.WriteLine("عرض ملف PowerPoint (C#) - العرض التقديمي جاهز.");
    }

    public class ImageDocument : IDocument
    {
        public void Show() => Console.WriteLine("عرض ملف Image (C#) - تم تحميل الصورة بنجاح.");
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
                default: throw new ArgumentOutOfRangeException(nameof(type), "نوع الملف غير مدعوم في المصنع.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- تجربة المصنع مع أنواع ملفات متعددة ---");

            var doc1 = DocumentFactory.CreateDocument(DocumentType.Pdf);
            doc1.Show();

            var doc2 = DocumentFactory.CreateDocument(DocumentType.Excel);
            doc2.Show();

            var doc3 = DocumentFactory.CreateDocument(DocumentType.Ppt);
            doc3.Show();

            var doc4 = DocumentFactory.CreateDocument(DocumentType.Image);
            doc4.Show();

            try {
                // مثال على عدم السماح بنوع غير معروف: سيتم اكتشاف الخطأ أثناء البرمجة وليس من النص.
                var invalidType = (DocumentType)999;
                var doc5 = DocumentFactory.CreateDocument(invalidType);
                doc5.Show();
            } catch (ArgumentOutOfRangeException ex) {
                Console.WriteLine($"\nخطأ متوقع: {ex.Message}");
            }
        }
    }
}
