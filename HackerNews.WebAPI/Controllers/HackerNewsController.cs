using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNews.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static HackerNews.WebAPI.Models.UrlProvider;
using HackerNews.WebAPI.Services;

namespace HackerNews.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private HackerNewsApiService service;

        public HackerNewsController()
        {
            service = new HackerNewsApiService();
        }

        // GET: api/HackerNews
        [HttpGet]
        public async Task<string> Get()
        {
            return await service.GetStoriesAsync();
        }

        // GET: api/HackerNews/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/HackerNews
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/HackerNews/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
