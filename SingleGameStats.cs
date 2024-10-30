using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball {
    internal class SingleGameStats {
        internal int Inning = 0;
        internal InningStatus InningStatus;
        internal string LastAction { get; set; }

        internal TeamSingleGameStats Team1Stats { get; set; }
        internal TeamSingleGameStats Team2Stats { get; set; }

        internal int TotalSingles => Team1Stats.Singles + Team2Stats.Singles;
        internal int TotalDoubles => Team1Stats.Doubles + Team2Stats.Doubles;
        internal int TotalTriples => Team1Stats.Triples + Team2Stats.Triples;
        internal int TotalHomeruns => Team1Stats.Homeruns + Team2Stats.Homeruns;
        internal int TotalHits => Team1Stats.TotalHits + Team2Stats.TotalHits;
        internal int TotalPopouts => Team1Stats.PopOuts + Team2Stats.PopOuts;
        internal int TotalFlyouts => Team1Stats.FlyOuts + Team2Stats.FlyOuts;
        internal int TotalLineOuts => Team1Stats.LineOuts + Team2Stats.LineOuts;
        internal int TotalGroundouts => Team1Stats.GroundOuts + Team2Stats.GroundOuts;
        internal int TotalOuts => Team1Stats.TotalOuts + Team2Stats.TotalOuts;
        internal int Balls => Team1Stats.Balls + Team2Stats.Balls;
        internal int Walks => Team1Stats.Walks + Team2Stats.Walks;
        internal int Strikes => Team1Stats.Strikes + Team2Stats.Strikes;
        internal int Strikeouts => Team1Stats.Strikeouts + Team2Stats.Strikeouts;
        internal int TotalRuns => Team1Stats.TotalRuns + Team2Stats.TotalRuns;

        internal SingleGameStats(Team team1, Team team2)
        {
            Team1Stats = new TeamSingleGameStats(team1);
            Team2Stats = new TeamSingleGameStats(team2);
        }
    }
}
