using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CRDownloader.Models
{
    public class InnerSnippet
    {
        public string authorDisplayName { get; set; }
        public string authorProfileImageUrl { get; set; }
        public string authorChannelUrl { get; set; }
        public AuthorChannelId authorChannelId { get; set; }
        public string videoId { get; set; }
        public string textDisplay { get; set; }
        public string textOriginal { get; set; }
        public string canRate { get; set; }
        public string viewerRating { get; set; }
        public int likeCount { get; set; }
        public string publishedAt { get; set; }
        public string updatedAt { get; set; }



    }
}