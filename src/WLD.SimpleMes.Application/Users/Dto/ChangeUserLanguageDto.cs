using System.ComponentModel.DataAnnotations;

namespace WLD.SimpleMes.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
