using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace azurefunction1
{
    public static class SplitFunction
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("SplitFunction")]
        public static async Task Run([IoTHubTrigger("messages/events", Connection = "IoTHub", ConsumerGroup = "table")] EventData message, ILogger log)
        {
            string type = message.Properties["Name"].ToString();
            HttpContent payload = new StringContent(Encoding.UTF8.GetString(message.Body.Array), Encoding.UTF8, "application/json");

           
            

            
            switch (type)
            {
                case "Conny":
                    //Save to Table Storage
                    await client.PostAsync(new Uri(Environment.GetEnvironmentVariable("TableUrl")), payload);

                    break;
                case "William":
                    //Save to Cosmos Database
                    await client.PostAsync(new Uri(Environment.GetEnvironmentVariable("CosmosUrl")), payload);

                    break;
            }
        }
    }
}