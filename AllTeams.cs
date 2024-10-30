using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace baseball
{
    static class AllTeams
    {
        private static Random _r = new Random();

        static List<Team> Teams = new List<Team>()
        {
            new Team(
                name: "Team A",
                players: new List<Player>()
                {
                    new Player("Chuck"),
                    new Player("Larry"),
                    new Player("Mort"),
                    new Player("Spud"),
                    new Player("Gort"),
                    new Player("Barry"),
                    new Player("Lenny"),
                    new Player("Batman"),
                    new Player("Clang"),
                }
            ),
            new Team(
                name: "Second team",
                new List<Player>()
                {
                    new Player("Will"),
                    new Player("Torple"),
                    new Player("Channing"),
                    new Player("Madison"),
                    new Player("Buck"),
                    new Player("Clem"),
                    new Player("Garfield"),
                    new Player("Sebastian"),
                    new Player("Steve"),
                }
            )
       };

        public static Team Random(Team notThisTeam = null)
        {
            Team team = null;

            while (team == notThisTeam)
            {
                team = Teams[_r.Next(Teams.Count - 1)];
            }

            return team;
        }
    }
}