using QMand.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QMand.Commands.Definition;
using QRoller;

namespace ProjectA.Console.Commands
{
    public class YoutubeCrawler : ConsoleCommand
    {
        public override string Description => "Crawl youtube video title and description";

        public override string Name => "tube";

        public override List<CommandParameter> ParametersDefinition => new List<CommandParameter>
        {
            new CommandParameter { Name = "url", Description = "Url of the video to be crawled", Type = ParameterType.Required }
        };

        public override void Run()
        {
            var url = GetParametar("url");

            var crawler = new Crawler();
            var mapper = new Dictionary<string, string>
            {
                { ".watch-title", "Name" },
                { "#watch-description-text", "Description" },
            };

            var meta = crawler.Crawl<YoutubeMeta>(url, mapper);
            meta.Url = url;

            System.Console.WriteLine(meta.Name);
        }
    }


    public class YoutubeMeta{

        public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }
    }
}
