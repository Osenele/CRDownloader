using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Net.Http.Headers;
using CRDownloader.Models;
using CRDownloader.Controllers.Helper;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CRDownloader.ViewModels;


namespace CRDownloader.Controllers.APIs
{
    public class CommentsController : ApiController
    {
        public IEnumerable<string> GetComments()
        {
            return new string[] { "Hello", "World" };
        }


        [HttpPost]
        public IHttpActionResult CreateComments(UrlViewModel url)
        {
            try
            {

                if (url.Url.ToLower().Contains("youtube"))
                {
                    List<Item> items = Helper.Helper.GetItems(url.Url);
                    string filePath = Helper.Helper.CreateCsv(items, url.Url);
                    Email.SendEmail(url.Email, filePath);
                }
                else if (url.Url.ToLower().Contains("amazon"))
                {
                    Helper.Amazon.HelperMethods.ProcessReviews(url.Url);
                }
                else
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }

                ;

                return Ok();
            }
            catch (Exception ex)
            {

                //Log Exception
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            
        }

    }
}
