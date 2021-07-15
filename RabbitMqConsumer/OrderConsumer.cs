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

            if (context.Message.Name == "Rejected")
            {
                int? maxAttempts = context.GetRedeliveryCount();

                if (maxAttempts > 3)
                {
                    System.Diagnostics.Debug.WriteLine($"Número de tentativas {maxAttempts}, excedeu o número máximo {3}.");
                    throw new Exception("Número de tentativas excedidas.");
                }

                await context.Defer(TimeSpan.FromSeconds(15));
            }

            if (context.Message.Name == "Error")
            {
                throw new NotImplementedException();
            }

            if (context.Message.Name == "Success")
            {
                await context.Publish<OrderSubmitted>(new { context.Message.Name });
            }
        }
    }
}