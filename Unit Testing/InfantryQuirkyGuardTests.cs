using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FighterProgram.Tests
{
    [TestClass]
    public class InfantryQuirkyGuardTests
    {
        private int[] shields;
        private int[] artilleryProvider;
        private InfantryQuirkyGuard infantryQuirkyGuard;

        [TestInitialize]
        public void TestInitialize()
        {
            shields = new int[] { 1, 2, 3, 4, 5 };
            artilleryProvider = new int[] { 10, 20, 30, 40, 50 };
            infantryQuirkyGuard = new InfantryQuirkyGuard(0, 0, 100, 50, artilleryProvider, shields);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "No values should be negative!")]
        public void InfantryQuirkyGuard_Constructor_ShouldThrowArgumentException_WhenNegativeValuesProvided()
        {
            // Arrange
            int negativeValue = -1;
            int[] artillery = { 10, 20, 30, 40, 50 };
            int[] shields = { 10, 20, 30, 40, 50 };

            // Act
            InfantryQuirkyGuard infantryQuirkyGuard = new InfantryQuirkyGuard(negativeValue, 0, 100, 50, artillery, shields);
        }


        [TestMethod]
        public void AttackAndBlock_ShouldReturnTrue_WhenAttackAndBlockAreSuccessful()
        {
            InfantryQuirkyGuard infantryQG = new InfantryQuirkyGuard(0, 0, 10, 3, new int[] { 10 }, shields);
            // Arrange
            int x = 0;
            int y = 3;
            int q = 5;
            int shieldIndex = 3;

            // Act
            bool result = infantryQG.AttackAndBlock(x, y, q, shieldIndex);

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
            bool result = infantryQuirkyGuard.AttackAndBlock(x, y, q, shieldIndex);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Block_ShouldReturnTrue_WhenShieldBlocksSuccessfully()
        {
            // Arrange
            int x = 1;

            // Act
            bool result = infantryQuirkyGuard.Block(x);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Block_ShouldReturnFalse_WhenHalfOfTheShieldsAreDestroyed()
        {
            // Arrange
            // Destroy more than half of the shields
            infantryQuirkyGuard.Block(0);
            infantryQuirkyGuard.Block(1);
            infantryQuirkyGuard.Block(1);
            infantryQuirkyGuard.Block(2);
            infantryQuirkyGuard.Block(2);
            infantryQuirkyGuard.Block(2);
            int x = 3;

            // Act
            bool result = infantryQuirkyGuard.Block(x);
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
            infantryQuirkyGuard.Block(x);

            // Assert - Expect an exception
        }
    }
}