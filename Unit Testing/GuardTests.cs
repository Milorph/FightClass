using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FighterProgram;

namespace FighterProgram.Tests
{
    [TestClass]
    public class GuardTests
    {

        [TestMethod]
        public void Constructor_NullOrEmptyShields_ThrowsArgumentException()
        {
            // Arrange
            int[] nullShields = null;
            int[] emptyShields = new int[0];

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Guard(nullShields));
            Assert.ThrowsException<ArgumentException>(() => new Guard(emptyShields));
        }

        [TestMethod]
        public void Block_ShouldReturnTrue_WhenGuardIsAliveAndShieldExist()
        {
            // Arrange
            int shieldIndex = 2;
            int[] shields = { 10, 20, 30, 40, 50 };
            Guard guard = new Guard(shields);

            // Act
            bool result = guard.Block(shieldIndex);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Block_ShouldReturnFalse_WhenGuardIsNotAlive()
        {
            // Arrange
            int shieldIndex = 2;
            int[] shields = { 0, 0, 0, 0, 0 }; // All shields are 0, so the guard is not alive
            Guard guard = new Guard(shields);

            // Act
            bool result = guard.Block(shieldIndex);

            // Assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid shield index.")]
        public void Block_ShouldThrowException_WhenShieldIndexIsNegative()
        {
            // Arrange
            int shieldIndex = -1;
            int[] shields = { 10, 20, 30, 40, 50 };
            Guard guard = new Guard(shields);

            // Act
            guard.Block(shieldIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid shield index.")]
        public void Block_ShouldThrowException_WhenShieldIndexIsGreaterThanOrEqualToShieldLength()
        {
            // Arrange
            int shieldIndex = 5;
            int[] shields = { 10, 20, 30, 40, 50 };
            Guard guard = new Guard(shields);

            // Act
            guard.Block(shieldIndex);
        }
    }
}
