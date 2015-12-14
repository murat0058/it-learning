using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.News
{
    public class SingleNewsViewModel
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Content { get; set; }

        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
