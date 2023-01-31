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

        
        [HttpGet]
        [Route("/{id}")]
        public RedirectResult RedirectUser(string id)
        {
            var url = _urlServices.GetByShortURL(id);
            if (url != null && url.OriginalUrl != null)
            {
                return new RedirectResult("http://localhost:5001");
            }
            return  new RedirectResult(url.OriginalUrl);
        }
        
        [HttpDelete]
        [Route("/{id}")]
        public JsonResult DeleteUrlByShortId(string id)
        {
            var result = _urlServices.DeleteUrlByShortId(id);
            if (result == null)
            {
                return new JsonResult(new { messahe = "Something went wrong" });
            }
            return new JsonResult( new {message = result});
        }


        [HttpPatch]
        [Route("/{id}")]
        public JsonResult UpdateUrlByUserIdAndShortId(string id, [FromBody] ResponseUrl url)
        {
            return new JsonResult(new { shortid = id }, new { url.OriginalUrl });
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