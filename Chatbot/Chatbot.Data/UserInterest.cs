using System;
using System.Collections.Generic;

namespace Chatbot.Data
{
    public partial class UserInterest
    {
        public int InterestId { get; set; }
        public int UserId { get; set; }
        public int? LeagueId { get; set; }
        public int? Nflid { get; set; }
        public string FavAthlete { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public League League { get; set; }
        public Nflteams Nfl { get; set; }
        public UserInfo User { get; set; }
    }
}
