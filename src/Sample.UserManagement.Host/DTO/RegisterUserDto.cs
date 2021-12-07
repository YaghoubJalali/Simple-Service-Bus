using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.UserManagement.DTO
{
    public class RegisterUserDto
    {
        [DisplayName("First Name")]
        [Required(AllowEmptyStrings =false)]
        public string FirstName { get; set; }
        
        [DisplayName("Last Name")]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
    }
}
