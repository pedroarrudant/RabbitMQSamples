using MassTransit;
using Model;
using System;
using System.Threading.Tasks;

namespace RabbitMqConsumer
{
    internal class OrderConsumer : IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {
            System.Diagnostics.Debug.WriteLine(context.Message.Name);
        }
    }
}