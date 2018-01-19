using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chatbot.Client.Models
{
    public class Login
    {

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email Address Required")]
        public string Email { get; set; }

        [StringLength(50)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required Password")]
        public string Password { get; set; }
    }
}
