using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace azurefunction1
{
    public static class SaveToTable
    {
        [FunctionName("SaveToTable")]
        [return: Table("messages")]
        public static async Task<DeviceMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<DeviceMessage>(requestBody);

            data.PartitionKey = "Consoleapp";
            data.RowKey = Guid.NewGuid().ToString();

            log.LogInformation(requestBody);

            return data;

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

