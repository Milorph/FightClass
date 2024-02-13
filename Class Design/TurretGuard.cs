﻿/*
 * TurretGuard.cs
 * 
 * This class represents a specialized unit that combines the behavior of a Turret and a Guard. 
 * 
 * Class Invariants:
 * - Position (row, column), strength, attackRange, reviveBound, and values in shields and artilleryProvider should be non-negative.
 * - Supports Block functionality inherited from IGuard interface.
 * - Inherits from the Turret class.
 *
 * Implementation Invariants:
 * - The 'shields' array holds the status of all the shields, non-negative integers.
 * - 'IsAlive' checks whether the TurretGuard is alive based on the number of operational shields.
 * - 'ChangeMode' toggles the mode of the TurretGuard.
 * - The 'Block' method uses the mode and the given shield index to decrease the value of the corresponding shield, or sets it to 0.
 * - 'AttackAndBlock' is a composite method that uses the 'Target' method from Turret and the 'Block' method from IGuard.
 */

using System;

namespace FighterProgram
{
    public class TurretGuard : Turret, IGuard
    {
        private int[] shields;
        private bool isUpMode;

        // Precondition: All inputs should be non-negative. Arrays 'artilleryProvider' and 'shields' should not be null.
        // Postcondition: TurretGuard and its base Turret are initialized.
        public TurretGuard(int row, int column, int strength, int attackRange, int reviveBound, int[] artilleryProvider, int[] shields)
            : base(row, column, strength, attackRange, reviveBound, artilleryProvider)
        {
            if (row < 0 || column < 0 || strength < 0 || attackRange < 0 || reviveBound < 0)
            {
                throw new ArgumentException("No values should be negative!");
            }
            this.shields = shields;
            this.isUpMode = true;
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

        // Postcondition: TurretGuard mode is toggled.
        private void ChangeMode()
        {
            isUpMode = !isUpMode;
        }

        // Precondition: The input 'x' should be a valid index in shields array.
        // Postcondition: Blocks the shield at position 'x' and returns the result of the 'Block' operation.
        public bool Block(int x)
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

        // Precondition: 'x' and 'y' should be valid coordinates, 'q' a non-negative strength and 'shieldIndex' a valid shield index.
        // Postcondition: If attack was successful, the shield at 'shieldIndex' is blocked and the result is returned.
        public bool AttackAndBlock(int x, int y, int q, int shieldIndex)
        {
            // Use Turret's Target method
            bool attackResult = Target(x, y, q);

            // If the attack was successful, use Guard's Block method
            if (attackResult)
            {
                bool res = Block(shieldIndex);
                return res;
            }

            return false;
        }
    }
}
