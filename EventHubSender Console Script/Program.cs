﻿using Azure.Identity;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Text;
using System.Threading;


// number of events to be sent to the event hub
int numOfEvents = 100;

// The Event Hubs client types are safe to cache and use as a singleton for the lifetime
// of the application, which is best practice when events are being published or read regularly.
// TODO: Replace the <EVENT_HUB_NAMESPACE> and <HUB_NAME> placeholder values
EventHubProducerClient producerClient = new EventHubProducerClient(
    "deltonsorgeventhub.servicebus.windows.net",
    "eventhub1test",
    new DefaultAzureCredential());

// Create a batch of events 
/*using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();


for (int i = 1; i <= numOfEvents; i++)
{
    if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"Event {i}"))))
    {
        // if it is too large for the batch
        throw new Exception($"Event {i} is too large for the batch and cannot be sent.");
    }
    Thread.Sleep(500);
}


try
{
    // Use the producer client to send the batch of events to the event hub
    await producerClient.SendAsync(eventBatch);
    Console.WriteLine($"A batch of {numOfEvents} events has been published.");
    Console.ReadLine();
}
finally
{
    await producerClient.DisposeAsync();
}
*/

for (int i = 0; i < numOfEvents; i++)
{
    using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

    eventBatch.TryAdd(new EventData("Event" + i));

    await producerClient.SendAsync(eventBatch);

    eventBatch.Dispose();

    Thread.Sleep(500);

}