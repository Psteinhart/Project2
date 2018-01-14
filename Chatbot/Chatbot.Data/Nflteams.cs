using System;
using System.Collections.Generic;

namespace Chatbot.Data
{
    public partial class Nflteams
    {
        public Nflteams()
        {
            UserInterest = new HashSet<UserInterest>();
        }

        public int Nflid { get; set; }
        public string TeamName { get; set; }
        public string Conference { get; set; }
        public string City { get; set; }

        public ICollection<UserInterest> UserInterest { get; set; }
    }
}
