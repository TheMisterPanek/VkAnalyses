using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace VK_Analyze.Models
{ 
        public class UserFriendsView
        {
            [Required(ErrorMessage = "идентификатор не может быть пустым")]
            public int UserID { get; set; }
        }

}