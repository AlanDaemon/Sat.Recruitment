using Sat.Recruitment.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.API.Models.Users
{
    public class AddUserCommandModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required.")]
        public string Email { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required.")]
        public string Address { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required.")]
        public string Phone { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required.")]
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}
