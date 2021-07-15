using MassTransit;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace RabbitMqConsumer
{
    public class ReceiveObserver : IReceiveObserver
    {
        public async Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
        {
            System.Diagnostics.Debug.WriteLine($"Mensagem do tipo {context.Message.GetType().Name} enviada com erro {exception.Message} do consumer.");
        }

        public async Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            System.Diagnostics.Debug.WriteLine($"Mensagem do tipo {context.Message.GetType().Name} id {context.MessageId} consumida.");
        }

        public async Task PostReceive(ReceiveContext context)
        {
            System.Diagnostics.Debug.WriteLine($"Mensagem {context.GetMessageId()} recebida.");
        }

        public async Task PreReceive(ReceiveContext context)
        {
            System.Diagnostics.Debug.WriteLine($"Mensagem {context.GetMessageId()} consumida.");
        }

        public async Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            if (context.Redelivered)
            {
                System.Diagnostics.Debug.WriteLine($"Mensagem {context.GetMessageId()} reenviada.");
            }
            System.Diagnostics.Debug.WriteLine($"Mensagem {context.GetMessageId()} enviada porém foi dada excessao {exception.Message} no consumer.");
        }
    }
}
