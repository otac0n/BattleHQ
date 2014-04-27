using System;

namespace BattleHQ.Chess
{
    public class ColoredPiece : IEquatable<ColoredPiece>
    {
        public static ColoredPiece BlackBishop = new ColoredPiece(Piece.Bishop, Color.Black);
        public static ColoredPiece BlackKing = new ColoredPiece(Piece.King, Color.Black);
        public static ColoredPiece BlackKnight = new ColoredPiece(Piece.Knight, Color.Black);
        public static ColoredPiece BlackPawn = new ColoredPiece(Piece.Pawn, Color.Black);
        public static ColoredPiece BlackQueen = new ColoredPiece(Piece.Queen, Color.Black);
        public static ColoredPiece BlackRook = new ColoredPiece(Piece.Rook, Color.Black);
        public static ColoredPiece WhiteBishop = new ColoredPiece(Piece.Bishop, Color.White);
        public static ColoredPiece WhiteKing = new ColoredPiece(Piece.King, Color.White);
        public static ColoredPiece WhiteKnight = new ColoredPiece(Piece.Knight, Color.White);
        public static ColoredPiece WhitePawn = new ColoredPiece(Piece.Pawn, Color.White);
        public static ColoredPiece WhiteQueen = new ColoredPiece(Piece.Queen, Color.White);
        public static ColoredPiece WhiteRook = new ColoredPiece(Piece.Rook, Color.White);
        private readonly Color color;
        private readonly Piece piece;

        public ColoredPiece(Piece piece, Color color)
        {
            this.piece = piece;
            this.color = color;
        }

        public Color Color
        {
            get { return this.color; }
        }

        public Piece Piece
        {
            get { return this.piece; }
        }

        public static ColoredPiece Create(Color color, Piece piece)
        {
            if (color == Color.White)
            {
                switch (piece)
                {
                    case Piece.Pawn:
                        return WhitePawn;

                    case Piece.Knight:
                        return WhiteKnight;

                    case Piece.Bishop:
                        return WhiteBishop;

                    case Piece.Rook:
                        return WhiteRook;

                    case Piece.Queen:
                        return WhiteQueen;

                    case Piece.King:
                        return WhiteKing;

                    default:
                        throw new ArgumentOutOfRangeException("piece");
                }
            }
            else if (color == Color.Black)
            {
                switch (piece)
                {
                    case Piece.Pawn:
                        return BlackPawn;

                    case Piece.Knight:
                        return BlackKnight;

                    case Piece.Bishop:
                        return BlackBishop;

                    case Piece.Rook:
                        return BlackRook;

                    case Piece.Queen:
                        return BlackQueen;

                    case Piece.King:
                        return BlackKing;

                    default:
                        throw new ArgumentOutOfRangeException("piece");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("color");
            }
        }

        public static bool operator !=(ColoredPiece left, ColoredPiece right)
        {
            return !(left == right);
        }

        public static bool operator ==(ColoredPiece left, ColoredPiece right)
        {
            if (object.ReferenceEquals(left, null))
            {
                return object.ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as ColoredPiece);
        }

        public bool Equals(ColoredPiece other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            else if (object.ReferenceEquals(other, this))
            {
                return true;
            }

            return this.piece == other.piece && this.color == other.color;
        }

        public override int GetHashCode()
        {
            return ((int)this.piece) << 1 | (int)this.color;
        }

        public override string ToString()
        {
            return string.Concat(this.color, this.piece);
        }
    }
}
