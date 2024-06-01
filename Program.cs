using System;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
class Program
{
    static async Task Main(string[] args)
    {
        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();
        var dateNow = DateTime.Now;

        var options = new MqttClientOptionsBuilder()
            .WithClientId("MqttQrReaderClient")
            .WithTcpServer("10.5.235.195", 1883) // Replace with your MQTT broker host
            .WithCleanSession()
            .Build();

        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            var studentCin = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            Console.WriteLine($"Received message: date :  {dateNow} , {studentCin} ");
            return Task.CompletedTask;
        };

        mqttClient.ConnectedAsync += async e =>
        {
            Console.WriteLine("Connected to MQTT broker");
            await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("attendance/scan").Build());
            Console.WriteLine("Subscribed to topic 'attendance/scan'");
        };

        mqttClient.DisconnectedAsync += e =>
        {
            Console.WriteLine("Disconnected from MQTT broker");
            return Task.CompletedTask;
        };

        await mqttClient.ConnectAsync(options);

        Console.WriteLine("Press any key to exit");
        Console.ReadLine();

        await mqttClient.DisconnectAsync();
    }
}
