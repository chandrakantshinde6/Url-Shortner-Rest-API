using System.Collections.Generic;
using UrlShortner.Models;

namespace UrlShortner.Services.Url
{
    public interface IUrlServices
    {
        public ResponseUrl GetByShortURL(string shortId);
        public string CreateShortUrl(RequestUrl url);
        public List<ResponseUrl> GetUrlsByUserId(string userId);
        public void DeleteUrlByShortId(string shortId);
        public List<ResponseUrl> GetUrlsByUserIdAndShortId(string userID, string shortId);
        
        
        
        

    }
}