using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Contract.Data.Model.News
{
    public class NewsData
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Content { get; set; }
    }
}
