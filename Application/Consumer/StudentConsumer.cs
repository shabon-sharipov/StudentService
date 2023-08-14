using System.Text;
using System.Text.Json;
using Domain.Models;
using System.Text;
using System.Text.Json;
using Application.Common.interfaces;
using Application.Common.interfaces.Enum;
using Application.Consumer.Models;
using RabbitMQ.Client;

namespace Application.Consumer;

public class StudentConsumer
{
    private HelperClass _helperClass;

    public StudentConsumer()
    {
        _helperClass = new HelperClass();
    }

    public void SendToRabbitMq(Student student, EntityChangeEventType entityChangeEventType)
    {
        var factory = _helperClass.ConnectionFactory();
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare(Constant.ExchangeName, ExchangeType.Direct);
        channel.QueueDeclare(Constant.QueueName, false, false, false, null);
        channel.QueueBind(Constant.QueueName, Constant.ExchangeName, Constant.RoutinKey, null);

        var serislixe = JsonSerializer.Serialize(new BaseModel
            { Id = student.Id, Type = nameof(Student), EntityChangeEventType = entityChangeEventType });

        var message = Encoding.UTF8.GetBytes(serislixe);
        try
        {
            channel.BasicPublish(Constant.ExchangeName, Constant.RoutinKey, null, message);
        }
        catch (Exception e)
        {
            throw new Exception($"When Publish data to RabbitMq: {e.Message}");
        }

        channel.Close();
        connection.Close();
    }
}