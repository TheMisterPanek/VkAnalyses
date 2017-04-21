using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VK_Analyze.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Токен")]
        public string Token { get; set; }
    }

   

   
}
