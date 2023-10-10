using System.Text;
using System.Text.Json;
using Auth.Services.AsyncDataServices.Interfaces;
using Auth.Services.ViewModels.PublishedAccountModels;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Auth.Services.AsyncDataServices;
public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"]!)
        };
        System.Console.WriteLine($"{factory.HostName}, {factory.Port}");
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown!;
            System.Console.WriteLine("--> Connected to Message Bus");

        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
            throw new Exception(ex.Message);
        }
    
    }
    public void PublishNewAccount(UserPublishedModel userPublishedModel)
    {
        var message = JsonSerializer.Serialize(userPublishedModel);
        if (_connection.IsOpen)
        {
            System.Console.WriteLine("--> Sending Message To Rabbit Mq");
            SendMessage(message);
        }
        else
        {
            System.Console.WriteLine("--> RabbitMQ Connection closed! Not sending!");
        }
    }

     private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange : "trigger", routingKey : "", basicProperties : null, body : body);
            System.Console.WriteLine($"--> System send {message}");
        }
    public void Dispose()
    {
        System.Console.WriteLine("--> MessageBus Disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        System.Console.WriteLine("--> RabbitMQ Connection Shutdown");
    }
}