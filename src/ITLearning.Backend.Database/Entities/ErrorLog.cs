using ITLearning.Contract.Enums;
using System;

namespace ITLearning.Backend.Database.Entities
{
    public class ErrorLog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public ErrorSourceEnum ErrorSource { get; set; }
        public string ErrorMessage { get; set; }
    }
}