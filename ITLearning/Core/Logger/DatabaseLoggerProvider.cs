using Microsoft.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Logger
{
    public class DatabaseLoggerProvider : ILoggerProvider
    {
        private DatabaseLogger _logger = new DatabaseLogger();

        public ILogger CreateLogger(string name)
        {
            return _logger;
        }

        public void Dispose()
        {
        }
    }
}
