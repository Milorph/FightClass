/*
 * TurretSkipGuard.cs
 * 
 * This class represents a specialized unit that combines the behavior of a Turret and a SkipGuard.
 * 
 * Class Invariants:
 * - Position (row, column), strength, attackRange, reviveBound, skipNumber, and values in shields and artilleryProvider should be non-negative.
 * - Supports Block functionality inherited from IGuard interface.
 * - Inherits from the Turret class.
 *
 * Implementation Invariants:
 * - The 'shields' array holds the status of all the shields, non-negative integers.
 * - 'IsAlive' checks whether the TurretSkipGuard is alive based on the number of operational shields.
 * - 'ChangeMode' toggles the mode of the TurretSkipGuard.
 * - The 'Block' method uses the mode, the skipNumber, and the given shield index to decrease the value of the corresponding shield, or sets it to 0.
 * - 'AttackAndBlock' is a composite method that uses the 'Target' method from Turret and the 'Block' method from IGuard.
 */

using System;

namespace FighterProgram
{
    public class TurretSkipGuard : Turret, IGuard
    {
        private int[] shields;
        private int skipNumber;
        private bool isUpMode;

        // Precondition: All inputs should be non-negative. Arrays 'artilleryProvider' and 'shields' should not be null.
        // Postcondition: TurretSkipGuard and its base Turret are initialized.
        public TurretSkipGuard(int row, int column, int strength, int attackRange, int reviveBound, int[] artilleryProvider, int skipNumber, int[] shields)
            : base(row, column, strength, attackRange, reviveBound, artilleryProvider)
        {
            if (row < 0 || column < 0 || strength < 0 || attackRange < 0 || reviveBound < 0)
            {
                throw new ArgumentException("No values should be negative!");
            }
            this.shields = shields;
            this.skipNumber = skipNumber;
        }

        // Implementation: Checks whether at least half of the shields are operational.
        private bool IsAlive()
        {
            int viableShields = 0;
            foreach (int shield in shields)
            {
                if (shield > 0) viableShields++;
            }

            return viableShields >= shields.Length / 2.0;
        }

        // Postcondition: TurretSkipGuard mode is toggled.
        private void ChangeMode()
        {
            isUpMode = !isUpMode;
        }

        // Precondition: The input 'x' should be a valid index in shields array.
        // Postcondition: Blocks the shield at position 'x' and returns the result of the 'Block' operation.
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

        // Precondition: 'x' should be a valid index in shields array.
        // Postcondition: If Block operation was successful, it skips 'skipNumber' of shields from the index 'x'.
        public bool Block(int x)
        {
            if (x < 0 || x > shields.Length)
            {
                throw new ArgumentException("Invalid shield index.");
            }
            return BaseBlock((x + skipNumber) % shields.Length);
        }

        // Precondition: 'x' and 'y' should be valid coordinates, 'q' and 'shieldIndex' should be non-negative.
        // Postcondition: The 'Target' method is called with input parameters 'x', 'y', and 'q'.
        // If the attack was successful, it calls the 'Block' method with 'shieldIndex'.
        public bool AttackAndBlock(int x, int y, int q, int shieldIndex)
        {
            // Use Turret's Target method
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
