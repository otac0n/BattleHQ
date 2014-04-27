using System.Collections.Generic;
using NUnit.Framework;

namespace BattleHQ.Chess
{
    [TestFixture]
    public class GameStateTests
    {
        public IEnumerable<TestCaseData> StartPosition
        {
            get
            {
                yield return new TestCaseData("a1", ColoredPiece.Create(Color.White, Piece.Rook));
                yield return new TestCaseData("b1", ColoredPiece.Create(Color.White, Piece.Knight));
                yield return new TestCaseData("c1", ColoredPiece.Create(Color.White, Piece.Bishop));
                yield return new TestCaseData("d1", ColoredPiece.Create(Color.White, Piece.Queen));
                yield return new TestCaseData("e1", ColoredPiece.Create(Color.White, Piece.King));
                yield return new TestCaseData("f1", ColoredPiece.Create(Color.White, Piece.Bishop));
                yield return new TestCaseData("g1", ColoredPiece.Create(Color.White, Piece.Knight));
                yield return new TestCaseData("h1", ColoredPiece.Create(Color.White, Piece.Rook));
                yield return new TestCaseData("a2", ColoredPiece.Create(Color.White, Piece.Pawn));
                yield return new TestCaseData("b2", ColoredPiece.Create(Color.White, Piece.Pawn));
                yield return new TestCaseData("c2", ColoredPiece.Create(Color.White, Piece.Pawn));
                yield return new TestCaseData("d2", ColoredPiece.Create(Color.White, Piece.Pawn));
                yield return new TestCaseData("e2", ColoredPiece.Create(Color.White, Piece.Pawn));
                yield return new TestCaseData("f2", ColoredPiece.Create(Color.White, Piece.Pawn));
                yield return new TestCaseData("g2", ColoredPiece.Create(Color.White, Piece.Pawn));
                yield return new TestCaseData("h2", ColoredPiece.Create(Color.White, Piece.Pawn));
                yield return new TestCaseData("a3", null);
                yield return new TestCaseData("b3", null);
                yield return new TestCaseData("c3", null);
                yield return new TestCaseData("d3", null);
                yield return new TestCaseData("e3", null);
                yield return new TestCaseData("f3", null);
                yield return new TestCaseData("g3", null);
                yield return new TestCaseData("h3", null);
                yield return new TestCaseData("a4", null);
                yield return new TestCaseData("b4", null);
                yield return new TestCaseData("c4", null);
                yield return new TestCaseData("d4", null);
                yield return new TestCaseData("e4", null);
                yield return new TestCaseData("f4", null);
                yield return new TestCaseData("g4", null);
                yield return new TestCaseData("h4", null);
                yield return new TestCaseData("a5", null);
                yield return new TestCaseData("b5", null);
                yield return new TestCaseData("c5", null);
                yield return new TestCaseData("d5", null);
                yield return new TestCaseData("e5", null);
                yield return new TestCaseData("f5", null);
                yield return new TestCaseData("g5", null);
                yield return new TestCaseData("h5", null);
                yield return new TestCaseData("a6", null);
                yield return new TestCaseData("b6", null);
                yield return new TestCaseData("c6", null);
                yield return new TestCaseData("d6", null);
                yield return new TestCaseData("e6", null);
                yield return new TestCaseData("f6", null);
                yield return new TestCaseData("g6", null);
                yield return new TestCaseData("h6", null);
                yield return new TestCaseData("a7", ColoredPiece.Create(Color.Black, Piece.Pawn));
                yield return new TestCaseData("b7", ColoredPiece.Create(Color.Black, Piece.Pawn));
                yield return new TestCaseData("c7", ColoredPiece.Create(Color.Black, Piece.Pawn));
                yield return new TestCaseData("d7", ColoredPiece.Create(Color.Black, Piece.Pawn));
                yield return new TestCaseData("e7", ColoredPiece.Create(Color.Black, Piece.Pawn));
                yield return new TestCaseData("f7", ColoredPiece.Create(Color.Black, Piece.Pawn));
                yield return new TestCaseData("g7", ColoredPiece.Create(Color.Black, Piece.Pawn));
                yield return new TestCaseData("h7", ColoredPiece.Create(Color.Black, Piece.Pawn));
                yield return new TestCaseData("a8", ColoredPiece.Create(Color.Black, Piece.Rook));
                yield return new TestCaseData("b8", ColoredPiece.Create(Color.Black, Piece.Knight));
                yield return new TestCaseData("c8", ColoredPiece.Create(Color.Black, Piece.Bishop));
                yield return new TestCaseData("d8", ColoredPiece.Create(Color.Black, Piece.Queen));
                yield return new TestCaseData("e8", ColoredPiece.Create(Color.Black, Piece.King));
                yield return new TestCaseData("f8", ColoredPiece.Create(Color.Black, Piece.Bishop));
                yield return new TestCaseData("g8", ColoredPiece.Create(Color.Black, Piece.Knight));
                yield return new TestCaseData("h8", ColoredPiece.Create(Color.Black, Piece.Rook));
            }
        }

        [Test]
        public void Create_WhenGivenDefaults_ReturnsStateOnFirstTurn()
        {
            var state = GameState.Create();
            Assert.That(state.Turn, Is.EqualTo(1));
        }

        [TestCase(Color.Black, Piece.King)]
        [TestCase(Color.Black, Piece.Queen)]
        [TestCase(Color.White, Piece.King)]
        [TestCase(Color.White, Piece.Queen)]
        public void Create_WhenGivenDefaults_ReturnsStateWithCastlingAvailable(Color color, Piece side)
        {
            var state = GameState.Create();
            Assert.That(state.CanCastle(color, side), Is.True);
        }

        [TestCaseSource("StartPosition")]
        public void Create_WhenGivenDefaults_ReturnsStateWithCorrectPiecePlacement(string coord, ColoredPiece piece)
        {
            var state = GameState.Create();
            Assert.That(state[coord], Is.EqualTo(piece));
        }

        [Test]
        public void Create_WhenGivenDefaults_ReturnsStateWithNoEnPassantFile()
        {
            var state = GameState.Create();
            Assert.That(state.EnPassantFile, Is.Null);
        }

        [Test]
        public void Create_WhenGivenDefaults_ReturnsStateWithWhiteToMove()
        {
            var state = GameState.Create();
            Assert.That(state.ActivePlayer, Is.EqualTo(Color.White));
        }

        [Test]
        public void Create_WhenGivenDefaults_ReturnsStateWithZeroOnTheFiftyMoveClock()
        {
            var state = GameState.Create();
            Assert.That(state.FiftyMoveClock, Is.EqualTo(0));
        }
    }
}
