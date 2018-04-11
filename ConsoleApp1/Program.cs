using Mag.HttpClient;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MagClient client = new MagClient();
            var token = client.SetUrl("http://localhost:53863/login?username=mesut.agacyetistiren@petour.com&password=magacy227900").SetMethod("POST").Request<dynamic>();
            Console.WriteLine(token.token.Value);
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
