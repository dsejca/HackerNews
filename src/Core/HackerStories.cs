using AppSettings;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;



namespace Core
{
    public class HackerStories : IHackerStories
    {
        private readonly ConfigAppSettings _configApp;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        public HackerStories(IOptions<ConfigAppSettings> configApp, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, IMapper mapper)
        {
            _configApp = configApp.Value;
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }
        public async Task<HackNewsResponse> HackerStoriesById(long id)
        {
            var response = await GetItemById(id);

            return response; 
        }

        public async Task<string> ListOfHackerStories(int numberOfStories)
        {
            var result = string.Empty;
            var hackerStories = new List<HackNewsResponse>();
            var mainList = await MainList();
            var number = mainList.Count();
            var counter =0;
            if (mainList.Any())
            {
                foreach (var item in mainList)
                {
                    if (counter >= numberOfStories)
                    {
                        break;
                    }
                    var response = await GetItemById(item);
                    if (response != null)
                    {
                        hackerStories.Add(response);
                    }
                    counter +=1;
                }
            }

            if (hackerStories.Count > 0)
            {
                var order = hackerStories.OrderByDescending(o => o.Score);
                result = JsonConvert.SerializeObject(order);
            }
            return result;
        }

        private async Task<IEnumerable<long>> MainList() 
        {
            IEnumerable<long> result = new List<long>();
            try
            {
                var client = _httpClientFactory.CreateClient();

                var getRequest = await client.GetFromJsonAsync<IEnumerable<long>>($"{_configApp.BaseUrl}{_configApp.BestStories}");

                if (getRequest != null)
                {
                    result = getRequest;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        

        private async Task<HackNewsResponse> GetItemById(long Id) 
        {
            var result = new HackNewsResponse();
            
            try
            {
                var responseCache = new ItemResponse();
                var findInCache = _memoryCache.TryGetValue(Id, out responseCache);
                if (findInCache && responseCache != null)
                {
                    result = _mapper.Map<HackNewsResponse>(responseCache);
                }
                else 
                {
                    var client = _httpClientFactory.CreateClient();
                    var request = await client.GetFromJsonAsync<ItemResponse>($"{_configApp.BaseUrl}{_configApp.ById}{Id}.json"); 
                    if (request != null) 
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(_configApp.MinutesForResetCache))
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(_configApp.MinutesForResetCache))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(long.MaxValue) ; 

                        _memoryCache.Set(Id, request, cacheEntryOptions);
                        result = _mapper.Map<HackNewsResponse>(request);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            return result;
        }
    }
}
