using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Proje.Model;

namespace MakeTransaction
{
    class FatherResultConsumer : IConsumer<IEntryResult>
    {
        private readonly FatherService _fatherService;
        public FatherResultConsumer(FatherService service)
        {
            _fatherService = service;
        }
        public Task Consume(ConsumeContext<IEntryResult> context)
        {
            _fatherService.determinetotalPaid(context.Message.Entrytransactions.Sum(x => x.Price));
            //System.Console.WriteLine(context.Message.Entrytransactions.Sum(x => x.Price));
            return Task.CompletedTask;
        }

    }

}