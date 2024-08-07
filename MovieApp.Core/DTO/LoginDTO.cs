using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "{0} can't be Blank")]
        public string? UserName {  get; set; }
        [Required(ErrorMessage = "{0} can't be Blank")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
