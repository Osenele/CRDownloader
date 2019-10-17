using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CRDownloader.Models
{
    public class TopLevelComment : Metadata
    {
        [JsonProperty("snippet")]
        public InnerSnippet innerSnippet { get; set; }
    }
}