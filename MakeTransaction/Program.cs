using System;
using System.Collections.Generic;
using System.Linq;
using MassTransit;
using Proje.Model;

namespace MakeTransaction
{
    class Program
    {


        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "Shop", ep =>
                {
                    ep.Consumer(() => new EntryConsumer());
                });
            });

            bus.Start();

            Console.WriteLine("Alışveriş Yapınız");
            Console.WriteLine("Çıkmak için press Q");
            Console.WriteLine("0-9 arası bir Sipariş Seçiniz");
            Console.WriteLine(string.Join(Environment.NewLine, Bakkal.Select((x, i) => $"[{i}]: {x.name} - {x.price}")));

            var bakkal = new List<(string name, int price)>();
            for (; ; )
            {
                var consoleKeyInfo = Console.ReadKey(true);
                if (consoleKeyInfo.Key == ConsoleKey.Q)
                {
                    break;
                }


                if (char.IsNumber(consoleKeyInfo.KeyChar))
                {
                    // ...
                    var product = Bakkal[(int)char.GetNumericValue(consoleKeyInfo.KeyChar)];
                    bakkal.Add(product);
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine($"Eklendi {product.name}");

                }

                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    bus.Publish<IEntry>(new
                    {
                        Entrytransactions = bakkal.Select(x => new { Name = x.name, Price = x.price }).ToList()
                    });

                    Console.WriteLine("Sipariş Geçildi");
                    bakkal.Clear();
                }
            }

            bus.Stop();
        }

        public static readonly IReadOnlyList<(string name, int price)> Bakkal = new List<(string, int)> { ("Goflet", 1), ("Cips", 2), ("Araba", 30) };

    }
}
