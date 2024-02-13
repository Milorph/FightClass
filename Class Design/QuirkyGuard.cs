/*
 * QuirkyGuard.cs
 * 
 * This class represents a specialized type of Guard. It arbitrarily selects shields, ignoring the 'x' in block(x).
 * 
 * Class Invariants:
 * 1. Position (row, column), strength, attackRange, skipNumber, and values in shields and artilleryProvider should be non-negative.
 * 2. If the QuirkyGuard is not alive, it cannot block.
 * 
 * Implementation Invariants:
 * 1. The 'shields' array holds the status of all the shields, non-negative integers.
 * 2. The 'lastUsedShieldIndex' holds the index of the last used shield.
 * 3. The 'Block' method is overridden from the base class to provide the functionality of arbitrarily selecting shields.
 */

using System;

namespace FighterProgram
{
    public class QuirkyGuard : Guard
    {
        private int lastUsedShieldIndex;

        // Precondition: All elements in 'shields' array should be non-negative.
        // Postcondition: QuirkyGuard and its base Guard are initialized.
        public QuirkyGuard(int[] shields) : base(shields)
        {
        }

        // Precondition: The input 'x' should be a valid index in shields array.
        // Postcondition: Blocks the next shield that is not 'x', and returns the result of the 'Block' operation.
        public override bool Block(int x)
        {
            if (x < 0 || x >= shields.Length)
            {
                throw new ArgumentException("Invalid shield index.");
            }
            // Find next index, skipping x
            do
            {
                lastUsedShieldIndex = (lastUsedShieldIndex + 1) % shields.Length;
            }
            while (lastUsedShieldIndex == x);

            return base.Block(lastUsedShieldIndex);
        }
    }
}
