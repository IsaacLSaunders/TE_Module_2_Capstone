using RestSharp;
using System;
using System.Net.Http;
using TEBucksServer.Services;


namespace TEBucksServer.NewFolder
{
    public class TEARSLoggerApiService : ITearsLog
    {
        protected static RestClient client = null;
        private string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI4MDc3IiwibmFtZSI6InRlYW0wSSZFIiwibmJmIjoxNjk5MjE0OTc2LCJleHAiOjE2OTkzMDEzNzYsImlhdCI6MTY5OTIxNDk3Nn0.e9xP22QgOelKCWqjIFeLrFbVeCGAoGyyc7xyHnvQg8g";
        public TEARSLoggerApiService(string apiUrl)
        {
            if (client == null)
            {
                client = new RestClient(apiUrl);
                client.AddDefaultHeader("Authorization", $"Bearer {token}");
            }
        }

        //right now we arent doing anything with the response object here but we could create a table in the DB to store a record of the created log
        public TEARSLogModel Log(TEARSLogModel log)
        {
            RestRequest request = new RestRequest("api/TxLog");
            request.AddJsonBody(log);
            IRestResponse<TEARSLogModel> response = client.Post<TEARSLogModel>(request);
            LogErrorCheck(response);
            return response.Data;
        }

        private static void LogErrorCheck(IRestResponse response)
        {
            string message = "";
            string messageDescription = "";

            if(response.ResponseStatus != ResponseStatus.Completed)
            {
                message = "Could not reach the TEARS server.";
                messageDescription = $"{response.StatusDescription} Status code {(int)response.StatusCode}";
                throw new HttpRequestException($"{message}\n{messageDescription}");
            }
            else if (!response.IsSuccessful)
            {
                message = "HTTP error occurred.";
                messageDescription = $"{response.StatusDescription} Status code {(int)response.StatusCode}";
                throw new HttpRequestException($"{message}\n{messageDescription}");
            }
        }

    }
}
