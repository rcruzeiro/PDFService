using System;
using Core.Framework.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace PDFService.API.Controllers
{
    public abstract class BaseController : Controller
    {
        readonly ICacheService _cache;
        protected readonly IConfiguration _configuration;

        protected BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
            _cache = CacheFactory.Instance.Redis(
                _configuration.GetValue<string>("Redis:Connection"),
                _configuration.GetValue<int>("Redis:Database"));
            _cache.Expires = TimeSpan.FromDays(
                _configuration.GetValue<int>("Redis:Expires"));
        }

        protected bool ExistsInCache(string key)
        {
            try
            {
                return _cache.Exists(key);
            }
            catch (Exception ex)
            { throw ex; }
        }

        protected T GetFromCache<T>(string key)
        {
            try
            {
                return (T)JsonConvert.DeserializeObject(_cache.Get(key));
            }
            catch (Exception ex)
            { throw ex; }
        }

        protected void AddToCache<T>(string key, T value)
        {
            try
            {
                _cache.Set(key, JsonConvert.SerializeObject(value));
            }
            catch (Exception ex)
            { throw ex; }
        }

        protected void RemoveFromCache(string key)
        {
            try
            {
                _cache.Remove(key);
            }
            catch (Exception ex)
            { throw ex; }
        }

        protected void ClearCache()
        {
            try
            {
                _cache.Clear();
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
