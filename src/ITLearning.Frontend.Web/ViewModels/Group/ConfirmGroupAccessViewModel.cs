using ITLearning.Contract.Enums;
using System.ComponentModel.DataAnnotations;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
	public class ConfirmGroupAccessViewModel
	{
		public GroupAccessTypeEnum AccessType { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Has³o")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Has³o jest wymagane.")]
        public string Password { get; set; }
	}
}