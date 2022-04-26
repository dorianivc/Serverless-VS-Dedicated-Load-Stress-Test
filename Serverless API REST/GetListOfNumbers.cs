using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Serverless_API_REST
{
    public static class GetListOfNumbers
    {

        [FunctionName("GetListOfNumbers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
           var random = new Random();
            int ExitCondition = 700;
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            while (ExitCondition != 0)
            {
                int value1=random.Next();
                int value2=random.Next();
                int value3= value1*value1 + value2*value2;
                list1.Add(value1);
                list2.Add(value3);
                list1.Add(value2);
                list1.Add(value1*value3);
                list1 = list1.Concat(list2).ToList();
                log.LogInformation("Value 1: "+ value1);
                log.LogInformation("Value 2: " + value2);
                log.LogInformation("Value 3: " + value3);
                ExitCondition--;
            }
            list2.Sort();
            string output="Sorted List: ";
            for (int i = 0; i < list2.Count; i++)
            {
                output = output + list2[i] + ", ";
                log.LogInformation(output);
            }
            output = output + " END";
            string responseMessage = output;

            return new OkObjectResult(responseMessage);
        }
    }
}
