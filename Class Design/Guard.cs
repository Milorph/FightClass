using System;

namespace FighterProgram
{
    /*
     * Guard.cs
     * 
     * Class Invariants:
     * 1. Shields array length should be positive.
     * 2. The initial mode of guard is up (isUpMode = true).
     * 3. If the guard is not alive, it cannot block.
     * 4. Used constructor injection to get its shields array.
     * 
     * Implementation Invariants:
     * 1. The 'shields' array holds the status of all the shields, non-negative integers.
     * 2. IsAlive checks if more than half of shields are greater than 0, return true, else false
     * 3. ChangeMode changes the boolean of isUpMode to its opposite boolean value
     * 4. Block uses the integer input as the shield index to use
     * 
     */

    public class Guard : IGuard
    {
        protected int[] shields;
        protected bool isUpMode;

        // Precondition: Shields array should be not null and should have positive length.
        // Postcondition: Shields array and mode (isUpMode) are set.
        public Guard(int[] shields)
        {
            // Asserting preconditions
            if (shields == null || shields.Length <= 0)
                throw new ArgumentException("Shields array cannot be null or empty.");

            this.shields = shields;
            isUpMode = true;
        }

        // Precondition: None
        // Postcondition: Returns true if more than half of the shields are viable (value > 0), false otherwise.
        private bool IsAlive()
        {
            int viableShields = 0;
            foreach (int shield in shields)
            {
                if (shield > 0) viableShields++;
            }

            return viableShields >= shields.Length / 2;
        }

        // Precondition: None
        // Postcondition: Changes the mode of the guard.
        private void ChangeMode()
        {
            isUpMode = !isUpMode;
        }

        /*
         * Precondition: The guard should be alive and the input (x) should be a valid index in shields array.
         * Postcondition: If the guard is in up mode and the shield at index x is viable, decreases the shield value by 1 and returns true.
         *                If the guard is not in up mode, sets the shield at index x to 0 and returns true.
         *                If the guard is not alive, returns false.
         */
        public virtual bool Block(int x)
        {
            if (IsAlive())
            {
                ChangeMode();
                if (x < 0 || x >= shields.Length)
                {
                    throw new ArgumentException("Invalid shield index.");
                }

                if (isUpMode && shields[x] > 0)
                {
                    shields[x]--;
                    return true;
                }
                else
                {
                    shields[x] = 0;
                    return true;
                }
            }
            return false;
        }
    }

    /*
     * Implementation Invariants:
     * 1. The mode of the guard changes every time a Block action is performed.
     * 2. If the guard is in up mode, the Block action will decrease the value of shield at index x (if the shield is viable).
     * 3. If the guard is not in up mode, the Block action will set the shield at index x to 0.
     * 4. If the guard is not alive, it cannot perform Block action.
     */
}
