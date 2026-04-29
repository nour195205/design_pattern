using System;

namespace DesignPatterns.Singleton
{
    public class DatabaseManager
    {
        private static DatabaseManager _instance;
        private static readonly object _lock = new object();
        public string ConnectionName { get; set; }

        // Constructor private عشان مفيش حد يقدر يعمل منه نسخة بره الكلاس
        private DatabaseManager()
        {
            Console.WriteLine("--- إنشاء اتصال جديد بقاعدة البيانات (C#) ---");
            ConnectionName = "Main_C#_DB_Connection";
        }

        public static DatabaseManager GetInstance()
        {
            // Thread-safety لضمان الأمان في حالة تعدد الخيوط (Threads)
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseManager();
                    }
                }
            }

            return _instance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db1 = DatabaseManager.GetInstance();
            var db2 = DatabaseManager.GetInstance();

            Console.WriteLine($"db1 HashCode: {db1.GetHashCode()}");
            Console.WriteLine($"db2 HashCode: {db2.GetHashCode()}");

            if (db1 == db2)
            {
                Console.WriteLine("برافو! النسختين متطابقتين تماماً في C#.");
            }
        }
    }
}
