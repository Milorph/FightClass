using Microsoft.VisualStudio.TestTools.UnitTesting;
using FighterProgram;

namespace FighterProgram.Tests
{
    [TestClass]
    public class SkipGuardTests
    {
        [TestMethod]
        public void Block_ShouldReturnTrue_WhenBlockIsSuccessful()
        {
            // Arrange
            int shieldIndex = 2;
            int skipNumber = 1;
            int[] shields = { 10, 20, 30, 40, 50 };
            SkipGuard skipGuard = new SkipGuard(shields, skipNumber);

            // Act
            bool result = skipGuard.Block(shieldIndex);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Block_ShouldThrowArgumentException_WhenShieldIndexIsNegative()
        {
            // Arrange
            int shieldIndex = -1;
            int skipNumber = 1;
            int[] shields = { 10, 20, 30, 40, 50 };
            SkipGuard skipGuard = new SkipGuard(shields, skipNumber);

            // Act
            bool result = skipGuard.Block(shieldIndex);
        }

        [TestMethod]
        public void Block_ShouldReturnFalse_WhenGuardIsNotAlive()
        {
            // Arrange
            int shieldIndex = 2;
            int skipNumber = 1;
            int[] shields = { 0, 0, 0, 0, 0 }; // All shields are 0, so the guard is not alive
            SkipGuard skipGuard = new SkipGuard(shields, skipNumber);

            // Act
            bool result = skipGuard.Block(shieldIndex);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
