# RabbitMQ Sample

Repo utilizado para techtalk sobre resiliencia e filas sobre rabbit mq.

![](../header.png)


## Configuração para Desenvolvimento

Subindo uma instancia do Rabbit MQ do Docker na porta padrão.

Com o Docker Desktop já instalado e configurado execute o seguinte comando no shell de sua preferencia:
```
docker pull rabbitmq
```

Execute o comando seguinte para abrir as portas necessárias ao rabbit.
```
docker run -d --hostname my-rabbit --name rabbitmq -p 15672:15672 -p 5672:5672 -p 25676:25676 rabbitmq
```

Já dentro do container execute o seguinte comando para habilitar a interface de gerenciamento do Rabbit MQ
```
rabbitmq-plugins enable rabbitmq_management
```

Em alguns casos pode haver a necessidade de desabilitar a flag ```management_agent.disable_metrics_collector ``` de acordo com a issue abaixo.
[Issue3112](https://github.com/rabbitmq/rabbitmq-server/discussions/3112)

## Meta

Pedro Arruda – [@pedroarrudant](https://twitter.com/pedroarrudant) – pedro.arruda@outlook.com.br

[https://github.com/pedroarrudant](https://github.com/pedroarrudant)

## Contributing

1. Faça o _fork_ do projeto (https://github.com/pedroarrudant/AutoInstaller/fork)
2. Crie uma _branch_ para sua modificação (`git checkout -b feature/fooBar`)
3. Faça o _commit_ (`git commit -am 'Add some fooBar'`)
4. _Push_ (`git push origin feature/fooBar`)
5. Crie um novo _Pull Request_