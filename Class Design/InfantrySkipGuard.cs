using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FighterProgram
{
    /*
     * InfantrySkipGuard.cs
     * 
     * Class Invariants:
     * 1. Position (row, column), strength, attackRange, skipNumber, and values in shields and artilleryProvider should be non-negative.
     * 2. If the InfantrySkipGuard is not alive, it cannot block.
     * 
     * Implementation Invariants:
     * 1. The 'shields' array holds the status of all the shields, non-negative integers.
     * 2. The 'isUpMode' boolean flag determines the mode of the InfantrySkipGuard.
     * 3. The 'skipNumber' determines the number of shields to skip when performing a block operation.
     */

    public class InfantrySkipGuard : Infantry, IGuard
    {
        private int[] shields;
        private int skipNumber;
        private bool isUpMode;

        // Precondition: All parameters should be non-negative.
        // Postcondition: InfantrySkipGuard and its base Infantry are initialized.
        public InfantrySkipGuard(int row, int column, int strength, int attackRange, int[] artilleryProvider, int skipNumber, int[] shields)
            : base(row, column, strength, attackRange, artilleryProvider)
        {
            if (row < 0 || column < 0 || strength < 0 || attackRange < 0 || shields.Any(s => s < 0) || artilleryProvider.Any(a => a < 0) || skipNumber < 0)
            {
                throw new ArgumentException("No values should be negative!");
            }
            this.shields = shields;
            this.skipNumber = skipNumber;
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
        // Postcondition: Changes the mode of the InfantrySkipGuard.
        private void ChangeMode()
        {
            isUpMode = !isUpMode;
        }

        // Precondition: The InfantrySkipGuard should be alive and the input (x) should be a valid index in shields array.
        // Postcondition: If the InfantrySkipGuard is in up mode and the shield at index (x + skipNumber) % shields.Length is viable, decreases the shield value by 1 and returns true.
        //                If the InfantrySkipGuard is not in up mode, sets the shield at index (x + skipNumber) % shields.Length to 0 and returns true.
        //                If the InfantrySkipGuard is not alive, returns false.
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

        // Precondition: The input (x) should be a valid index in shields array.
        // Postcondition: Performs a block operation on shield at index (x + skipNumber) % shields.Length.
        public bool Block(int x)
        {
            if (x < 0 || x >= shields.Length)
            {
                throw new ArgumentException("Invalid shield index.");
            }
            return BaseBlock((x + skipNumber) % shields.Length);
        }

        // Precondition: x, y, q, and shieldIndex should be valid indices.
        // Postcondition: If the attack on the target at position (x, y) with quality q is successful, perform a block operation on shield at index (shieldIndex + skipNumber) % shields.Length.
        public bool AttackAndBlock(int x, int y, int q, int shieldIndex)
        {
            // Use Infantry's Target method
            bool attackResult = Target(x, y, q);

            // If the attack was successful, use SkipGuard's Block method
            if (attackResult)
            {
                bool res = Block(shieldIndex);
                return res;
            }

            return false;
        }
    }

}
