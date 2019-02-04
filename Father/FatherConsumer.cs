using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Proje.Model;

namespace MakeTransaction
{
    class FatherConsumer : IConsumer<IEntry>
    {
        private readonly FatherService _fatherService;
        public FatherConsumer(FatherService service)
        {
            _fatherService = service;
        }
        public Task Consume(ConsumeContext<IEntry> context)
        {
            _fatherService.determinetotalrequested(context.Message.Entrytransactions.Sum(x => x.Price));


            //System.Console.WriteLine(context.Message.Entrytransactions.Sum(x => x.Price));
            return Task.CompletedTask;
        }

    }

}