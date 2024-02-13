using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FighterProgram.Tests
{
    [TestClass]
    public class TurretQuirkyGuardTests
    {
        private int[] shields;
        private int[] artilleryProvider;
        private TurretQuirkyGuard turretQuirkyGuard;

        [TestInitialize]
        public void TestInitialize()
        {
            shields = new int[] { 1, 2, 3, 4, 5 };
            artilleryProvider = new int[] { 10, 20, 30, 40, 50 };
            turretQuirkyGuard = new TurretQuirkyGuard(0, 0, 100, 50, 3, artilleryProvider, shields);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "No values should be negative!")]
        public void TurretQuirkyGuard_Constructor_ShouldThrowArgumentException_WhenNegativeValuesProvided()
        {
            // Arrange
            int negativeValue = -1;
            int[] artillery = { 10, 20, 30, 40, 50 };
            int[] shields = { 10, 20, 30, 40, 50 };

            // Act
            TurretQuirkyGuard turretQuirkyGuard = new TurretQuirkyGuard(negativeValue, negativeValue, 100, 50, 3, artillery, shields);
        }


        [TestMethod]
        public void AttackAndBlock_ShouldReturnTrue_WhenAttackAndBlockAreSuccessful()
        {
            TurretQuirkyGuard turretQG = new TurretQuirkyGuard(0, 0, 10, 3, 3, new int[] { 10 }, shields);
            Infantry target = new Infantry(3, 0, 5, 3, new int[] { 5 }); // Place the target at a row equal to the attack range

            // Set turret's attack range to the distance between turret and target
            turretQG.Shift(Math.Abs(target.getRow() - turretQG.getRow()));

            int shieldIndex = 4;

            // Act
            bool result = turretQG.AttackAndBlock(target.getRow(), target.getColumn(), target.getStrength(), shieldIndex);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AttackAndBlock_ShouldReturnFalse_WhenAttackIsNotSuccessful()
        {
            // Arrange
            int x = 0;
            int y = 0;
            int q = 1000;  // The power is more than available artillery
            int shieldIndex = 2;

            // Act
            bool result = turretQuirkyGuard.AttackAndBlock(x, y, q, shieldIndex);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Block_ShouldReturnTrue_WhenShieldBlocksSuccessfully()
        {
            // Arrange
            int x = 1;

            // Act
            bool result = turretQuirkyGuard.Block(x);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Block_ShouldReturnFalse_WhenHalfOfTheShieldsAreDestroyed()
        {
            // Arrange
            // Destroy more than half of the shields
            turretQuirkyGuard.Block(0);
            turretQuirkyGuard.Block(1);
            turretQuirkyGuard.Block(1);
            turretQuirkyGuard.Block(2);
            turretQuirkyGuard.Block(2);
            turretQuirkyGuard.Block(2);
            int x = 3;

            // Act
            bool result = turretQuirkyGuard.Block(x);
            Console.WriteLine(result);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Block_ShouldThrowException_WhenInvalidShieldIndex()
        {
            // Arrange
            int x = 1000;  // Invalid shield index

            // Act
            turretQuirkyGuard.Block(x);

            // Assert - Expect an exception
        }
    }
}