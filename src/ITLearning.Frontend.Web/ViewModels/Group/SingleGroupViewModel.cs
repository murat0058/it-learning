using System;
using ITLearning.Contract.Enums;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
	public class SingleGroupViewModel
	{
		public GroupAccessTypeEnum AccessType { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public int NoOfUsers { get; set; }
    }
}