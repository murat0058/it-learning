using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLearning.Frontend.Web.Contract.Data.Model.News;
using System.IO;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using CommonMark;
using Newtonsoft.Json;
using Microsoft.AspNet.FileProviders;
using ITLearning.Frontend.Web.Contract.Configs;
using Microsoft.Framework.OptionsModel;
using ITLearning.Frontend.Web.Contract.Providers.ModelProviders;

namespace ITLearning.Frontend.Web.Providers.ModelProviders
{
    public class StaticFilesNewsProvider : INewsProvider
    {
        private IHostingEnvironment _hostingEnvironment;
        private IOptions<PathsConfiguration> _pathsConfiguration;

        private string _newsPath;
        private string _newsImagesPath;

        public StaticFilesNewsProvider(IHostingEnvironment hostingEnvironment, IOptions<PathsConfiguration> pathsConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _pathsConfiguration = pathsConfiguration;

            _newsPath = _pathsConfiguration.Value.News;
            _newsImagesPath = _pathsConfiguration.Value.NewsImages;
        }

        public IEnumerable<NewsData> GetAll()
        {
            var newsList = new List<NewsData>();
            newsList.AddRange(GetAllWithoutContent());

            var fileProvider = _hostingEnvironment.WebRootFileProvider;
            var directoryContents = fileProvider.GetDirectoryContents(_newsPath).Where(x => x.Name.EndsWith("_content.md"));

            foreach (var news in newsList)
            {
                var contentFileForNews = directoryContents.Single(x => x.Name == $"{news.Id}_content.md");
                string htmlContent = GetHtmlContentFromMarkdownFile(contentFileForNews);

                news.Content = htmlContent;
            }

            return newsList;
        }

        public IEnumerable<NewsData> GetAllWithoutContent()
        {
            var newsList = new List<NewsData>();

            var fileProvider = _hostingEnvironment.WebRootFileProvider;
            var directoryContents = fileProvider.GetDirectoryContents(_newsPath).Where(x => x.Name.EndsWith(".json"));

            foreach (var directoryContent in directoryContents)
            {
                newsList.Add(GetNewsFromJsonFile(directoryContent));
            }

            return newsList;
        }

        public NewsData GetById(string id)
        {
            var fileProvider = _hostingEnvironment.WebRootFileProvider;

            var directoryContents = fileProvider.GetDirectoryContents(_newsPath);

            var newsJsonInfoFile = directoryContents.SingleOrDefault(x => x.Name == $"{id}.json");
            var newsContentFile = directoryContents.SingleOrDefault(x => x.Name == $"{id}_content.md");

            if(newsJsonInfoFile == null || newsContentFile == null)
            {
                return null;
            }

            NewsData news = GetNewsFromJsonFile(newsJsonInfoFile);
            news.Content = GetHtmlContentFromMarkdownFile(newsContentFile);

            return news;
        }

        private string GetHtmlContentFromMarkdownFile(IFileInfo contentFile)
        {
            var markdownContent = File.ReadAllText(contentFile.PhysicalPath);
            return CommonMarkConverter.Convert(markdownContent);
        }

        private NewsData GetNewsFromJsonFile(IFileInfo contentFile)
        {
            var jsonNews = File.ReadAllText(contentFile.PhysicalPath);
            var news = JsonConvert.DeserializeObject<NewsData>(jsonNews);
            news.ImagePath = _newsImagesPath + news.ImagePath;

            return news;
        }
    }
}
