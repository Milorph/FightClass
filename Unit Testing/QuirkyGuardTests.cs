using Microsoft.VisualStudio.TestTools.UnitTesting;
using FighterProgram;

namespace FighterProgram.Tests
{
    [TestClass]
    public class QuirkyGuardTests
    {
        [TestMethod]
        public void Block_ShouldReturnTrue_WhenBlockIsSuccessful()
        {
            // Arrange
            int shieldIndex = 2;
            int[] shields = { 10, 20, 30, 40, 50 };
            QuirkyGuard quirkyGuard = new QuirkyGuard(shields);

            // Act
            bool result = quirkyGuard.Block(shieldIndex);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Block_ShouldThrowArgumentException_WhenShieldIndexIsNegative()
        {
            // Arrange
            int shieldIndex = -1;
            int[] shields = { 10, 20, 30, 40, 50 };
            QuirkyGuard quirkyGuard = new QuirkyGuard(shields);

            // Act
            bool result = quirkyGuard.Block(shieldIndex);
        }

        [TestMethod]
        public void Block_ShouldReturnFalse_WhenGuardIsNotAlive()
        {
            // Arrange
            int shieldIndex = 2;
            int[] shields = { 0, 0, 0, 0, 0 }; // All shields are 0, so the guard is not alive
            QuirkyGuard quirkyGuard = new QuirkyGuard(shields);

            // Act
            bool result = quirkyGuard.Block(shieldIndex);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
