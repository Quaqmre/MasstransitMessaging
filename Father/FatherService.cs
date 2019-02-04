using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Proje.Model;

namespace MakeTransaction
{
    public class FatherService
    {
        public int totalEntryMount;
        public int totalRequested;

        public FatherService()
        {
            totalEntryMount = 0;
        }

        public void determinetotalPaid(int price)
        {
            totalEntryMount += price;
        }
        public void determinetotalrequested(int price)
        {
            totalRequested += price;
        }


    }

}