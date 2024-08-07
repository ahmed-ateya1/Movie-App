using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Enumerator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "{0} Can't be Blank")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "{0} Can't be Blank")]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "UniqueEmail",controller:"Account",ErrorMessage ="Account Already used")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "{0} Can't be Blank")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "{0} Can't be Blank")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber {  get; set; }
        [Required(ErrorMessage = "{0} Can't be Blank")]
        [DataType(DataType.Password)]
        public string? Password {  get; set; }
        [Required(ErrorMessage = "{0} Can't be Blank")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public UserOption userOption { get; set; } = UserOption.USER;
    }
}
