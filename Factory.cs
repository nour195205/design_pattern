using System;

namespace DesignPatterns.Factory
{
    public interface IDocument
    {
        void Show();
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
        public static IDocument CreateDocument(string type)
        {
            switch (type.ToLower())
            {
                case "pdf": return new PdfDocument();
                case "word": return new WordDocument();
                case "excel": return new ExcelDocument();
                case "ppt": return new PptDocument();
                case "image": return new ImageDocument();
                default: throw new ArgumentException($"نوع الملف '{type}' غير مدعوم في المصنع.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- تجربة المصنع مع أنواع ملفات متعددة ---");

            var doc1 = DocumentFactory.CreateDocument("pdf");
            doc1.Show();

            var doc2 = DocumentFactory.CreateDocument("excel");
            doc2.Show();

            var doc3 = DocumentFactory.CreateDocument("ppt");
            doc3.Show();

            var doc4 = DocumentFactory.CreateDocument("image");
            doc4.Show();

            try {
                var doc5 = DocumentFactory.CreateDocument("video");
                doc5.Show();
            } catch (Exception ex) {
                Console.WriteLine($"\nخطأ متوقع: {ex.Message}");
            }
        }
    }
}
