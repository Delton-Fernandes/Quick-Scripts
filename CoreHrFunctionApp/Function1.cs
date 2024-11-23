using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;



using Azure.Messaging.ServiceBus;
/*using CoreHR_test.Models;
*/using Azure.Identity;

namespace CoreHrFunctionApp
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public async Task<string> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {

            Console.WriteLine("print test");
            Console.WriteLine(await sendMessage());

            return "" ;
            /*            return new OkObjectResult("Welcome to Azure Functions!");
            */
        }

        public async Task<string> sendMessage()
        {
            string fdqn = "corehrbusnamespace.servicebus.windows.net";
            string queueName = "noseshqueue";


            ServiceBusClient serviceBusClient = new ServiceBusClient(fdqn, new DefaultAzureCredential());
            ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(queueName);


            using ServiceBusMessageBatch serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync();

            serviceBusMessageBatch.TryAddMessage(new ServiceBusMessage("This is deltons message"));

            await serviceBusSender.SendMessagesAsync(serviceBusMessageBatch);

            return "Finished";

        }
    }
}
