using FizzBuzzInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace FizzBuzzService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.PerCall)]
    public class WCFFizzBuzzService : IWCFFizzBuzzService
    {
        private static object _lock = new object();

        public List<string> GetData(string value)
        {
            Console.WriteLine("\r\nDrunken client {0} entered start number at {1}",
                    Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());

            List<string> listSequenceNumbers = new List<string>();
            const int limit = 100;
            int num = 0;

            Int32.TryParse(value, out num);

            if (num <= limit)
            {
                while (num <= limit)
                {
                    listSequenceNumbers.Add((num % 3 == 0 && num % 5 == 0) ? "fizzbuzz" :
                        ((num % 3 == 0) ? "fizz" : (num % 5 == 0) ? "buzz" : num.ToString()));

                    num++;
                }

                string sequence = string.Join(", ", listSequenceNumbers.ToArray());
                
                lock (_lock)
                {
                    try
                    {
                        Console.WriteLine("\r\nDrunken client {0} entered lock at {1}",
                             Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());

                        WriteToAFile(string.Concat(DateTime.Now.ToString(
                            "[MMMM dd, yyyy hh:mm ss's']"), "\t", sequence));

                        Console.WriteLine("\r\nDrunken client {0} exited lock at {1}",
                            Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString());
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }                    
                }
                
                listSequenceNumbers.Insert(0, " ¡GO!\n");
                return listSequenceNumbers;
            }
            else
            {
                Console.WriteLine("\r\nValue is out of range!");
                return listSequenceNumbers;
            }
        }

        private static void WriteToAFile(string sequence)
        {
            string fileName = "list-registry.txt";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            Console.WriteLine("\r\nlist saved into: '{0}'\r\n", path);
            /* La instrucción using garantiza la llamada al método Dispose */
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(sequence);
            }
        }
    }
}
