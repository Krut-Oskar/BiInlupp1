using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace azurefunction1
{
    public static class SaveToCosmos
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("SaveToCosmos")]
        public static void Run([IoTHubTrigger("messages/events", Connection = "IoTHub", ConsumerGroup = "cosmos")] EventData message,
            [CosmosDB(databaseName: "IOT20", collectionName: "Measurements", CreateIfNotExists = true, ConnectionStringSetting = "CosmosDB")] out dynamic cosmos,
            ILogger log)
        {
            //log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");


            var msg = JsonConvert.DeserializeObject<DhtMeasurements>(Encoding.UTF8.GetString(message.Body.Array));
            msg.DeviceId = message.SystemProperties["iothub-connection-device-id"].ToString();
            msg.Name = message.Properties["Name"].ToString();
            msg.School = message.Properties["School"].ToString();
            msg.Timestamp = message.Properties["TS"].ToString();

            var json = JsonConvert.SerializeObject(msg);
            cosmos = json;





        }
    }
}