/*
 * SkipGuard.cs
 * 
 * This class represents a specialized type of Guard. It skips k shields upon block(x) so that the targeted shield x is offset by k.
 * 
 * Class Invariants:
 * 1. All integer values in the class must be non-negative.
 * 2. Supports Block functionality, with the specialization that it skips k shields upon block(x).
 * 3. Inherits from the Guard class.
 * 
 * Implementation Invariants:
 * 1. The 'shields' array holds the status of all the shields, non-negative integers.
 * 2. The 'skipNumber' holds the number of shields to skip.
 * 3. The 'Block' method is overridden from the base class to provide the functionality of skipping shields.
 */

using System;

namespace FighterProgram
{
    public class SkipGuard : Guard
    {
        private int skipNumber;

        // Precondition: All elements in 'shields' array and 'skipNumber' should be non-negative.
        // Postcondition: SkipGuard and its base Guard are initialized.
        public SkipGuard(int[] shields, int skipNumber) : base(shields)
        {
            this.skipNumber = skipNumber;
        }

        // Precondition: The input 'x' should be a valid index in shields array.
        // Postcondition: Blocks the shield at position '(x + skipNumber) % shields.Length' and returns the result of the 'Block' operation.
        public override bool Block(int x)
        {
            if (x < 0 || x >= shields.Length)
            {
                throw new ArgumentException("Invalid shield index.");
            }
            return base.Block((x + skipNumber) % shields.Length);
        }
    }
}
