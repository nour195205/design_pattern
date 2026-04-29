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
                Console.WriteLine($"--- End: No one in the organization can handle the request: '{request}' ---");
            }
        }
    }

    public class TechnicalSupport : Handler
    {
        public override void Handle(string request)
        {
            if (request == "technical")
            {
                Console.WriteLine("Level 1 (Technical Support): The technical issue has been resolved successfully.");
            }
            else
            {
                Console.WriteLine("Level 1 (Technical Support): Not my specialty, passing it to the next handler...");
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
                Console.WriteLine("Level 2 (Billing Support): The invoice has been settled successfully.");
            }
            else
            {
                Console.WriteLine("Level 2 (Billing Support): Not my specialty, passing it to the next handler...");
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
                Console.WriteLine("Level 3 (Manager): The administrative complaint has been resolved successfully.");
            }
            else
            {
                Console.WriteLine("Level 3 (Manager): This is a big issue, it should go to the CEO...");
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
                Console.WriteLine("Level 4 (CEO): The partnership agreement has been signed successfully.");
            }
            else
            {
                Console.WriteLine("Level 4 (CEO): Even I cannot help with this request!");
                base.Handle(request);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create handlers
            var tech = new TechnicalSupport();
            var billing = new BillingSupport();
            var manager = new Manager();
            var ceo = new CEO();

            // Build the chain: Tech -> Billing -> Manager -> CEO
            tech.SetNext(billing);
            billing.SetNext(manager);
            manager.SetNext(ceo);

            Console.WriteLine("--- Partnership request test ---");
            tech.Handle("partnership");

            Console.WriteLine("\n--- Complaint request test ---");
            tech.Handle("complaint");

            Console.WriteLine("\n--- Unknown request test ---");
            tech.Handle("magic");
        }
    }
}
