using System.Linq;
using NUnit.Framework;

namespace GoGame
{
    [TestFixture]
    internal class UnitTests
    {
        // X is Black (and Odd #s), O is White (and Even #s).

        private GoGame game = new GoGame { BoardSize = 3, Handicap = 0 };

        /* Conflict playing one stone on top of the other
      
         X = 1 & 2
         
         . .
         X .
         
         */
        [Test]
        public void Disallow_StoneConflict()
        {
            game = new GoGame { BoardSize = 2 };

            game.ProposedPlay(new Loc(0, 0));

            Assert.True(game.ProposedPlay(new Loc(0, 0)).Reason == ReasonEnum.Conflict);

            // TEST: It's still White's turn.
            Assert.True(game.ProposedPlay(new Loc(1, 1)).Move.StonePlaced.IsWhite);
        }

        /* Live normally.
         
         . . .
         . 1 .
         . . .
         
         */
        [Test]
        public void Live()
        {
            const short n = 3;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(1, 1));

            Assert.True(game.blackChains.Count == 1);
            Assert.True(game.blackChains[0].Stones.Count == 1);
            Assert.True(game.blackChains[0].Liberties.Count == 4);
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(0, 1)));
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(1, 0)));
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(2, 1)));
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(1, 2)));
        }

        /* Live normally on edge.
         
         . . .
         1 . .
         . . .
         
         */
        [Test]
        public void LiveEdge()
        {
            const short n = 3;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(0, 1));

            Assert.True(game.blackChains.Count == 1);
            Assert.True(game.blackChains[0].Stones.Count == 1);
            Assert.True(game.blackChains[0].Liberties.Count == 3);
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(0, 0)));
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(1, 1)));
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(0, 2)));
        }

        /* Live normally in corner.
         
         . .
         1 .
         
         */
        [Test]
        public void LiveCorner()
        {
            const short n = 2;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(0, 0));

            Assert.True(game.blackChains.Count == 1);
            Assert.True(game.blackChains[0].Stones.Count == 1);
            Assert.True(game.blackChains[0].Liberties.Count == 2);
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(1, 0)));
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(0, 1)));
        }

        /* Live normally in other corner.
         
         . 1
         . .
         
         */
        [Test]
        public void LiveOtherCorner()
        {
            const short n = 2;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(1, 1));

            Assert.True(game.blackChains.Count == 1);
            Assert.True(game.blackChains[0].Stones.Count == 1);
            Assert.True(game.blackChains[0].Liberties.Count == 2);
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(1, 0)));
            Assert.True(game.blackChains[0].Liberties.Contains(new Loc(0, 1)));
        }

        /* Makes groups.
         
         . . . .
         . X O .
         X O . .
         X O . .
         
         */
        [Test]
        public void MakesGroups()
        {
            const short n = 4;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(0, 0)); // X
            game.ProposedPlay(new Loc(1, 0)); // O
            game.ProposedPlay(new Loc(0, 1)); // X
            game.ProposedPlay(new Loc(1, 1)); // O
            game.ProposedPlay(new Loc(1, 2)); // X
            game.ProposedPlay(new Loc(2, 2)); // O

            // TEST: explicitly check number of groups and each groups stones and breaths.
            Assert.True(game.blackChains.Count == 2);
            Assert.True(game.blackChains.Where(chain => chain.Stones.Count == 1).First().Liberties.Count == 2); // the one with 1 stone has 2 libs
            Assert.True(game.blackChains.Where(chain => chain.Stones.Count == 2).First().Liberties.Count == 1); // the one with 2 stones has 1 lib
            Assert.True(game.whiteChains.Count == 2);
            Assert.True(game.whiteChains.Where(chain => chain.Stones.Count == 1).First().Liberties.Count == 3); // the one with 1 stone has 3 libs
            Assert.True(game.whiteChains.Where(chain => chain.Stones.Count == 2).First().Liberties.Count == 2); // the one with 2 stones has 2 libs
        }

        /* Doesn't double-count liberties
      
         . . .
         X X .
         X . .
         
         */
        [Test]
        public void LibertyCount()
        {
            const short n = 3;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(0, 0)); // X
            game.ProposedPlay(new Loc(n, n)); // O: PASS
            game.ProposedPlay(new Loc(0, 1)); // X
            game.ProposedPlay(new Loc(n, n)); // O: PASS
            game.ProposedPlay(new Loc(1, 1)); // X

            // TEST: explicitly check number of groups and each groups stones and breaths.
            Assert.True(game.blackChains.Count == 1);
            Assert.True(game.blackChains.First().Liberties.Count == 4);
        }

        /* Kill.
         
         . . .
         X . .
         O 1 .
         
         */
        [Test]
        public void Kill()
        {
            const short n = 3;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(0, 1)); // X
            game.ProposedPlay(new Loc(0, 0)); // O
            game.ProposedPlay(new Loc(1, 0)); // 1

            Assert.True(game.blackChains.Count == 2);
            Assert.True(game.whiteChains.Count == 0);
            Assert.True(game.blackChains.First().Liberties.Count == 3);
            Assert.True(game.PrisonersTakenByBlack == 1);
        }

        /* Would be suicide, but it's a kill... and it's not a Ko bc 2nd move kills many.
         
            . . . .
            O O . .
            1 X O .
            O X O .
         
            */
        [Test]
        public void Allow_Kill()
        {
            const short n = 4;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(1, 0)); // X
            game.ProposedPlay(new Loc(0, 2)); // O
            game.ProposedPlay(new Loc(1, 1)); // X
            game.ProposedPlay(new Loc(1, 2)); // O
            game.ProposedPlay(new Loc(n, n)); // X: PASS
            game.ProposedPlay(new Loc(2, 1)); // O
            game.ProposedPlay(new Loc(n, n)); // X: PASS
            game.ProposedPlay(new Loc(2, 0)); // O
            game.ProposedPlay(new Loc(n, n)); // X: PASS
            game.ProposedPlay(new Loc(0, 0)); // O
            game.ProposedPlay(new Loc(0, 1)); // 1

            Assert.True(game.blackChains.Count == 1);
            Assert.True(game.whiteChains.Count == 2);
            Assert.True(game.PrisonersTakenByBlack == 1);
            Assert.True(game.blackChains.First().Liberties.Count == 1);
            Assert.True(game.blackChains.First().Liberties[0].Equals(new Loc(0, 0)));
        }

        /* Would be suicide, but the merge makes a breath.
         
         . . . .
         . . . .
         O O O .
         X 1 X .
         
         */
        [Test]
        public void Allow_MergeMakesBreath()
        {
            const short n = 4;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(0, 0)); // X
            game.ProposedPlay(new Loc(1, 1)); // O
            game.ProposedPlay(new Loc(2, 0)); // X
            game.ProposedPlay(new Loc(2, 1)); // O
            game.ProposedPlay(new Loc(n, n)); // X: PASS
            game.ProposedPlay(new Loc(0, 1)); // O
            game.ProposedPlay(new Loc(1, 0)); // 1

            Assert.True(game.blackChains.Count == 1);
            Assert.True(game.whiteChains.Count == 1);
            Assert.True(game.blackChains.First().Liberties.Count == 1);
            Assert.True(game.blackChains.First().Liberties[0].Equals(new Loc(3, 0)));
        }

        /* Ko.
         
         . . . .       . . . .
         . . . .  -->  . . . .
         X O . .       X O . .
         O 1 O .       2 X O .
         
         */
        [Test]
        public void Ko()
        {
            const short n = 4;
            game = new GoGame { BoardSize = n };

            /* First Step */
            game.ProposedPlay(new Loc(0, 1)); // X
            game.ProposedPlay(new Loc(0, 0)); // O
            game.ProposedPlay(new Loc(n, n)); // X: PASS
            game.ProposedPlay(new Loc(1, 1)); // O
            game.ProposedPlay(new Loc(n, n)); // X: PASS
            game.ProposedPlay(new Loc(2, 0)); // O
            RequestResponse response = game.ProposedPlay(new Loc(1, 0)); // X: 1

            Assert.True(response.Move.ChainsKilled.Count == 1);             // only one group
            Assert.True(response.Move.ChainsKilled[0].Stones.Count == 1);   // of only one stone is killed
            Assert.True(game.PossibleKoLoc.Equals(new Loc(0, 0)));

            /* Second Step */
            response = game.ProposedPlay(new Loc(0, 0)); // O: 2

            Assert.True(game.blackChains.Count == 2);
            Assert.True(game.whiteChains.Count == 2);
            Assert.True(response.Reason == ReasonEnum.Fine);
            Assert.True(response.Reason == ReasonEnum.Ko);

            /* Play Elsewhere */
            response = game.ProposedPlay(new Loc(2, 2)); // O: 2, again.

            Assert.True(response.Reason == ReasonEnum.Fine);
            Assert.True(response.Move.StonePlaced.IsWhite);
        }

        /* Suicide.
         
         . . .
         O . .
         1 O .
         
         */
        [Test]
        public void Disallow_Suicide()
        {
            const short n = 3;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(n, n)); // X: PASS
            game.ProposedPlay(new Loc(0, 1)); // O
            game.ProposedPlay(new Loc(n, n)); // X: PASS
            game.ProposedPlay(new Loc(1, 0)); // O
            RequestResponse response = game.ProposedPlay(new Loc(0, 0)); // 1

            Assert.True(game.blackChains.Count == 0);
            Assert.True(game.whiteChains.Count == 2);
            Assert.True(response.Reason == ReasonEnum.Suicide);

            // TEST: It's still black's turn
            response = game.ProposedPlay(new Loc(2, 2)); // X
            Assert.True(!response.Move.StonePlaced.IsWhite);

            // TEST: White can still play there.
            response = game.ProposedPlay(new Loc(0, 0)); // O
            Assert.True(response.Reason == ReasonEnum.Fine);
        }

        /* Merge that's a suicide.
         
         . . .
         O O O
         X 1 X
         
         */
        [Test]
        public void Disallow_MergeMakesSuicide()
        {
            const short n = 3;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(0, 0)); // X
            game.ProposedPlay(new Loc(0, 1)); // O
            game.ProposedPlay(new Loc(2, 0)); // X
            game.ProposedPlay(new Loc(2, 1)); // O
            game.ProposedPlay(new Loc(n, n)); // X: PASS
            game.ProposedPlay(new Loc(1, 1)); // O
            game.ProposedPlay(new Loc(1, 0)); // 1

            Assert.True(game.blackChains.Count == 2);
            Assert.True(game.whiteChains.Count == 1);
            Assert.True(game.blackChains.Count(chain => chain.Liberties.First().Equals(new Loc(1, 0))) == 2); // 2 black chains with same Liberty

            // TEST: Still Black's turn.
            Assert.True(!game.ProposedPlay(new Loc(1, 2)).Move.StonePlaced.IsWhite); // X
        }

        /* Not a suicide bc adds a new breath.
         
         . . .
         O . O
         X 1 X
         
         */
        [Test]
        public void Allow_BreathPreventsSuicide()
        {
            const short n = 3;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(0, 0)); // X
            game.ProposedPlay(new Loc(0, 1)); // O
            game.ProposedPlay(new Loc(2, 0)); // X
            game.ProposedPlay(new Loc(2, 1)); // O
            game.ProposedPlay(new Loc(1, 0)); // 1

            Assert.True(game.blackChains.Count == 1);
            Assert.True(game.blackChains.First().Liberties.Count == 1);
            Assert.True(game.blackChains.First().Liberties.First().Equals(new Loc(1, 1)));
        }

        /* Pass + Pass => ends game. 
         */
        [Test]
        public void PassPass_EndGame()
        {
            const short n = 2;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(n, n));
            game.ProposedPlay(new Loc(n, n));

            Assert.True(game.IsOver);
        }

        /* Play-Pass-Play => two stones of same color.
         
         . . .
         . . .
         1 3 .   2 = PASS
         
         */
        [Test]
        public void PassPlayPass_SameColor()
        {
            const short n = 3;
            game = new GoGame { BoardSize = n };

            game.ProposedPlay(new Loc(0, 0)); // 1
            game.ProposedPlay(new Loc(n, n)); // 2: PASS
            RequestResponse response = game.ProposedPlay(new Loc(1, 0)); // 3

            Assert.True(!response.Move.StonePlaced.IsWhite);
        }
    }
}