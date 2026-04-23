using System;

namespace DesignPatterns.ChainOfResponsibility
{
    public abstract class Handler
    {
        protected Handler _nextHandler;

        public void SetNext(Handler handler)
        {
            _nextHandler = handler;
        }

        public virtual void Handle(string request)
        {
            if (_nextHandler != null)
            {
                _nextHandler.Handle(request);
            }
            else
            {
                Console.WriteLine($"--- النهاية: لا يوجد أحد في المؤسسة يستطيع معالجة الطلب: '{request}' ---");
            }
        }
    }

    public class TechnicalSupport : Handler
    {
        public override void Handle(string request)
        {
            if (request == "technical")
            {
                Console.WriteLine("المستوى 1 (الدعم الفني): تم حل المشكلة التقنية بنجاح.");
            }
            else
            {
                Console.WriteLine("المستوى 1 (الدعم الفني): مش تخصصي، بحولها للي بعدي...");
                base.Handle(request);
            }
        }
    }

    public class BillingSupport : Handler
    {
        public override void Handle(string request)
        {
            if (request == "billing")
            {
                Console.WriteLine("المستوى 2 (قسم الحسابات): تم تسوية الفاتورة بنجاح.");
            }
            else
            {
                Console.WriteLine("المستوى 2 (قسم الحسابات): مش تخصصي، بحولها للي بعدي...");
                base.Handle(request);
            }
        }
    }

    public class Manager : Handler
    {
        public override void Handle(string request)
        {
            if (request == "complaint")
            {
                Console.WriteLine("المستوى 3 (المدير): تم حل الشكوى الإدارية بنجاح.");
            }
            else
            {
                Console.WriteLine("المستوى 3 (المدير): دي حاجة كبيرة، لازم تروح للمدير التنفيذي...");
                base.Handle(request);
            }
        }
    }

    public class CEO : Handler
    {
        public override void Handle(string request)
        {
            if (request == "partnership")
            {
                Console.WriteLine("المستوى 4 (المدير التنفيذي CEO): تم توقيع عقد الشراكة بنجاح.");
            }
            else
            {
                Console.WriteLine("المستوى 4 (المدير التنفيذي CEO): حتى أنا مش هقدر أساعد في دي!");
                base.Handle(request);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // إنشاء المعالجين
            var tech = new TechnicalSupport();
            var billing = new BillingSupport();
            var manager = new Manager();
            var ceo = new CEO();

            // بناء السلسلة الطويلة: Tech -> Billing -> Manager -> CEO
            tech.SetNext(billing);
            billing.SetNext(manager);
            manager.SetNext(ceo);

            Console.WriteLine("--- تجربة طلب شراكة (Partnership) ---");
            tech.Handle("partnership");

            Console.WriteLine("\n--- تجربة طلب شكوى (Complaint) ---");
            tech.Handle("complaint");

            Console.WriteLine("\n--- تجربة طلب مجهول (Magic) ---");
            tech.Handle("magic");
        }
    }
}
