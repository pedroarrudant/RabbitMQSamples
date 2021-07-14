using MassTransit;
using Model;
using System.Threading.Tasks;

namespace RabbitPublisher.Services
{
    public interface IPublisherServices
    {

        async Task Publish(Order order, IPublishEndpoint publishEndpoint)
        {
            await publishEndpoint.Publish(order);
        }
    }
}