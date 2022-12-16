using System.ComponentModel.DataAnnotations;

namespace Fiorello_Lab.ViewModels
{
    public class RegisterViewModel
    {
        [MaxLength(25)]

        public string Fullname { get; set; }
        [MaxLength(25)]

        public string Username { get; set; }
        [DataType(DataType.Password)]
        [MinLength(5)]

        public string Password { get; set; }
        [Compare("Password", ErrorMessage ="Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
