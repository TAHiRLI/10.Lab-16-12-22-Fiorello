using System.ComponentModel.DataAnnotations;

namespace Fiorello_Lab.ViewModels
{
    public class adminLoginViewModel
    {
        [MaxLength(25)]
        public string Username { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}
