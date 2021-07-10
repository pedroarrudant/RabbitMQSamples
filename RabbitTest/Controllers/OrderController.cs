using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitTest.Controllers
{
    [Route("publisher/")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IPublishEndpoint publishEndpoint;

        public OrderController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        [HttpPost("postar-mensagem")]
        public ActionResult Create([FromBody] Order order)
        {
            publishEndpoint.Publish<Order>(order);

            return Ok();
        }

    }
}
