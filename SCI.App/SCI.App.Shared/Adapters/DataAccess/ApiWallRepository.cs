using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SCI.Adapters.DataAccess;
using SCI.BL.Entities;

namespace SCI.App.Adapters.DataAccess
{
    public class ApiWallRepository : IWallRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonWallEntryConverter _converter = new JsonWallEntryConverter();

        public ApiWallRepository(string apiBaseAddress)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiBaseAddress)
            };
        }

        public async Task SaveAsync(WallEntry wallEntry)
        {
            var response = await _httpClient.PostAsJsonAsync("", wallEntry);
        }

        public async Task<IQueryable<WallEntry>> GetAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("");
                return JsonConvert.DeserializeObject<IEnumerable<WallEntry>>
                (response, _converter)
                .AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<WallEntry>().AsQueryable();                
            }
        }

        public async Task UpdateAsync(WallEntry wallEntry)
        {
            var response = await _httpClient.PutAsJsonAsync("", wallEntry);
        }

        public async Task DeleteAsync(WallEntry wallEntry)
        {
            var response = await _httpClient.DeleteAsync(wallEntry.Id.ToString());
        }
    }
}