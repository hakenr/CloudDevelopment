using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache
{
    class Program
    {
        public static string Url = "http://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.txt?date={0}";

        static void Main(string[] args)
        {
            Console.WriteLine("Kurzovni listek CNB");
            Console.WriteLine("-------------------");
            Console.Write("Insert date in format DD.MM.YYYY: ");
            string date = Console.ReadLine();

            string url = string.Format(Url, date);

            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                string result = webClient.DownloadString(url);

                Console.WriteLine();
                Console.WriteLine(result);
                Console.ReadKey();
            }
        }
    }
}
