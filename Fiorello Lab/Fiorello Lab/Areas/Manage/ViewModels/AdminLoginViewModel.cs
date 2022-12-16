using System.ComponentModel.DataAnnotations;

namespace Fiorello_Lab.Areas.Manage.ViewModels
{
    public class AdminLoginViewModel
    {
        [MaxLength(25)]
        public string Username { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}
