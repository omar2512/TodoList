using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Infrastrucutre.Dto
{
    public class RegisterDto
    {
      
            [Required]
            [StringLength(15, MinimumLength = 2, ErrorMessage = "first name at least 2 character and max length 15")]
            public string? FirstName { get; set; }
            [Required]
            [StringLength(15, MinimumLength = 2, ErrorMessage = "second name at least 2 character and max length 15")]
            public string? LastName { get; set; }
            [Required]
            [RegularExpression(pattern: "^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "email is invalid expression")]
            public string? Email { get; set; }
            [Required]
            [StringLength(15, MinimumLength = 5, ErrorMessage = "password at least 5 character and max length 15")]
            public string? Password { get; set; }

        
    }
}
