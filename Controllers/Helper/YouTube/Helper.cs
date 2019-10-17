using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CRDownloader.Models;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;

namespace CRDownloader.Controllers.Helper
{
    public class Helper
    {
        private static string FilePath = @ConfigurationManager.AppSettings["FilePath"];

        private const string YouTubeAPIUrl = "https://www.googleapis.com/youtube/v3/commentThreads";
        private static string ApiKey = ConfigurationManager.AppSettings["ApiKey"];
        private static string UrlParameters = $"?part=snippet&textFormat=plainText&maxResults=100&key={ApiKey}o&videoId=";

        public static List<Item> GetItems(string url)
        {
            try
            {
                string videoId = url.Split('=')[1];
                string pageToken = "&pageToken=";
                string token = "";
                List<Item> items = new List<Item>();

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(YouTubeAPIUrl);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


                do
                {
                    // List data response.
                    HttpResponseMessage response = client.GetAsync(UrlParameters + videoId + pageToken + token).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body.
                        object dataObjects = response.Content.ReadAsAsync<object>().Result;

                        YoutubeCommentThread commentThread = JsonConvert.DeserializeObject<YoutubeCommentThread>(dataObjects.ToString());
                        Parallel.ForEach(commentThread.items, i => items.Add(i));

                        token = commentThread.nextPageToken;

                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    }

                } while (!string.IsNullOrWhiteSpace(token));
                ;
                client.Dispose();
                return items;


            }
            catch (Exception ex)
            {

                throw new SystemException("Error at " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ex.Message + " : " + ex.InnerException);
            }
        }

        public static string CreateCsv(List<Item> items, string url)
        {
            try
            {
                string fileName = url.Split('=')[1];
                DataTable dt = GetTable(items);
                string filePath = WriteToCsv(dt, fileName);
                return filePath;
            }
            catch (Exception ex)
            {

                throw new SystemException("Error at " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ex.Message + " : " + ex.InnerException);
            }
        }
        private static DataTable GetTable(List<Item> items)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn() { DataType = typeof(string), ColumnName = "Username", AllowDBNull = true });
                dt.Columns.Add(new DataColumn() { DataType = typeof(DateTime), ColumnName = "Date", AllowDBNull = true });
                dt.Columns.Add(new DataColumn() { DataType = typeof(int), ColumnName = "Star Rating", AllowDBNull = true });
                dt.Columns.Add(new DataColumn() { DataType = typeof(string), ColumnName = "Review or Comment", AllowDBNull = true });
                dt.Columns.Add(new DataColumn() { DataType = typeof(string), ColumnName = "Link", AllowDBNull = true });


                var loadTable = items.AsEnumerable().Select(i => dt.LoadDataRow(new object[]
                {
                    i.GetUserName(),
                    i.GetDate(),
                    i.GetLikeCount(),
                    i.GetComment(),
                    i.GetAuthorChannelLink()
                }, false)).ToList();

                return dt;
            }
            catch (Exception ex)
            {

                throw new SystemException("Error at " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ex.Message + " : " + ex.InnerException);
            }

        }

        private static string WriteToCsv(DataTable dt, string fileName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in dt.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field =>
                      string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                    sb.AppendLine(string.Join(",", fields));
                }

                string filePath = FilePath + fileName + ".csv";

                File.WriteAllText(filePath, sb.ToString());
                return filePath;
            }
            catch (Exception ex)
            {

                throw new SystemException("Error at " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ex.Message + " : " + ex.InnerException);
            }
        }
    }
}