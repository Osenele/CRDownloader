using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRDownloader.Models.Amazon
{
    public class AmazonReview
    {
        public string UserName { get; set; }
        public string Date { get; set; }
        public string StarRating { get; set; }
        public string Review { get; set; }
        public string Link { get; set; }
    }
}