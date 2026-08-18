using System.ComponentModel.DataAnnotations;

namespace SC.SimpleMes.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
