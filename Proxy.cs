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
        private readonly Lazy<ISecureSystem> _realSystem;
        private readonly IAuthenticationService _authService;
        private readonly string _providedPassword;

        public SecureSystemProxy(IAuthenticationService authService, string providedPassword)
        {
            _authService = authService;
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
            string passwordFromEnv = "SecureHash1234!"; 
            var authService = new AuthenticationService(passwordFromEnv);

            Console.WriteLine("--- First attempt (unauthorized access test) ---");
            var intruder = new SecureSystemProxy(authService, "WrongPass");
            try
            {
                intruder.OpenSecureSession();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Exception Caught]: {ex.Message}");
            }

            Console.WriteLine("\n--- Second attempt (authorized access and lazy loading test) ---");
            var admin = new SecureSystemProxy(authService, "SecureHash1234!");
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