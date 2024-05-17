using System.ComponentModel.DataAnnotations;

namespace CRUD_Identity_Full.ViewModels
{
    public class LoginVM
    {
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
