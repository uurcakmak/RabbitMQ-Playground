using RabbitMQ.Domain.Models;
using RabbitMQ.Helper.Abstraction;
using RestSharp;
using RestSharp.Authenticators;
#pragma warning disable CS8603

namespace RabbitMQ.Helper.Implementation
{
    public class RabbitApi : IRabbitApi
    {
        private readonly RestClient _client;

        public RabbitApi(string url, string userName, string password)
        {
            _client = new RestClient(url);
            _client.Authenticator = new HttpBasicAuthenticator(userName, password);
        }

        public async Task<List<Queue>> ListQueues()
        {
            var request = await _client.GetAsync<List<Queue>>(new RestRequest("queues")).WaitAsync(CancellationToken.None);

            return request;
        }

        public async Task<List<Exchange>> ListExchanges()
        {
            var request = await _client.GetAsync<List<Exchange>>(new RestRequest("exchanges")).WaitAsync(CancellationToken.None);

            return request?.Where(s => !string.IsNullOrEmpty(s.Name)).ToList();
        }
    }
}
