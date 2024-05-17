using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Identity_Full.ViewModels
{
    public class RegisterVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]

        public string RepeatPassword { get; set; }
    }
}
