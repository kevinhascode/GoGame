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
            
        }

        /* B: Would be suicide, but the merge makes a breath.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void Allow_MergeMakesBreath()
        {

        }

        /* C: Kills.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void Kill()
        {

        }

        /* D: Kos.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void Ko()
        {

        }

        /* F: Live normally.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void Live()
        {

        }

        /* G: Live normally on edge.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void LiveEdge()
        {

        }

        /* H: Live normally in corner.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void LiveCorner()
        {

        }
        
        /* I: Suicide.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void Disallow_Suicide()
        {

        }
        
        /* J: Makes groups.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void MakesGroups()
        {

        }
        
        /* K: Pass + Pass => ends game.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void PassPass_EndGame()
        {

        }

        /* L: Play-Pass-Play => two stones of same color.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void PassPlayPass_SameColor()
        {

        }

        /* M: Play w/ Merge that's a suicide.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void Disallow_MergeMakesSuicide()
        {

        }

        /* N: Not a suicide bc adds a new breath.
         
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         . . . . .
         
         */
        [Test]
        public void Allow_BreathPreventsSuicide()
        {
            
        }

        /* P: Conflict playing one stone on top of the other
      
         X = 1 & 2
         
         . . .
         . . .
         X . .
         
         */
        [Test]
        public void Disallow_StoneConflict()
        {
            game.Reset();
            game.ProposedPlay(new Loc(0, 0));
            RequestResponse response = game.ProposedPlay(new Loc(0, 0));
            Assert.IsNotNullOrEmpty(response.Reason);
        }
    }
}