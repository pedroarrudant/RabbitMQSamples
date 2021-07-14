using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RabbitMqConsumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Criação de conexão com o RabbitMq
            //services.AddMassTransit(
            //    config =>
            //    {
            //        config.AddConsumer<OrderConsumer>();
            //        config.UsingRabbitMq(
            //            (ctx, cfg) =>
            //            {
            //                cfg.ConnectReceiveObserver(new ReceiveObserver());

            //                cfg.Host("amqp://guest:guest@localhost:5672");

            //                cfg.ReceiveEndpoint("order-queue", c =>
            //                {
            //                    c.ConfigureConsumer<OrderConsumer>(ctx);
            //                });
            //            }
            //            );
            //    }
            //    );

            services.AddMassTransit(
                config => config.UsingRabbitMq()
                );

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("amqp://guest:guest@localhost:5672");

                cfg.ReceiveEndpoint("order-queue", e =>
                {
                    e.Consumer<OrderConsumer>();
                });
            });

            var observer = new ReceiveObserver();

            busControl.StartAsync();

            var handle = busControl.ConnectReceiveObserver(observer);


            //Ele cria o Bus e controla o mesmo
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
