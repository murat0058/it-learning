using Microsoft.AspNet.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Common
{
    public class StaticFilesContentTypeProvider : FileExtensionContentTypeProvider
    {
        public StaticFilesContentTypeProvider()
        {
            Mappings.Add(".md", "text/x-markdown");
        }
    }
}
