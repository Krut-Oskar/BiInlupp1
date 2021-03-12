using System.Collections.Generic;
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
    public static class GetFromCosmos
    {
        [FunctionName("GetFromCosmos")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "IOT20", collectionName: "Measurements", CreateIfNotExists = true, ConnectionStringSetting = "CosmosDB", SqlQuery = "SELECT * FROM c")] IEnumerable<DhtMeasurements> cosmos,
            ILogger log)
        {
            return new OkObjectResult(cosmos);
        }
    }
}

