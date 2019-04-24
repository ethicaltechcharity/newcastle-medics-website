using System;

namespace Web.Models
{
    public class FixtureViewModel
    {
        public DateTime Pushback{ get; set; }

        public string CompetitionName { get; set; }
        public string StageName { get; set; }

        public string VenueName { get; set; }

        public string ClubOneName { get; set; }
        public string ClubTwoName { get; set; }

        public string TeamOneName { get; set; }
        public string TeamTwoName { get; set; }

        public string MatchStatus { get; set; }


        public int TeamOneRegularGoals { get; set; }
        public int TeamTwoRegularGoals { get; set; }

        public int TeamOnePenaltyShootoutGoals { get; set; }
        public int TeamTwoPenaltyShootoutGoals { get; set; }

        public string Outcome { get; set; }
    }
}