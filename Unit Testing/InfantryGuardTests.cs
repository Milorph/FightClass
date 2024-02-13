using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FighterProgram.Tests
{
    [TestClass]
    public class InfantryGuardTests
    {
        private int[] shields;
        private int[] artilleryProvider;
        private InfantryGuard infantryGuard;

        [TestInitialize]
        public void TestInitialize()
        {
            shields = new int[] { 1, 2, 3, 4, 5 };
            artilleryProvider = new int[] { 10, 20, 30, 40, 50 };
            infantryGuard = new InfantryGuard(0, 0, 100, 50, artilleryProvider, shields);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "No values should be negative!")]
        public void InfantryGuard_Constructor_ShouldThrowArgumentException_WhenNegativeValuesProvided()
        {
            // Arrange
            int negativeValue = -1;
            int[] artillery = { 10, 20, 30, 40, 50 };
            int[] shields = { 10, 20, 30, 40, 50 };

            // Act
            InfantryGuard infantryGuard = new InfantryGuard(negativeValue, 0, 100, 50, artillery, shields);
        }


        [TestMethod]
        public void AttackAndBlock_ShouldReturnTrue_WhenAttackAndBlockAreSuccessful()
        {
            InfantryGuard infantryG = new InfantryGuard(0, 0, 10, 3, new int[] { 10 }, shields);
            // Arrange
            int x = 0;
            int y = 3;
            int q = 5;
            int shieldIndex = 3;

            // Act
            bool result = infantryGuard.AttackAndBlock(x, y, q, shieldIndex);

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
            bool result = infantryGuard.AttackAndBlock(x, y, q, shieldIndex);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Block_ShouldReturnTrue_WhenShieldBlocksSuccessfully()
        {
            // Arrange
            int x = 1;

            // Act
            bool result = infantryGuard.Block(x);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Block_ShouldReturnFalse_WhenHalfOfTheShieldsAreDestroyed()
        {
            // Arrange
            // Destroy more than half of the shields
            infantryGuard.Block(0);
            infantryGuard.Block(1);
            infantryGuard.Block(1);
            infantryGuard.Block(2);
            infantryGuard.Block(2);
            infantryGuard.Block(2);
            int x = 3;

            // Act
            bool result = infantryGuard.Block(x);

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
            infantryGuard.Block(x);

            // Assert - Expect an exception
        }
    }
}
