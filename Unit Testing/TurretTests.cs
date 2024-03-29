﻿/* TurretTests.cs
 * 
 * In this file, I have tested my public methods for my derived Turret class
 * 
*/

using FighterProgram;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FighterProgram.Tests
{
    [TestClass]
    public class TurretTests
    {

        [TestMethod]
        public void Constructor_NegativeValues_ThrowsArgumentException()
        {
            // Arrange
            int negativeRow = -1;
            int column = 2;
            int strength = 100;
            int attackRange = -2;
            int reviveBound = -5;
            int[] artilleryProvider = { 1, 2, 3 };

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Turret(negativeRow, column, strength, attackRange, reviveBound, artilleryProvider));
        }

        // Test revive after certain amount of failed requests
        [TestMethod]
        public void TestRevive()
        {
            Turret turret = new Turret(0, 0, 5, 3, 3, new int[] { 10, 5 });
            Assert.IsFalse(turret.getIsPermanentlyDead());
            turret.Target(0, 3, 20); // Failed request
            turret.Target(0, 3, 20); // Failed request
            turret.Target(0, 3, 20); // Failed request, revive called
            Assert.AreEqual(5, turret.getStrength());
        }


        // Testing shift
        [TestMethod]
        public void TestShift()
        {
            Turret turret = new Turret(0, 0, 10, 5, 3, new int[] { 10, 5 });
            turret.Shift(2);
            Assert.AreEqual(2, turret.getAttackRange());
        }


        // Testing Target
        [TestMethod]
        public void TestTarget()
        {
            Turret turret = new Turret(0, 0, 10, 3, 3,new int[] { 10, 5 });
            Infantry target = new Infantry(3, 0, 5, 3, new int[] { 5 }); // Place the target at a row equal to the attack range

            // Set turret's attack range to the distance between turret and target
            turret.Shift(Math.Abs(target.getRow() - turret.getRow()));

            // Simulate turret targeting the infantry object
            bool targetResult = turret.Target(target.getRow(), target.getColumn(), target.getStrength());
            Assert.IsTrue(targetResult);
        }


        // Test if turret is permanently dead after certain failed requests
        [TestMethod]
        public void TestPermanentlyDead()
        {
            Turret turret = new Turret(0, 0, 5, 3, 3,new int[] { 10, 5 });
            Assert.IsFalse(turret.getIsPermanentlyDead());
            turret.Target(0, 3, 20); // Failed request
            turret.Target(0, 3, 20); // Failed request
            turret.Target(0, 3, 20); // Failed request, revive called
            turret.Target(0, 3, 20); // Failed request
            turret.Target(0, 3, 20); // Failed request
            turret.Target(0, 3, 20); // Failed request, permanently dead
            Assert.IsTrue(turret.getIsPermanentlyDead());
        }
    }
}
