using System;
using System.Collections.Generic;

namespace BattleHQ.Chess
{
    public class GameState
    {
        private readonly Color activePlayer;
        private readonly bool canBlackCastleKingside;
        private readonly bool canBlackCastleQueenside;
        private readonly bool canWhiteCastleKingside;
        private readonly bool canWhiteCastleQueenside;
        private readonly int? enPassantFile;
        private readonly int fiftyMoveClock;
        private readonly ColoredPiece[,] pieces;
        private readonly GameState previousState;
        private readonly int turn;

        private GameState(ColoredPiece[,] pieces, Color activePlayer, bool canBlackCastleKingside, bool canBlackCastleQueenside, bool canWhiteCastleKingside, bool canWhiteCastleQueenside, int? enPassantFile, int fiftyMoveClock, int turn, GameState previousState)
        {
            this.pieces = pieces;
            this.activePlayer = activePlayer;
            this.canBlackCastleKingside = canBlackCastleKingside;
            this.canBlackCastleQueenside = canBlackCastleQueenside;
            this.canWhiteCastleKingside = canWhiteCastleKingside;
            this.canWhiteCastleQueenside = canWhiteCastleQueenside;
            this.enPassantFile = enPassantFile;
            this.fiftyMoveClock = fiftyMoveClock;
            this.turn = turn;
            this.previousState = previousState;
        }

        public Color ActivePlayer
        {
            get { return this.activePlayer; }
        }

        public int? EnPassantFile
        {
            get { return this.enPassantFile; }
        }

        public int FiftyMoveClock
        {
            get { return this.fiftyMoveClock; }
        }

        public int Turn
        {
            get { return this.turn; }
        }

        public ColoredPiece this[string coord]
        {
            get
            {
                int file, rank;
                try
                {
                    ToFileRank(coord, out file, out rank);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException("Invalid coordinate '" + coord + "'.", ex);
                }

                return this[file, rank];
            }
        }

        public ColoredPiece this[int file, int rank]
        {
            get
            {
                if (file < 0 || file >= 8)
                {
                    throw new ArgumentOutOfRangeException("file");
                }
                else if (rank < 0 || rank >= 8)
                {
                    throw new ArgumentOutOfRangeException("rank");
                }

                return this.pieces[file, rank];
            }
        }

        public static GameState Create()
        {
            return GameState.FromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        }

        public static GameState FromFen(string fen)
        {
            var fields = fen.Split(' ');
            if (fields.Length != 6)
            {
                throw new FormatException();
            }

            var ranks = fields[0].Split('/');
            if (ranks.Length != 8)
            {
                throw new FormatException();
            }

            var pieces = new ColoredPiece[8, 8];
            for (var rank = 0; rank < 8; rank++)
            {
                var file = 0;
                foreach (var c in ranks[rank])
                {
                    if (file >= 8)
                    {
                        throw new FormatException();
                    }

                    switch (c)
                    {
                        case '1': file += 1; break;
                        case '2': file += 2; break;
                        case '3': file += 3; break;
                        case '4': file += 4; break;
                        case '5': file += 5; break;
                        case '6': file += 6; break;
                        case '7': file += 7; break;
                        case '8': file += 8; break;
                        case 'P': pieces[file, rank] = ColoredPiece.WhitePawn; file += 1; break;
                        case 'N': pieces[file, rank] = ColoredPiece.WhiteKnight; file += 1; break;
                        case 'B': pieces[file, rank] = ColoredPiece.WhiteBishop; file += 1; break;
                        case 'R': pieces[file, rank] = ColoredPiece.WhiteRook; file += 1; break;
                        case 'Q': pieces[file, rank] = ColoredPiece.WhiteQueen; file += 1; break;
                        case 'K': pieces[file, rank] = ColoredPiece.WhiteKing; file += 1; break;
                        case 'p': pieces[file, rank] = ColoredPiece.BlackPawn; file += 1; break;
                        case 'n': pieces[file, rank] = ColoredPiece.BlackKnight; file += 1; break;
                        case 'b': pieces[file, rank] = ColoredPiece.BlackBishop; file += 1; break;
                        case 'r': pieces[file, rank] = ColoredPiece.BlackRook; file += 1; break;
                        case 'q': pieces[file, rank] = ColoredPiece.BlackQueen; file += 1; break;
                        case 'k': pieces[file, rank] = ColoredPiece.BlackKing; file += 1; break;
                        default: throw new FormatException();
                    }
                }

                if (file != 8)
                {
                    throw new FormatException();
                }
            }

            Color activePlayer;
            switch (fields[1])
            {
                case "b": activePlayer = Color.Black; break;
                case "w": activePlayer = Color.White; break;
                default: throw new FormatException();
            }

            bool canBlackCastleKingside = false;
            bool canBlackCastleQueenside = false;
            bool canWhiteCastleKingside = false;
            bool canWhiteCastleQueenside = false;
            var available = new List<char> { 'K', 'Q', 'k', 'q', '-' };
            for (int i = 0; i < fields[2].Length; i++)
            {
                var c = fields[2][i];
                var index = available.IndexOf(c);
                if (index == -1)
                {
                    throw new FormatException();
                }

                available.RemoveRange(0, index + 1);

                switch (c)
                {
                    case 'K': canWhiteCastleKingside = true; break;
                    case 'Q': canWhiteCastleQueenside = true; break;
                    case 'k': canBlackCastleKingside = true; break;
                    case 'q': canBlackCastleQueenside = true; break;
                    case '-':
                        if (canWhiteCastleKingside || canWhiteCastleQueenside || canBlackCastleKingside || canBlackCastleQueenside)
                        {
                            throw new FormatException();
                        }

                        break;
                }
            }

            int? enPassantFile;
            if (fields[3] == "-")
            {
                enPassantFile = null;
            }
            else
            {
                int file, rank;
                ToFileRank(fields[3], out file, out rank);

                var white = rank == 5;
                var black = rank == 2;
                if (!(white ^ black))
                {
                    throw new FormatException();
                }

                var color = (white ? Color.White : Color.Black);
                if (color == activePlayer)
                {
                    throw new FormatException();
                }
                else if (pieces[file, rank] != null || pieces[file, rank + (white ? -1 : 1)] != null)
                {
                    throw new FormatException();
                }
                else if (pieces[file, rank - (white ? -1 : 1)] != ColoredPiece.Create(color, Piece.Pawn))
                {
                    throw new FormatException();
                }

                enPassantFile = file;
            }

            var fiftyMoveClock = int.Parse(fields[4]);
            var turn = int.Parse(fields[5]);

            if (fiftyMoveClock < 0 || turn < 1 || fiftyMoveClock.ToString() != fields[4] || turn.ToString() != fields[5])
            {
                throw new FormatException();
            }

            return new GameState(pieces, activePlayer, canBlackCastleKingside, canBlackCastleQueenside, canWhiteCastleKingside, canWhiteCastleQueenside, enPassantFile, fiftyMoveClock, turn, null);
        }

        public bool CanCastle(Color color, Piece side)
        {
            switch (color)
            {
                case Color.White:
                    switch (side)
                    {
                        case Piece.Queen:
                            return this.canWhiteCastleQueenside;

                        case Piece.King:
                            return this.canWhiteCastleKingside;

                        default:
                            throw new ArgumentOutOfRangeException("side");
                    }

                case Color.Black:
                    switch (side)
                    {
                        case Piece.Queen:
                            return this.canBlackCastleQueenside;

                        case Piece.King:
                            return this.canBlackCastleKingside;

                        default:
                            throw new ArgumentOutOfRangeException("side");
                    }

                default:
                    throw new ArgumentOutOfRangeException("color");
            }
        }

        private static void ToFileRank(string coord, out int file, out int rank)
        {
            if (coord == null)
            {
                throw new ArgumentNullException("coord");
            }
            else if (coord.Length != 2)
            {
                throw new FormatException();
            }

            file = (coord[0] - 'a');
            rank = 8 - (coord[1] - '1') - 1;
        }
    }
}
