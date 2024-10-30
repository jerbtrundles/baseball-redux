using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball
{
    internal static class Stats
    {
        internal static List<SingleGameStats> GameStats = new List<SingleGameStats>();
        internal static int TotalGames => GameStats.Count;
        internal static int TotalSingles => GameStats.Sum(gameStats => gameStats.TotalSingles);
        internal static double AverageSingles => GameStats.Average(gameStats => gameStats.TotalSingles);
        internal static int TotalDoubles => GameStats.Sum(gameStats => gameStats.TotalDoubles);
        internal static double AverageDoubles => GameStats.Average(gameStats => gameStats.TotalDoubles);
        internal static int TotalTriples => GameStats.Sum(gameStats => gameStats.TotalTriples);
        internal static double AverageTriples => GameStats.Average(gameStats => gameStats.TotalTriples);
        internal static int TotalHomeruns => GameStats.Sum(gameStats => gameStats.TotalHomeruns);
        internal static double AverageHomeruns => GameStats.Average(gameStats => gameStats.TotalHomeruns);
        internal static int TotalBalls => GameStats.Sum(gameStats => gameStats.Balls);
        internal static double AverageBalls => GameStats.Average(gameStats => gameStats.Balls);
        internal static int TotalStrikes => GameStats.Sum(gameStats => gameStats.Strikes);
        internal static double AverageStrikes => GameStats.Average(gameStats => gameStats.Strikes);
        internal static int TotalStrikeouts => GameStats.Sum(gameStats => gameStats.Strikeouts);
        internal static double AverageStrikeouts => GameStats.Average(gameStats => gameStats.Strikeouts);
        internal static int TotalWalks => GameStats.Sum(gameStats => gameStats.Walks);
        internal static double AverageWalks => GameStats.Average(gameStats => gameStats.Walks);
        internal static int TotalLineouts => GameStats.Sum(gameStats => gameStats.TotalLineOuts);
        internal static double AverageLineouts => GameStats.Average(gameStats => gameStats.TotalLineOuts);
        internal static int TotalPopouts => GameStats.Sum(gameStats => gameStats.TotalPopouts);
        internal static double AveragePopouts => GameStats.Average(gameStats => gameStats.TotalPopouts);
        internal static int TotalFlyouts => GameStats.Sum(gameStats => gameStats.TotalFlyouts);
        internal static double AverageFlyouts => GameStats.Average(gameStats => gameStats.TotalFlyouts);
        internal static int TotalGroundouts => GameStats.Sum(gameStats => gameStats.TotalGroundouts);
        internal static double AverageGroundouts => GameStats.Average(gameStats => gameStats.TotalGroundouts);
        internal static int TotalOuts => GameStats.Sum(gameStats => gameStats.TotalOuts);
        internal static double AverageOuts => GameStats.Average(gameStats => gameStats.TotalOuts);
        internal static int TotalRuns => GameStats.Sum(gameStats => gameStats.TotalRuns);
        internal static double AverageRuns => GameStats.Average(gameStats => gameStats.TotalRuns);
    }
}
