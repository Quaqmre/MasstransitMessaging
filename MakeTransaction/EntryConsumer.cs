using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Proje.Model;

namespace MakeTransaction
{
    public class EntryConsumer : IConsumer<Fault<IEntry>>
    {
        public Task Consume(ConsumeContext<Fault<IEntry>> context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Siparişiniz Onaylanmamış Fiş kesilememiştir:");
            foreach (var transaction in context.Message.Message.Entrytransactions.GroupBy(x => x.Name))
            {
                Console.WriteLine($"{transaction.Key} x{transaction.Count()}");
            }
            Console.ResetColor();
            return Task.CompletedTask;
        }
    }
}