using System;
using Microsoft.AspNetCore.Mvc;
using shortid;
using shortid.Configuration;
using UrlShortner.Models;
using UrlShortner.Services.Url;

namespace UrlShortner.Controllers
{
    [ApiController]
    [Route("/api/v1/")]
    public class UrlController : ControllerBase
    {

        private readonly IUrlServices _urlServices;

        public UrlController(IUrlServices urlServices)
        {
            _urlServices = urlServices;
        }


        [HttpPost]
        [Route("url")]
        public JsonResult CreateShortUrl([FromForm] RequestUrl url)
        {

            var shortURl = _urlServices.CreateShortUrl(url: url);
            
            
            return shortURl != null ? new JsonResult(_urlServices.GetByShortURL(shortURl)
            ) : new JsonResult(
                "Something went wrong while Generating ShortUrl"
            );
        }

        [HttpGet]
        [Route("detail/{id}")]
        public JsonResult ShortUrlAllDetail(string id)
        {
            var responseUrl = _urlServices.GetByShortURL(id);
            return new JsonResult(responseUrl);
        }
        
        
        // GET
        [HttpGet]
        [Route("allUrl")]
        public JsonResult GetAllUrls(string userId)
        {
            var result = _urlServices.GetUrlsByUserId(userId);
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("error")]
        public JsonResult ErrorHandling(string msg)
        {
            return new JsonResult($"Message : '{msg}'");
        }
    }
}