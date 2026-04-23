using System;

namespace DesignPatterns.Proxy
{
    public interface ISystem
    {
        void Access();
    }

    public class SecureSystem : ISystem
    {
        public void Access()
        {
            Console.WriteLine("تم الدخول للنظام السري (C#) بنجاح!");
        }
    }

    public class SystemProxy : ISystem
    {
        private SecureSystem _realSystem;
        private string _password;

        public SystemProxy(string password)
        {
            _password = password;
        }

        public void Access()
        {
            if (_password == "1234")
            {
                if (_realSystem == null)
                {
                    _realSystem = new SecureSystem();
                }
                _realSystem.Access();
            }
            else
            {
                Console.WriteLine("خطأ: كلمة المرور غلط! ممنوع الدخول في C#.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("المحاولة الأولى:");
            var p1 = new SystemProxy("0000");
            p1.Access();

            Console.WriteLine("\nالمحاولة الثانية:");
            var p2 = new SystemProxy("1234");
            p2.Access();
        }
    }
}
