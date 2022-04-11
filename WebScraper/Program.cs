using System;
using System.Linq;
using WebScraper.Models;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new RentAPlaceContext();
            Console.WriteLine(db.Districts.FirstOrDefault().Name);
        }
    }
}
