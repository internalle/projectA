using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QRoller
{
    public class Crawler
    {
        public T Crawl<T>(string url, Dictionary<string, string> mapper) where T : new()
        {
            var client = new WebClient();
            var html = client.DownloadString(url);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var instance = new T();

            foreach (var entry in mapper)
            {
                var value = htmlDoc.QuerySelector(entry.Key).InnerText;
                typeof(T).GetProperty(entry.Value).SetValue(instance, value);
            }

            return instance;
        }
    }
}
