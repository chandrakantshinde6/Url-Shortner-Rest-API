using System.Collections.Generic;
using UrlShortner.Models;

namespace UrlShortner.Services.Url
{
    public interface IUrlServices
    {
        public ResponseUrl GetByShortURL(string shortId);
        public string CreateShortUrl(RequestUrl url);
        public List<ResponseUrl> GetUrlsByUserId(string userId);
        public string DeleteUrlByShortId(string shortId);
        public string UpdateUrlByShortId(string shortId, string userId);



    }
}