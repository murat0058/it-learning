using ITLearning.Frontend.Web.DAL;
using Microsoft.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Logger
{
    public class DatabaseLogger : ILogger
    {
        public IDisposable BeginScopeImpl(object state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                ctx.ErrorLogs.Add(new DAL.Model.ErrorLogs
                {
                    Date = DateTime.Now,
                    ErrorMessage = exception.Message,
                    ErrorSource = DAL.Model.ErrorSource.PlatformApp
                });

                ctx.SaveChanges();
            }
        }
    }
}
