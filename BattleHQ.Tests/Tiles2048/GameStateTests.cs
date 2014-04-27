using NUnit.Framework;

namespace BattleHQ.Tiles2048
{
    [TestFixture]
    public class GameStateTests
    {
        [Test]
        [Combinatorial]
        public void Create_WhenGivenDefaults_ReturnsStateWithNoTiles([Values(0, 1, 2, 3)] int x, [Values(0, 1, 2, 3)] int y)
        {
            var state = GameState.Create();
            Assert.That(state[x, y], Is.EqualTo(0));
        }

        [Test]
        public void Create_WhenGivenDefaults_ReturnsStateWithPlacerToMove()
        {
            var state = GameState.Create();
            Assert.That(state.ActivePlayer, Is.EqualTo(Player.Placer));
        }
    }
}
