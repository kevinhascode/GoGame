using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GoGame
{
    [TestFixture]
    internal class UnitTests
    {
        private GoGame game = new GoGame { BoardSize = 3, Handicap = 0 };

        // Odd #s are Xs, Evens are Os

        /* A: Would be suicide, but it's a kill... and it's not a Ko bc 2nd move kills many.
         
         . . . . .
         . . . . .
         O O O . .
         1 X O . .
         O X O . .
         
         */
        [Test]
        public void Allow_Kill()
        {
            Assert.Equals(0, 1);
        }

        /* B: Would be suicide, but the merge makes a breath.
         
         . . . .
         . . . .
         O O O .
         X 1 X .
         
         */
        [Test]
        public void Allow_MergeMakesBreath()
        {
            Assert.Equals(0, 1);
        }

        /* C: Kill.
         
         . . .
         X . .
         O 1 .
         
         */
        [Test]
        public void Kill()
        {
            Assert.Equals(0, 1);
        }

        /* D: Ko.
         
         . . . .       . . . .
         . . . .  -->  . . . .
         X O . .       X O . .
         O . O .       2 1 O .
         
         */
        [Test]
        public void Ko()
        {
            Assert.Equals(0, 1);
        }

        /* F: Live normally.
         
         . . .
         . 1 .
         . . .
         
         */
        [Test]
        public void Live()
        {
            Assert.Equals(0, 1);
        }

        /* G: Live normally on edge.
         
         . . .
         1 . .
         . . .
         
         */
        [Test]
        public void LiveEdge()
        {
            Assert.Equals(0, 1);
        }

        /* H: Live normally in corner.
         
         . . .
         . . .
         1 . .
         
         */
        [Test]
        public void LiveCorner()
        {
            Assert.Equals(0, 1);
        }

        /* I: Suicide.
         
         . . .
         O . .
         1 O .
         
         */
        [Test]
        public void Disallow_Suicide()
        {
            Assert.Equals(0, 1);
        }

        /* J: Makes groups.
         
         . . . .
         . X O .
         X O . .
         X O . .
         
         */
        [Test]
        public void MakesGroups()
        {
            // Test explicitly for number of groups and each groups stones and breaths.
            Assert.Equals(0, 1);
        }

        /* K: Pass + Pass => ends game. 
         */
        [Test]
        public void PassPass_EndGame()
        {
            Assert.Equals(0, 1);
        }

        /* L: Play-Pass-Play => two stones of same color.
         
         . . .
         . . .
         1 3 .   2 = PASS
         
         */
        [Test]
        public void PassPlayPass_SameColor()
        {
            Assert.Equals(0, 1);
        }

        /* M: Merge that's a suicide.
         
         . . . . .
         . . . . .
         . . . . .
         O O O . .
         X 1 X O .
         
         */
        [Test]
        public void Disallow_MergeMakesSuicide()
        {
            Assert.Equals(0, 1);
        }

        /* N: Not a suicide bc adds a new breath.
         
         . . . . .
         . . . . .
         . . . . .
         O . O . .
         X 1 X O .
         
         */
        [Test]
        public void Allow_BreathPreventsSuicide()
        {
            Assert.Equals(0, 1);
        }

        /* P: Conflict playing one stone on top of the other
      
         X = 1 & 2
         
         . .
         X .
         
         */
        [Test]
        public void Disallow_StoneConflict()
        {
            game.Reset();
            game.ProposedPlay(new Loc(0, 0));
            RequestResponse response = game.ProposedPlay(new Loc(0, 0));
            Assert.IsNotNullOrEmpty(response.Reason);
        }

        /* R: Doesn't double-count liberties
      
         . . .
         X X .
         X . .
         
         */
        [Test]
        public void LibertyCount()
        {
            Assert.Equals(0, 1);
        }
    }
}