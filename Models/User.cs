using System.Linq;
using StokTakipProjesi2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StokTakipProjesi2.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Display(Name = "Username"), Required(ErrorMessage = "Please enter correct value")]
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool isRemembered { get; set; } = false;

        public static bool ValidateUser(string username, string password)
        {
            return Accounts.Users.Any(u => u.Username == username && u.Password == password);
        }
    }
}
