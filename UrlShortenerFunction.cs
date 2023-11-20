using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace UrlShortenerFunction
{
    public class UrlShortenerFunction
    {
        private readonly ILogger _logger;

        public UrlShortenerFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UrlShortenerFunction>();
        }

        [Function("UrlShortenerFunction")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            FunctionContext executionContext)
            {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

           // Read the long URL from the request body
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var longUrl = requestBody.Trim();

            // Generate a short code (you may use a library or a custom method)
            var shortCode = GenerateShortCode();

            // Store the mapping in Azure Storage (use Azure Table Storage or Cosmos DB)
            SaveUrlMapping(shortCode, longUrl);

            // Return the short URL
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString($"Short URL: https://yourdomain.com/{shortCode}");

            return new OkObjectResult(response);
        }

        private string GenerateShortCode()
        {
            // Implement your logic to generate a unique short code
            // This can involve using a library or a custom algorithm
            // For simplicity, you might use a GUID or a short random string
            return Guid.NewGuid().ToString("N").Substring(0, 6);
        }

        private void SaveUrlMapping(string shortCode, string longUrl)
        {
            // Implement your logic to save the mapping in Azure Storage
            // This can involve using Azure Table Storage or Cosmos DB
            // Ensure to handle uniqueness and collisions appropriately
            // For simplicity, you might use a dictionary or a database
            // Example: Save the mapping to a dictionary in memory
            urlMappings[shortCode] = longUrl;
        }


    
    }
}


            

