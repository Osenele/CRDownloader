using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRDownloader.Models
{
    public class Item : Metadata
    {
        public Snippet snippet { get; set; }

        public string GetUserName()
        {
            return snippet.topLevelComment.innerSnippet.authorDisplayName;
        }

        public DateTime GetDate()
        {
            return Convert.ToDateTime(snippet.topLevelComment.innerSnippet.updatedAt);
        }

        public int GetLikeCount()
        {
            return snippet.topLevelComment.innerSnippet.likeCount;                   
        }

        public string GetComment()
        {
            return snippet.topLevelComment.innerSnippet.textDisplay;                    
        }

        public string GetAuthorChannelLink()
        {
            return snippet.topLevelComment.innerSnippet.authorChannelUrl;
        }
    }
}