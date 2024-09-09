using Azure.Messaging.ServiceBus;
using Azure.Identity;

string queueName = "servicebus-queue1";
string fdqn = "deltonsorg-servicebus.servicebus.windows.net";

ServiceBusClient serviceBusClient = new ServiceBusClient(fdqn,new DefaultAzureCredential());
ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(queueName);

using ServiceBusMessageBatch messageBatch = await serviceBusSender.CreateMessageBatchAsync();

for (int i =0; i <= 3; i++)
{
    if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
    {
        throw new Exception($"Exception {i} has occured");
    }
}

try
{
    await serviceBusSender.SendMessagesAsync(messageBatch);
    Console.WriteLine($"A batch of three messages has been published to the queue");
}
finally
{
    await serviceBusSender.DisposeAsync();
    await serviceBusClient.DisposeAsync();
}

Console.WriteLine("Follow the directions in the exercise to review the results in the Azure portal.");
Console.WriteLine("Press any key to continue");
Console.ReadKey();