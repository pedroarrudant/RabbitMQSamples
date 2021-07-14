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
            ConsumeContext<Order> message;

            context.TryGetMessage<Order>(out message);

            if (message.Message.Name == "Redeliver")
            {
                if (message.GetRedeliveryCount() > 3)
                {
                    System.Diagnostics.Debug.WriteLine($"Mensagem reenviada mais de 3 vezes.");
                }
            }
        }

        public async Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            System.Diagnostics.Debug.WriteLine($"Mensagem do tipo {context.Message.GetType().Name} id {context.MessageId} consumida.");
        }

        public async Task PostReceive(ReceiveContext context)
        {
            System.Diagnostics.Debug.WriteLine($"Mensagem {context.GetMessageId()} enviada e processada.");

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Order));
            var body = new MemoryStream(context.GetBody());
            var totalMessage = JsonDeserializer<Order>(body);

            var streamReader = new StreamReader(body);
        }

        public async Task PreReceive(ReceiveContext context)
        {
            System.Diagnostics.Debug.WriteLine($"Mensagem {context.GetMessageId()} consumida.");

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Order));
            var body = new MemoryStream(context.GetBody());
            var totalMessage = JsonDeserializer<Order>(body);

            var streamReader = new StreamReader(body);

        }

        public async Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine($"Mensagem {context.GetMessageId()} enviada porém foi dada excessao {exception.Message} cedo no consumer.", context.GetBody().ToString());
        }

        public static T JsonDeserializer<T>(MemoryStream strm)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            return (T)serializer.ReadObject(strm);
        }
    }
}
