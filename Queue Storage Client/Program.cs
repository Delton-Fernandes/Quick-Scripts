using Azure.Storage.Queues;
using Azure.Identity;
using Azure.Storage.Queues.Models;

/*Uri storageAccountUri = new Uri("https://delbbbstore.queue.core.windows.net/queue-storage");
*/
string storageAccountName = "delbbbstore";
string queueName = "quickstartqueues-1";

/*QueueClient queueClient = new QueueClient(storageAccountUri, new DefaultAzureCredential());*/

QueueClient queueClient = new QueueClient(new Uri($"https://{storageAccountName}.queue.core.windows.net/{queueName}"),
    new DefaultAzureCredential());



async Task send() 
{
    try
    {
        Console.WriteLine("\nAdding messages to the queue...");

        // Send several messages to the queue
        await queueClient.SendMessageAsync("First message");
        await queueClient.SendMessageAsync("Second message");

        // Save the receipt so we can update this message later
        SendReceipt receipt = await queueClient.SendMessageAsync("Third message");
    }
    catch (Exception ex)
    {

        Console.WriteLine($"An error occurred: {ex.Message}");
    }


}

async Task peek()
{
    Console.WriteLine("\nPeek at the messages in the queue...");

    // Peek at messages in the queue
    PeekedMessage[] peekedMessages = await queueClient.PeekMessagesAsync(maxMessages: 10);

    foreach (PeekedMessage peekedMessage in peekedMessages)
    {
        // Display the message
        Console.WriteLine($"Message: {peekedMessage.MessageText}");
    }

}

async Task update()
{
    Console.WriteLine("\nUpdating the third message in the queue...");
    SendReceipt receipt = await queueClient.SendMessageAsync("Third message");

    // Update a message using the saved receipt from sending the message
    await queueClient.UpdateMessageAsync(receipt.MessageId, receipt.PopReceipt, "Third message has been updated");
}

async Task getMessages()
{
    Console.WriteLine("\nReceiving messages from the queue...");

    // Get messages from the queue
    QueueMessage[] messages = await queueClient.ReceiveMessagesAsync(maxMessages: 10,visibilityTimeout:TimeSpan.Zero);

    foreach (QueueMessage m in messages)
    {
        // Display the message
        Console.WriteLine($"Message: {m.Body}");
    }
}



await getMessages();
