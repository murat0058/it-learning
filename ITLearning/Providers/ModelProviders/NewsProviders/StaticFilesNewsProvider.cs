using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLearning.Frontend.Web.Model;
using System.IO;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using CommonMark;
using Newtonsoft.Json;
using Microsoft.AspNet.FileProviders;

namespace ITLearning.Frontend.Web.Providers.ModelProviders.NewsProviders
{
    public class StaticFilesNewsProvider : INewsProvider
    {
        private const string NEWS_PATH = "/static/news";
        private IHostingEnvironment _hostingEnvironment;

        public StaticFilesNewsProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IEnumerable<News> GetAll()
        {
            var newsList = new List<News>();
            newsList.AddRange(GetAllWithoutContent());

            var fileProvider = _hostingEnvironment.WebRootFileProvider;
            var directoryContents = fileProvider.GetDirectoryContents(NEWS_PATH).Where(x => x.Name.EndsWith("_content.md"));

            foreach (var news in newsList)
            {
                var contentFileForNews = directoryContents.Single(x => x.Name == $"{news.Id}_content.md");
                string htmlContent = GetHtmlContentFromMarkdownFile(contentFileForNews);

                news.Content = htmlContent;
            }

            return newsList;
        }

        public IEnumerable<News> GetAllWithoutContent()
        {
            var newsList = new List<News>();

            var fileProvider = _hostingEnvironment.WebRootFileProvider;
            var directoryContents = fileProvider.GetDirectoryContents(NEWS_PATH).Where(x => x.Name.EndsWith(".json"));

            foreach (var directoryContent in directoryContents)
            {
                newsList.Add(GetNewsFromJsonFile(directoryContent));
            }

            return newsList;
        }

        public News GetById(string id)
        {
            var fileProvider = _hostingEnvironment.WebRootFileProvider;

            var directoryContents = fileProvider.GetDirectoryContents(NEWS_PATH);

            var newsJsonInfoFile = directoryContents.SingleOrDefault(x => x.Name == $"{id}.json");
            var newsContentFile = directoryContents.SingleOrDefault(x => x.Name == $"{id}_content.md");

            if(newsJsonInfoFile == null || newsContentFile == null)
            {
                return null;
            }

            News news = GetNewsFromJsonFile(newsJsonInfoFile);
            news.Content = GetHtmlContentFromMarkdownFile(newsContentFile);

            return news;
        }

        private string GetHtmlContentFromMarkdownFile(IFileInfo contentFile)
        {
            var markdownContent = File.ReadAllText(contentFile.PhysicalPath);
            return CommonMarkConverter.Convert(markdownContent);
        }

        private News GetNewsFromJsonFile(IFileInfo contentFile)
        {
            var jsonNews = File.ReadAllText(contentFile.PhysicalPath);
            return JsonConvert.DeserializeObject<News>(jsonNews);
        }
    }
}
