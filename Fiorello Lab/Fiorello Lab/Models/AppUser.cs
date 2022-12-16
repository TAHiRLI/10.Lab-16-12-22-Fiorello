using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Fiorello_Lab.Models
{
    public class AppUser:IdentityUser
    {
        [MaxLength(25)]
        public string Fullname { get; set; }
    }
}
