using System;

namespace BattleHQ.Tiles2048
{
    public class GameState
    {
        private readonly Player activePlayer;
        private readonly int[,] tiles;

        public GameState(int[,] tiles, Player activePlayer)
        {
            this.tiles = tiles;
            this.activePlayer = activePlayer;
        }

        public Player ActivePlayer
        {
            get { return this.activePlayer; }
        }

        public int this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= 4)
                {
                    throw new ArgumentOutOfRangeException("x");
                }
                else if (y < 0 || y >= 4)
                {
                    throw new ArgumentOutOfRangeException("y");
                }

                return this.tiles[x, y];
            }
        }

        public static GameState Create()
        {
            var tiles = new int[4, 4];
            var activePlayer = Player.Placer;
            return new GameState(tiles, activePlayer);
        }
    }
}
