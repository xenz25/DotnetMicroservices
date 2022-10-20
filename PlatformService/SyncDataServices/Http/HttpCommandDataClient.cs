using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _client;

        public HttpCommandDataClient(HttpClient client, IConfiguration config)
        {
            _client = client;
            _client.BaseAddress = new Uri(config["CommandService"]);
        }

        public async Task<bool> SendPlatformToCommand(PlatformReadDto platform)
        {
            var requestContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("api/commands/platforms", requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}