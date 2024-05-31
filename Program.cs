namespace demo_Qr_Mqtt
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var mqttClientHandler = new MqttClientHandler();
            await mqttClientHandler.StartAsync();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            await mqttClientHandler.StopAsync();
        }
    }
}
