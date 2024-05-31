using System;
using System.Text;
using System.Text.Json; // For System.Text.Json
// using Newtonsoft.Json.Linq; // Uncomment if using Newtonsoft.Json
using System.Threading.Tasks;
using demo_Qr_Mqtt;
using MQTTnet;
using MQTTnet.Client;

namespace demo_Qr_Mqtt
{
    public class MqttClientHandler
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _options;
        private readonly DatabaseHandler _databaseHandler;

        public MqttClientHandler()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // Load settings from configuration
            var brokerAddress = ConfigHelper.GetAppSetting("MqttBrokerAddress");
            var brokerPortStr = ConfigHelper.GetAppSetting("MqttBrokerPort");
            var clientId = ConfigHelper.GetAppSetting("MqttClientId");

            if (string.IsNullOrEmpty(brokerAddress))
            {
                throw new ArgumentNullException(nameof(brokerAddress), "MQTT broker address cannot be null or empty.");
            }

            if (!int.TryParse(brokerPortStr, out var brokerPort))
            {
                throw new ArgumentException("MQTT broker port must be a valid integer.", nameof(brokerPortStr));
            }

            _options = new MqttClientOptionsBuilder()
                .WithClientId(clientId)
                .WithTcpServer(brokerAddress, brokerPort)
                .WithCleanSession()
                .Build();

            _databaseHandler = new DatabaseHandler();
            ConfigureMqttClient();
        }

        private void ConfigureMqttClient()
        {
            _mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                var dateNow = DateTime.Now;

                try
                {
                    // Parse the JSON payload
                    var jsonDoc = JsonDocument.Parse(payload);
                    var studentCin = jsonDoc.RootElement.GetProperty("qr").GetProperty("content").GetString();

                    // Or use Newtonsoft.Json
                    // var jsonObject = JObject.Parse(payload);
                    // var studentCin = (string)jsonObject["qr"]["content"];

                    Console.WriteLine($"Received message: date : {dateNow}, {studentCin}");

                    await _databaseHandler.InsertIntoDatabaseAsync(studentCin, dateNow);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            };

            _mqttClient.ConnectedAsync += async e =>
            {
                Console.WriteLine("Connected to MQTT broker");
                await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("attendance/scan").Build());
                Console.WriteLine("Subscribed to topic 'attendance/scan'");
            };

            _mqttClient.DisconnectedAsync += e =>
            {
                Console.WriteLine("Disconnected from MQTT broker");
                return Task.CompletedTask;
            };
        }

        public async Task StartAsync()
        {
            await _mqttClient.ConnectAsync(_options);
        }

        public async Task StopAsync()
        {
            await _mqttClient.DisconnectAsync();
        }
    }
}
