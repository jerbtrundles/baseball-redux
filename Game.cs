using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball
{
    class Game
    {
        private Random _r = new Random();
        private Team[] Teams;

        public int First { get; private set; }
        public int Second { get; private set; }
        public int Third { get; private set; }

        public SingleGameStats Stats { get; private set; }
        public InningStatus InningStatus { get; private set; }

        internal int HittingTeamIndex = 0;
        internal Team HittingTeam => Teams[HittingTeamIndex];
        internal int PitchingTeamIndex = 1;
        internal Team PitchingTeam => Teams[PitchingTeamIndex];

        private int CurrentInning => InningStatus.CurrentInning;
        public int CurrentBatterBalls => InningStatus.CurrentBatterBalls;
        public int Strikes { get; private set; }
        public int Outs { get; private set; }
        public int Inning { get; private set; }

        public string LastAction = string.Empty;

        private Game(Team team1, Team team2)
        {
            Teams = new Team[] { team1, team2 };
        }

        public static Game CreateWithRandomTeams()
        {
            Team t1 = AllTeams.Random();
            Team t2 = AllTeams.Random(t1);

            return new Game(t1, t2);
        }

        private void ChangeTeams()
        {
            HittingTeamIndex = (HittingTeamIndex + 1) % 2;
            PitchingTeamIndex = (PitchingTeamIndex + 1) % 2;
        }

        #region Process Plays

        enum Action
        {
            HOMERUN,
            TRIPLE,
            DOUBLE,
            SINGLE,
            OUT,
            STRIKE,
            BALL
        }
        private Action GetNextBatterAction()
        { 
            switch(_r.Next(200))
            {
                case > 198: return Action.HOMERUN;
                case > 194: return Action.TRIPLE;
                case > 190: return Action.DOUBLE;
                case > 175: return Action.SINGLE;
                case > 130: return Action.OUT;
                case > 50: return Action.STRIKE;
                default: return Action.BALL;
            }
        }
        private void ProcessAction(Action action)
        {
            switch (action)
            {
                case Action.HOMERUN:
                    ProcessHomerun(); break;
                case Action.TRIPLE:
                    ProcessTriple(); break;
                case Action.DOUBLE:
                    ProcessDouble(); break;
                case Action.SINGLE:
                    ProcessSingle(); break;
                case Action.OUT:
                    ProcessRandomOut(); break;
                case Action.STRIKE:
                    ProcessStrike(); break;
                case Action.BALL:
                    ProcessBall(); break;
                default:
                    break;
            }
        }
        private void ProcessHomerun()
        {
            Log.d("Homerun!");
            LastAction = "Homerun!";

            // add homerun stat
            // add hit stat
            
            int nScoredRuns = 1;

            if (First != 0) { nScoredRuns++; }
            if (Second != 0) { nScoredRuns++; }
            if (Third != 0) { nScoredRuns++; }

            ScoreRuns(nScoredRuns);
            ClearBases();
            NextBatter();
        }
        private void ProcessTriple()
        {
            Log.d("Triple!");
            LastAction = "Triple!";

            // add triple to hitting team's stats
            nTotalTriples++;

            if (nTeam == 0)
            {
                nTeam1Hits++;
            }
            else
            {
                nTeam2Hits++;
            }

            int nRuns = 0;

            if (Third != 0)
            {
                nRuns++;
                Third = 0;
            }

            if (Second != 0)
            {
                nRuns++;
                Second = 0;
            }

            if (First != 0)
            {
                nRuns++;
                First = 0;
            }

            if (nRuns > 0)
            {
                ScoreRuns(nRuns);
            }

            Third = 1;

            NextBatter();
        }
        private void ProcessDouble()
        {
            Log.d("Double!");
            LastAction = "Double!";
            nTotalDoubles++;

            if (nTeam == 0)
            {
                nTeam1Hits++;
            }
            else
            {
                nTeam2Hits++;
            }

            int nRuns = 0;

            if (nThird != 0)
            {
                nRuns++;
                nThird = 0;
            }

            if (nSecond != 0)
            {
                nRuns++;
                nSecond = 0;
            }

            if (nFirst != 0)
            {
                nThird = 1;
                nFirst = 0;
            }

            if (nRuns > 0)
            {
                ScoreRuns(nRuns);
            }

            nSecond = 1;

            NextBatter();
        }

        private void ProcessSingle()
        {
            Log.d("Single!");
            LastAction = "Single!";
            nTotalSingles++;

            if (nTeam == 0)
            {
                nTeam1Hits++;
            }
            else
            {
                nTeam2Hits++;
            }

            int nRuns = 0;

            if (nThird != 0)
            {
                nRuns++;
                nThird = 0;
            }

            if (nSecond != 0)
            {
                nThird = 1;
                nSecond = 0;
            }

            if (nFirst != 0)
            {
                nSecond = 1;
                nFirst = 0;
            }

            if (nRuns > 0)
            {
                ScoreRuns(nRuns);
            }

            nFirst = 1;

            NextBatter();
        }

        private void ProcessRandomOut()
        {
            switch (_r.Next(3))
            {
                case 0:
                    // add stat for hitting team
                    // add stat for game

                    Log.d("Pop out!");
                    nTotalPopOuts++;
                    LastAction = "Pop out!";
                    break;
                case 1:
                    Log.d("Fly out!");
                    nTotalFlyOuts++;
                    LastAction = "Fly out!";
                    break;
                case 2:
                    Log.d("Line out!");
                    nTotalLineOuts++;
                    LastAction = "Line out!";
                    break;
            }

            nOuts++;
            if (nOuts == 3)
            {
                NextTeam();
            }
            else
            {
                NextBatter();
            }
        }
        private void ProcessStrikeout()
        {
            Log.d("Strikeout!");
            LastAction = "Strikeout!";
            nTotalStrikeouts++;

            nOuts++;
            if (nOuts == 3)
            {
                NextTeam();
            }
            else
            {
                NextBatter();
            }
        }
        private void ProcessStrike()
        {
            nStrikes++;
            nTotalStrikes++;

            Log.d("Strike " + nStrikes.ToString() + "!");

            if (nStrikes == 3)
            {
                ProcessStrikeout();
            }
        }
        private void ProcessBall()
        {
            nBalls++;
            nTotalBalls++;

            Log.d("Ball " + nBalls.ToString() + ".");

            if (nBalls == 4)
            {
                ProcessWalk();
            }
        }
        private void ProcessWalk()
        {
            Log.d("Walk!");
            LastAction = "Walk!";
            nTotalWalks++;

            if (nFirst != 0)
            {
                if (nSecond != 0)
                {
                    if (nThird != 0)
                    {
                        ScoreRuns(1);
                    }

                    nThird = 1;
                }

                nSecond = 1;
            }

            nFirst = 1;
            NextBatter();
        }
        #endregion

        public bool Finished()
        {
            if (Inning >= 8 && HittingTeamIndex == 1 && HittingTeam.Score > PitchingTeam.Score)
            {
                return true;
            }
            else if (Inning >= 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void MainLoop(Game game, bool bDoAction = true)
        {
            DrawLog();
            DrawTotalStats(62, 3);
            if (bDoAction) { RandomAction(); }
            GameStatus();
            DrawField(60, Console.WindowHeight - 8);
            DrawScoreboard();
        }

        private static void GameStatus()
        {
            Console.SetCursorPosition(0, 0);

            if (LastAction != string.Empty)
            {
                Console.WriteLine(strClearString);
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Console.WriteLine("Previous play: " + strLastAction);
            }

            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("Team 1: " + nTeam1Score.ToString());

            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("Team 2: " + nTeam2Score.ToString());

            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            if (nInning == 9)
            {
                Console.WriteLine("Inning: bottom 9");
            }
            else
            {
                Console.WriteLine("Inning: " + ((nTeam == 0) ? "top " : "bottom ") + (nInning + 1).ToString());
            }

            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("Batting: Team " + (nTeam + 1).ToString());
            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("Count: " + nBalls.ToString() + "-" + nStrikes.ToString());
            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("Outs: " + nOuts.ToString());

            Console.WriteLine();
        }

        private static void DrawHistory()
        {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Team 1 record: " + nTeam1Wins.ToString() + "-" + nTeam2Wins.ToString() + (nTies > 0 ? "-" + nTies.ToString() : string.Empty));
            Console.WriteLine("Team 2 record: " + nTeam2Wins.ToString() + "-" + nTeam1Wins.ToString() + (nTies > 0 ? "-" + nTies.ToString() : string.Empty));
        }

        private static void DrawAveragesShell()
        {
            Console.SetCursorPosition(30, 0);
            Console.WriteLine("|-------------------------|");
            Console.SetCursorPosition(30, 1);
            Console.WriteLine("| Game Stats              |");
            Console.SetCursorPosition(30, 2);
            Console.WriteLine("|-------------------------|");
            for (int i = 0; i < 12; i++)
            {
                Console.SetCursorPosition(30, i + 3);
                Console.WriteLine("|                         |");
            }
            Console.SetCursorPosition(30, 15);
            Console.WriteLine("|-------------------------|");
        }

        private static void DrawAverages()
        {
            Console.SetCursorPosition(32, 3);
            Console.WriteLine("Average singles: " + nAverageSingles.ToString());
            Console.SetCursorPosition(32, 4);
            Console.WriteLine("Average doubles: " + nAverageDoubles.ToString());
            Console.SetCursorPosition(32, 5);
            Console.WriteLine("Average triples: " + nAverageTriples.ToString());
            Console.SetCursorPosition(32, 6);
            Console.WriteLine("Average homeruns: " + nAverageHomeruns.ToString());
            Console.SetCursorPosition(32, 7);
            Console.WriteLine("Average balls: " + nAverageBalls.ToString());
            Console.SetCursorPosition(32, 8);
            Console.WriteLine("Average walks: " + nAverageWalks.ToString());
            Console.SetCursorPosition(32, 9);
            Console.WriteLine("Average strikes: " + nAverageStrikes.ToString());
            Console.SetCursorPosition(32, 10);
            Console.WriteLine("Average strikeouts: " + nAverageStrikeouts.ToString());
            Console.SetCursorPosition(32, 11);
            Console.WriteLine("Average fly outs: " + nAverageFlyOuts.ToString());
            Console.SetCursorPosition(32, 12);
            Console.WriteLine("Average line outs: " + nAverageLineOuts.ToString());
            Console.SetCursorPosition(32, 13);
            Console.WriteLine("Average pop outs: " + nAveragePopOuts.ToString());
            Console.SetCursorPosition(32, 14);
            Console.WriteLine("Average runs: " + nAverageRuns.ToString());
        }

        private static void DrawTotalStatsShell()
        {
            Console.SetCursorPosition(60, 0);
            Console.WriteLine("|-------------------------|");
            Console.SetCursorPosition(60, 1);
            Console.WriteLine("| Game Stats              |");
            Console.SetCursorPosition(60, 2);
            Console.WriteLine("|-------------------------|");
            for (int i = 0; i < 12; i++)
            {
                Console.SetCursorPosition(60, i + 3);
                Console.WriteLine("|                         |");
            }
            Console.SetCursorPosition(60, 15);
            Console.WriteLine("|-------------------------|");
        }
        private static void DrawTotalStats(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("Total singles: " + nTotalSingles.ToString());
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("Total doubles: " + nTotalDoubles.ToString());
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("Total triples: " + nTotalTriples.ToString());
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("Total homeruns: " + nTotalHomeruns.ToString());
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("Total balls: " + nTotalBalls.ToString());
            Console.SetCursorPosition(x, y + 5);
            Console.WriteLine("Total strikes: " + nTotalStrikes.ToString());
            Console.SetCursorPosition(x, y + 6);
            Console.WriteLine("Total walks: " + nTotalWalks.ToString());
            Console.SetCursorPosition(x, y + 7);
            Console.WriteLine("Total strikeouts: " + nTotalStrikeouts.ToString());
            Console.SetCursorPosition(x, y + 8);
            Console.WriteLine("Total fly outs: " + TotalFlyOuts.ToString());
            Console.SetCursorPosition(x, y + 9);
            Console.WriteLine("Total line outs: " + nTotalLineOuts.ToString());
            Console.SetCursorPosition(x, y + 10);
            Console.WriteLine("Total pop outs: " + nTotalPopOuts.ToString());
            Console.SetCursorPosition(x, y + 11);
            Console.WriteLine("Total runs: " + nTotalRuns.ToString());
        }

        #region Log
        private static void DrawLogShell()
        {
            Console.SetCursorPosition(90, 0);
            Console.WriteLine("|-------------------------|");
            Console.SetCursorPosition(90, 1);
            Console.WriteLine("| Game Log                |");
            Console.SetCursorPosition(90, 2);
            Console.WriteLine("|-------------------------|");
            for (int i = 0; i < nLogSize; i++)
            {
                Console.SetCursorPosition(90, i + 3);
                Console.WriteLine("|                         |");
            }
            Console.SetCursorPosition(90, 3 + nLogSize);
            Console.WriteLine("|-------------------------|");
        }
        private static void DrawLog()
        {
            for (int i = 0; i < GameLog.Count; i++)
            {
                Console.SetCursorPosition(92, i + 3);
                Console.WriteLine(new string(' ', 20));
                Console.SetCursorPosition(92, i + 3);
                Console.WriteLine(GameLog[i]);
            }

            Console.WriteLine();
        }
        #endregion

        #region Miscellaneous
        private static void NewGame()
        {
            Console.Clear();

            DrawTotalStatsShell();
            DrawLogShell();
            DrawAveragesShell();

            DrawHistory();
            DrawAverages();

            nLogIndex = 1;
            GameLog = new List<string>(nLogSize);

            nTotalSingles = 0;
            nTotalDoubles = 0;
            nTotalTriples = 0;
            nTotalHomeruns = 0;
            nTotalBalls = 0;
            nTotalWalks = 0;
            nTotalStrikeouts = 0;
            nTotalStrikes = 0;
            nTotalFlyOuts = 0;
            nTotalLineOuts = 0;
            nTotalPopOuts = 0;
            nTotalRuns = 0;
        }
        private static void EndOfGame()
        {
            if (nTeam1Score > nTeam2Score)
            {
                nTeam1Wins++;
            }
            else if (nTeam2Score > nTeam1Score)
            {
                nTeam2Wins++;
            }
            else
            {
                nTies++;
            }

            dAverageSingles = (dAverageSingles * (nTotalGames - 1) + nTotalSingles) / nTotalGames;
            dAverageDoubles = (dAverageDoubles * (nTotalGames - 1) + nTotalDoubles) / nTotalGames;
            dAverageTriples = (dAverageTriples * (nTotalGames - 1) + nTotalTriples) / nTotalGames;
            dAverageHomeruns = (dAverageHomeruns * (nTotalGames - 1) + nTotalHomeruns) / nTotalGames;
            dAverageBalls = (dAverageBalls * (nTotalGames - 1) + nTotalBalls) / nTotalGames;
            dAverageWalks = (dAverageWalks * (nTotalGames - 1) + nTotalWalks) / nTotalGames;
            dAverageStrikes = (dAverageStrikes * (nTotalGames - 1) + nTotalStrikes) / nTotalGames;
            dAverageStrikeouts = (dAverageStrikeouts * (nTotalGames - 1) + nTotalStrikeouts) / nTotalGames;
            dAverageFlyOuts = (dAverageFlyOuts * (nTotalGames - 1) + nTotalFlyOuts) / nTotalGames;
            dAverageLineOuts = (dAverageLineOuts * (nTotalGames - 1) + nTotalLineOuts) / nTotalGames;
            dAveragePopOuts = (dAveragePopOuts * (nTotalGames - 1) + nTotalPopOuts) / nTotalGames;
            dAverageRuns = (dAverageRuns * (nTotalGames - 1) + nTotalRuns) / nTotalGames;

            DrawHistory();
            DrawAverages();

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Starting new game in 5 seconds.");
            System.Threading.Thread.Sleep(5000);
        }

        private static void ScoreRuns(int nRuns)
        {
            nTotalRuns += nRuns;

            Log(nRuns.ToString() + (nRuns > 1 ? " runs scored." : " run scored."));

            if (nTeam == 0)
            {
                nTeam1Scores[nInning] += nRuns;
            }
            else
            {
                nTeam2Scores[nInning] += nRuns;
            }
        }
        private static void ClearBases()
        {
            Log("Clearing bases.");


            nFirst = 0;
            nSecond = 0;
            nThird = 0;
        }
        private void NextBatter()
        {
            Log.d("Next batter.");

            Balls = 0;
            Strikes = 0;
        }
        private void NextTeam()
        {
            Log.d("Changing teams.");
            inningStatus.NextTeam();

            First = 0;
            Second = 0;
            Third = 0;
            Strikes = 0;
            Balls = 0;
            Outs = 0;

            nTeam = (nTeam + 1) % 2;
            if (nTeam == 0)
            {
                nInning++;
            }
        }
        #endregion
    }
}
