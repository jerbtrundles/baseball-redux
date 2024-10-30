using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball {
    class Team(string name, List<Player> players)
    {
        public string Name { get; private set; } = name;
        public int Wins { get; private set; } = 0;
        public int Losses { get; private set; } = 0;
        public int Ties { get; private set; } = 0;

        public List<Player> Players { get; private set; } = players;

        private int _currentPlayerIndex = 0;
        public Player CurrentPlayer { get => Players[_currentPlayerIndex]; }
                public void NextPlayer() {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % Players.Count;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Team team) return false;

            return Name == team.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
