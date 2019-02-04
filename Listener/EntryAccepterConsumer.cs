using System;
using System.Threading.Tasks;
using MassTransit;
using Proje.Model;

namespace Proje.Listener
{
    public class EntryAccepterConsumer : IConsumer<IEntry>
    {
        public async Task Consume(ConsumeContext<IEntry> context)
        {
            if (Random.Next(1, 3) == 1)
            {
                await context.Publish<IEntryResult>(new
                {

                    Entrytransactions = context.Message.Entrytransactions
                });
                foreach (var item in context.Message.Entrytransactions)
                {
                    System.Console.WriteLine(item.Name);

                    System.Console.WriteLine(item.Price);
                }
                Console.WriteLine("Ödexme başarılı");
            }
            else
                throw new Exception("Ödemede hata oldu");

        }

        private static readonly Random Random = new Random();
    }
}