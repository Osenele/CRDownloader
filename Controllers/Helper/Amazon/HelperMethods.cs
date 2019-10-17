using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using CRDownloader.Models.Amazon;
using System.Text.RegularExpressions;

namespace CRDownloader.Controllers.Helper.Amazon
{
    public class HelperMethods
    {
        private static string ReviewsSectionSeperator = @"<div id=reviewsMedley data-hook=reviews-medley-widget class=a-fixed-left-grid a-spacing-extra-large>";

        private static string AllReviewsLinkElement = "";
        private static string AllReviewsLinkElementPattern = @"<a data-hook=see-all-reviews-link-foot class=a-link-emphasis a-text-bold href=.+>";
        private static string AllReviewsLink = @"https://www.amazon.com" + AllReviewsLinkElement.Split(new[] { "href=" }, StringSplitOptions.None)[0].Replace(">", "");

        private static string ReviewSeperator = @"data-hook=review class=a-section review aok-relative>";

        private static string UserNamePattern = @"<span class=a-profile-name>.+</span>";
        private static string DatePattern = @"<span data-hook=review-date class=a-size-base a-color-secondary review-date>.+</span>";
        private static string StarRatingPattern = @"<span class=a-icon-alt>.+</span>";
        private static string ReviewPattern = @"<span data-hook=review-body class=a-size-base review-text review-text-content><span class=>.+</span></span>";
        private static string LinkPattern = @"<a data-hook=review-title class=a-size-base a-link-normal review-title a-color-base review-title-content a-text-bold.href=.+><span class=>.+</span></a>";



        public static void ProcessReviews(string url)
        {
            try
            {
                List<AmazonReview> amazonReviews = new List<AmazonReview>();
                string html = GetAmazonHtml(url);
                string[] reviews = html.Split(new[] { ReviewSeperator }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var review in reviews)
                {
                    amazonReviews.Add(GetReview(review));
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        public static string GetAmazonHtml(string url)
        {
            string data = "";
            try
            {
                
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.90 Safari/537.36";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
                
            }
            catch (Exception)
            {

                throw;
            }

            return data.Replace("\"", "");
        }

        public static List<AmazonReview> GetReviews(string html)
        {
            List<AmazonReview> amazonReviews = new List<AmazonReview>();
            try
            {


            }
            catch (Exception)
            {

                throw;
            }

            return amazonReviews;
        }

        public static AmazonReview GetReview(string reviewSection)
        {
            AmazonReview review = new AmazonReview();

            try
            {
                review.UserName = Regex.Match(reviewSection, UserNamePattern).Value;
                review.Date = Regex.Match(reviewSection, DatePattern).Value;
                review.StarRating = Regex.Match(reviewSection, StarRatingPattern).Value;
                review.Review = Regex.Match(reviewSection, ReviewPattern).Value;
                review.Link = Regex.Match(reviewSection, LinkPattern).Value;
            }
            catch (Exception)
            {

                throw;
            }

            return review;
        }
    }
}