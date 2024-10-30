using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball
{
    class TeamSingleGameStats
    {
        internal int[] Runs = new int[9];
        internal int TotalRuns => Runs.Sum();

        public Team Team { get; private set; }
        public int Singles { get; set; } = 0;
        public int Doubles { get; set; } = 0;
        public int Triples { get; set; } = 0;
        public int Homeruns { get; set; } = 0;
        public int Balls { get; set; } = 0;
        public int Walks { get; set; } = 0;
        public int Strikes { get; set; } = 0; 
        public int Strikeouts { get; set; } = 0;        
        public int FlyOuts { get; set; } = 0;
        public int LineOuts { get; set; } = 0;
        public int PopOuts { get; set; } = 0;
        public int GroundOuts { get; set; } = 0;
        public int TotalHits => Singles + Doubles + Triples + Homeruns;
        public int TotalOuts => FlyOuts + LineOuts + PopOuts + GroundOuts;

        public TeamSingleGameStats(Team team)
        {
            Team = team;
        }
    }
}
