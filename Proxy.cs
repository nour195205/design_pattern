using System;

namespace DesignPatterns.Proxy
{
    public interface ISecureSystem
    {
        void OpenSecureSession();
    }

    public interface IAuthenticationService
    {
        bool Authenticate(string providedPassword);
    }

    public class SecureSystem : ISecureSystem
    {
        public SecureSystem()
        {
            Console.WriteLine("[Loading heavy system resources into memory...]");
        }

        public void OpenSecureSession()
        {
            Console.WriteLine("Secure session opened successfully!");
        }
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _hashedPasswordFromConfig;

        public AuthenticationService(string hashedPasswordFromConfig)
        {
            _hashedPasswordFromConfig = hashedPasswordFromConfig;
        }

        public bool Authenticate(string providedPassword)
        {
            return providedPassword == _hashedPasswordFromConfig; 
        }
    }

    public class SecureSystemProxy : ISecureSystem
    {
        private const string _internalPassword = "SecureHash1234!";
        private readonly Lazy<ISecureSystem> _realSystem;
        private readonly IAuthenticationService _authService;
        private readonly string _providedPassword;

        public SecureSystemProxy(string providedPassword)
        {
            _authService = new AuthenticationService(_internalPassword);
            _providedPassword = providedPassword;
            _realSystem = new Lazy<ISecureSystem>(() => new SecureSystem());
        }

        public void OpenSecureSession()
        {
            if (!_authService.Authenticate(_providedPassword))
            {
                throw new Exception("Access denied: invalid authentication credentials.");
            }

            Console.WriteLine("Authentication successful, redirecting to the secure system...");
            _realSystem.Value.OpenSecureSession();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- First attempt (unauthorized access test) ---");
            var intruder = new SecureSystemProxy("WrongPass");
            try
            {
                intruder.OpenSecureSession();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Exception Caught]: {ex.Message}");
            }

            Console.WriteLine("\n--- Second attempt (authorized access and lazy loading test) ---");
            var admin = new SecureSystemProxy("SecureHash1234!");
            try
            {
                admin.OpenSecureSession();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Exception Caught]: {ex.Message}");
            }
        }
    }
}