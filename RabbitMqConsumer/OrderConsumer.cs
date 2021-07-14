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
            //throw new InvalidOperationException();
            System.Diagnostics.Debug.WriteLine(context.Message.Name);

            if (context.Message.Name == "Redeliver")
            {
                context.Redeliver(TimeSpan.FromSeconds(300));
                context.Publish(context.Message);
            }

            if (context.Message.Name == "Error")
            {
                throw new Exception();
            }
        }
    }
}