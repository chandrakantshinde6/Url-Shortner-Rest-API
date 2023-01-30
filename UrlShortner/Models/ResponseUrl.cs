using System;

namespace UrlShortner.Models
{
    public class ResponseUrl
    {
       
        public int  UrlId { get; set; }
        public string UserId { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedAtDateTime { get; set; }
        public DateTime UpdatedAtDateTime { get; set; }
        public DateTime ExpiryDateTime { get; set; }
        
        
    }
}

