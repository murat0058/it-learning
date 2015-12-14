using Microsoft.AspNet.Http;
using System.ComponentModel.DataAnnotations;

namespace ITLearning.Frontend.Web.ViewModels.News
{
    public class CreateUpdateNewsViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MaxLength(100, ErrorMessage = "Tytuł jest zbyt długi (max. 100 znaków).")]
        public string Title { get; set; }

        [Display(Name = "Treść")]
        [Required(ErrorMessage = "Treść jest wymagana")]
        [MinLength(3, ErrorMessage = "Treść jest zbyt krótka (min. 3 znaki).")]
        [MaxLength(2000, ErrorMessage = "Treść jest zbyt długa (max. 2000 znaków).")]
        public string Content { get; set; }

        [Display(Name = "Tagi")]
        [Required(ErrorMessage = "Dodaj przynajmniej jeden tag")]
        public string TagsString { get; set; }

        [Display(Name = "Zdjęcie")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
