using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chatbot.Client.Models
{
    public  class User
    {

        [StringLength(20)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }
        [StringLength(20)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }

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
