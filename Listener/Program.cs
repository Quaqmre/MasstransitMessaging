using System;
using System.Linq;
using MassTransit;
using Proje.Listener;

namespace Model.Listener
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

                sbc.ReceiveEndpoint(host, "Payments", ep =>
                {
                    ep.Consumer(() => new EntryAccepterConsumer());
                });
            });

            bus.Start();

            Console.WriteLine("Ödeme Hizmetidir");
            Console.WriteLine("Çıkmak için Q ya basınız");
            while (Console.ReadKey(true).Key != ConsoleKey.Q) ;

            bus.Stop();
        }
    }
}
