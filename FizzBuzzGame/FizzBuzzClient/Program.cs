using FizzBuzzInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzzClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChannelFactory<IWCFFizzBuzzService> channelFactory = new
                    ChannelFactory<IWCFFizzBuzzService>("FizzBuzzServiceEndpoint");

                IWCFFizzBuzzService proxy = channelFactory.CreateChannel();
                
                Console.Write("Enter random number: ");
                string input = Console.ReadLine();
                if (input != null 
                    && input != String.Empty 
                        && Int32.TryParse(input, out int num))
                {
                    // Llama al servidor a través de la proxy                        
                    List<string> sequence = proxy.GetData(input);
                    foreach (var s in sequence)
                    {
                        centerText(s);
                    }
                }
                else if (input != String.Empty)
                {
                    throw new NotAnIntegerException();
                }
                else
                {
                    throw new EmptyValueException();
                }

                Console.ReadLine();
            }
            catch (EmptyValueException ev)
            {
                Console.WriteLine(ev.Message);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("\r\n");
                string text = "<Press enter to close window>";
                Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
                Console.WriteLine(text);
                Console.ReadLine();
            }
        }

        private static void centerText(string text)
        {
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
        }
    }

    class EmptyValueException : Exception
    {
        public override string Message
        {
            get
            {
                return "You didn't enter a value";
            }
        }
    }

    class NotAnIntegerException : Exception
    {
        public override string Message
        {
            get
            {
                return "The value is not an integer";
            }
        }
    }
}
