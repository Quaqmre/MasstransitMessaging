using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Proje.Model;

namespace MakeTransaction
{
    class Program
    {
        static void Main(string[] args)
        {
            FatherService fatherservice = new FatherService();

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
           {
               var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
               {
                   h.Username("guest");
                   h.Password("guest");
               });

               sbc.ReceiveEndpoint(host, "Father", ep =>
               {
                   ep.Consumer(() => new FatherResultConsumer(fatherservice));
                   ep.Consumer(() => new FatherConsumer(fatherservice));
               });
           });

            bus.Start();

            for (; ; )
            {
                System.Console.WriteLine("Sipariş geçilen Toplam Tutar:" + fatherservice.totalRequested);
                System.Console.WriteLine("Karttan Çekilen Toplam Tutar:" + fatherservice.totalEntryMount);
                System.Console.WriteLine("Rapor Görüntülemek için Bir tuşa basınız");

                Console.ReadKey(true);
            }
        }
    }
}
