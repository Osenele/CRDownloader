using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRDownloader.Models
{
    public class Snippet
    {
        public string videoId { get; set; }
        public TopLevelComment topLevelComment { get; set; }
        public bool canReply { get; set; }
        public int totalReplyCount { get; set; }
        public bool isPublic { get; set; }

    }
}