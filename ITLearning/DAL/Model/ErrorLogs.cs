using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public enum ErrorSource
    {
        SourceControlApp = 0,
        PlatformApp = 1
    }

    public class ErrorLogs
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public ErrorSource ErrorSource { get; set; }
        public string ErrorMessage { get; set; }
    }
}
