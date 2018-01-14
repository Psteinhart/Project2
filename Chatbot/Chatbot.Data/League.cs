using System;
using System.Collections.Generic;

namespace Chatbot.Data
{
    public partial class League
    {
        public League()
        {
            UserInterest = new HashSet<UserInterest>();
        }

        public int LeagueId { get; set; }
        public string LeagueName { get; set; }

        public ICollection<UserInterest> UserInterest { get; set; }
    }
}
