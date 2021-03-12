using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace azurefunction1
{
    public static class SaveToCosmos
    {
        [FunctionName("SaveToCosmos")]


        public static void Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "IOT20", collectionName: "Measurements", CreateIfNotExists = true, ConnectionStringSetting = "CosmosDB")] out dynamic cosmos,
            ILogger log)
        {

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            cosmos = requestBody;
            log.LogInformation(requestBody);

        }
    }
}


