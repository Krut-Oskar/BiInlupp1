using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;

namespace azurefunction1
{
    public static class SaveToTable
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("SaveToTable")]
        [return: Table("messages")]
        public static DeviceMessage Run([IoTHubTrigger("messages/events", Connection = "IoTHub", ConsumerGroup = "table")] EventData message, ILogger log)
        {




            var json = JsonConvert.DeserializeObject<DeviceMessage>(Encoding.UTF8.GetString(message.Body.Array));
            json.PartitionKey = message.SystemProperties["iothub-connection-device-id"].ToString();
            json.RowKey = Guid.NewGuid().ToString();
            json.name = message.Properties["Name"].ToString();
            json.school = message.Properties["School"].ToString();
            json.time = message.Properties["TS"].ToString();

            return json;




        }
    }

    public class DeviceMessage : TableEntity
    {
        public int number { get; set; }
        public string time { get; set; }

        public string name { get; set; }
        public string school { get; set; }

    }
}