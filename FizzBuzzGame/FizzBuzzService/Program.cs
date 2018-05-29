using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzzService
{
    class Program
    {        
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = null;
                try
                {
                    host = new ServiceHost(typeof(WCFFizzBuzzService));
                    host.Open();

                    Console.WriteLine("Server is open");
                    Console.WriteLine("<Press enter to close server>");
                    Console.ReadLine();
                }
                finally
                {
                    if (host != null && host.State != CommunicationState.Faulted)
                        ((IDisposable)host).Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("\r\n");

                string text = "<Press enter to close window>";
                Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
                Console.WriteLine(text);

                Console.ReadLine();
            } 
        }
    }
}
