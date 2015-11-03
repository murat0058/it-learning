using ITLearning.Frontend.Web.Contract.Enums;
using System;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public class ErrorLog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public ErrorSource ErrorSource { get; set; }
        public string ErrorMessage { get; set; }
    }
}