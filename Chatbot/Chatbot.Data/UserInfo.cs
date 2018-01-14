using System;
using System.Collections.Generic;

namespace Chatbot.Data
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            UserInterest = new HashSet<UserInterest>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? Active { get; set; }

        public ICollection<UserInterest> UserInterest { get; set; }
    }
}
