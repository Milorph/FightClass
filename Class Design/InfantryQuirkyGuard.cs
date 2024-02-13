using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FighterProgram
{
    /*
     * InfantryQuirkyGuard.cs
     * 
     * Class Invariants:
     * 1. Position (row, column), strength, attackRange, and values in shields and artilleryProvider should be non-negative.
     * 2. The initial mode of InfantryQuirkyGuard is up (isUpMode = true).
     * 3. If the InfantryQuirkyGuard is not alive, it cannot block.
     * 
     * Implementation Invariants"
     * 1. Block overrides base Block and will select any other shield other than the one inputted
     * 2. IsAlive checks if more than half of shields are greater than 0, return true, else false
     * 3. ChangeMode changes the boolean of isUpMode to its opposite boolean value
     */

    public class InfantryQuirkyGuard : Infantry, IGuard
    {
        private int lastUsedShieldIndex;
        private int[] shields;
        private bool isUpMode;

        // Precondition: All parameters should be non-negative.
        // Postcondition: InfantryQuirkyGuard and its base Infantry are initialized.
        public InfantryQuirkyGuard(int row, int column, int strength, int attackRange, int[] artilleryProvider, int[] shields)
            : base(row, column, strength, attackRange, artilleryProvider)
        {
            if (row < 0 || column < 0 || strength < 0 || attackRange < 0 || shields.Any(s => s < 0) || artilleryProvider.Any(a => a < 0))
            {
                throw new ArgumentException("No values should be negative!");
            }
            this.shields = shields;
            this.isUpMode = true;
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

            return viableShields >= shields.Length / 2.0;
        }

        // Precondition: None
        // Postcondition: Changes the mode of the InfantryQuirkyGuard.
        private void ChangeMode()
        {
            isUpMode = !isUpMode;
        }

        // Precondition: The InfantryQuirkyGuard should be alive and the input (x) should be a valid index in shields array.
        // Postcondition: If the InfantryQuirkyGuard is in up mode and the shield at index x is viable, decreases the shield value by 1 and returns true.
        //                If the InfantryQuirkyGuard is not in up mode, sets the shield at index x to 0 and returns true.
        //                If the InfantryQuirkyGuard is not alive, returns false.
        private bool BaseBlock(int x)
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

        /*
         * Precondition: Input (x) should be a valid index in shields array.
         * Postcondition: The block method is called on the next shield index, skipping the input (x), and the result is returned.
         */
        public bool Block(int x)
        {
            if (x < 0 || x > shields.Length)
            {
                throw new ArgumentException("Invalid shield index.");
            }
            // Find next index, skipping x
            do
            {
                lastUsedShieldIndex = (lastUsedShieldIndex + 1) % shields.Length;
            }
            while (lastUsedShieldIndex == x);
            return BaseBlock(lastUsedShieldIndex);
        }

        /*
         * Precondition: InfantryQuirkyGuard should be alive and inputs (x, y) should be valid targets. Also, shieldIndex should be a valid index in shields array.
         * Postcondition: If the attack on the target is successful and the block action is successful, returns true. Otherwise, returns false.
         */
        public bool AttackAndBlock(int x, int y, int q, int shieldIndex)
        {
            // Use Infantry's Target method
            bool attackResult = Target(x, y, q);

            // If the attack was successful, use QuirkyGuard's Block method
            if (attackResult)
            {
                bool res = Block(shieldIndex);
                return res;
            }

            return false;
        }
    }
}
