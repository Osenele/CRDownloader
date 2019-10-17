using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CRDownloader.Models
{
    //[JsonArray()]
    public class YoutubeCommentThread
    {
        public string kind { get; set; }

        public string etag { get; set; }

        public string nextPageToken { get; set; }
        public object pageInfo { get; set; }

        public List<Item> items { get; set; }
    }
}