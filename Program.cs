using System;


namespace AzureServiceBusTopicSend
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Configuration;

    class Program
    {
        const string connectionString = "<your_connection_string>";
        const string TopicName = "demotopic4699";
        static ITopicClient topicClient;

        public static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
          
            var connectionString = config["connectionString"];
            topicClient = new TopicClient(connectionString, TopicName);

            while (true)
            {
                Console.WriteLine("Type m to send a message to topic, q to quit");
                var keyInfo = Console.ReadKey();  

                if (keyInfo.KeyChar == 'm')
                {
                    Console.Write("\nMessage: ");
                    var line = Console.ReadLine();
                    await topicClient.SendAsync(new Message() { Body = Encoding.ASCII.GetBytes(line)});
                }

                if (keyInfo.KeyChar == 'q')
                {
                    break;
                }
            }  


            Console.ReadKey();

            await topicClient.CloseAsync();
        }
    }
}