using FizzBuzzInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Número de llamadas concurrentes al método */
            int numberOfCalls = 100;
            
            Console.WriteLine("Performance test");
            Console.WriteLine("<Press enter to run test>\r\n");
            Console.ReadLine();

            var tasks = new List<Task<List<string>>>();
            for (var i = 0; i < numberOfCalls; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => getDataLists()));
            }

            var sw = new Stopwatch();
            sw.Start();
            Task.WaitAll(tasks.ToArray());
            sw.Stop();
            Console.WriteLine("{0} calls to Method in {1} ms", numberOfCalls, sw.ElapsedMilliseconds);
            Console.ReadLine();
        }

        private static List<string> getDataLists()
        {
            List<string> strList = null;
            ChannelFactory<IWCFFizzBuzzService> channelFactory = null;

            try
            {
                channelFactory = new ChannelFactory<IWCFFizzBuzzService>("FizzBuzzServiceEndpoint");
                IWCFFizzBuzzService proxy = channelFactory.CreateChannel();
                strList = proxy.GetData("0");
                channelFactory.Close();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                channelFactory.Abort();
            }

            return strList;            
        }
    }
}
