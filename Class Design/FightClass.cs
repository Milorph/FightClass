/* 
 * Robert Widjaja
 * Multiple Inheritance & Interfaces
 * Revision History -- 6/2/2023 -> finalized documentation
 * Platform: Windows
 *
 *
 *
 * The program then proceeds to perform a series of actions on the Guard and Composite types from fighter and Guard, such as moving the infantry units to different locations,
 * attempting to Block depending on Up or Down mode, changing modes of each type whenever Block is success. This program also tests the new composite functionality of composite types
 * from fighter and guard, using the function called AttackAndBlock which uses Target from fighter and Block from guard to showcase the composite type is not only a concatenation of class members.
 * Finally, the program prints if it has blocked or not blocked using its shield from guard, hit the target or miss target from figher and lastly AttackAndBlock from its new composite function. 
 * The program then repeats this process multiple times with different sets of randomly generated composite types,
 * demonstrating the flexibility and scalability of the program.
 * 
 * 
 * IMPORTANT - Since values are random, there will be a chance that some values don't work well with each other, if so, keep running again to get expected mode transitions.
 */

using FighterProgram;
using System;
using System.Collections.Generic;

namespace FighterProgram
{
    public class FightClass
    {
        private static Random random = new Random();

        public static void Main()
        {
            List<IGuard> guards = InstantiateGuards();
            List<Fighter> fighters = InstantiateFighters();

            TestGuards(guards);
            TestFighters(fighters);
        }

        //Preconditions: None
        //Postconditions: A list of 3 guards is returned
        private static List<IGuard> InstantiateGuards()
        {
            List<IGuard> guards = new List<IGuard>();
            int[] shields = new int[5];
            for (int j = 0; j < shields.Length; j++)
            {
                shields[j] = random.Next(1, 5);
            }
            guards.Add(new Guard(shields));
            guards.Add(new SkipGuard(shields, random.Next(2, 5)));
            guards.Add(new QuirkyGuard(shields));
            return guards;
        }

        //Preconditions: None
        //Postconditions: A list of 6 fighters is returned
        private static List<Fighter> InstantiateFighters()
        {
            List<Fighter> fighters = new List<Fighter>();
            int[] artillery = new int[10];
            for (int j = 0; j < artillery.Length; j++)
            {
                artillery[j] = random.Next(5, 10);
            }
            int[] shields = new int[5];
            for (int j = 0; j < shields.Length; j++)
            {
                shields[j] = random.Next(1, 5);
            }

            fighters.Add(new TurretGuard(random.Next(5), random.Next(5), random.Next(10, 16), random.Next(1,6), random.Next(3, 5), artillery, shields));
            fighters.Add(new InfantryGuard(random.Next(5), random.Next(5), random.Next(10, 16), random.Next(1,6), artillery, shields));
            fighters.Add(new InfantrySkipGuard(random.Next(5), random.Next(5), random.Next(10, 16), random.Next(1, 6), artillery, random.Next(2, 5), shields));
            fighters.Add(new TurretSkipGuard(random.Next(5), random.Next(5), random.Next(10, 16), random.Next(1, 6), random.Next(3, 5), artillery, random.Next(2, 5), shields));
            fighters.Add(new InfantryQuirkyGuard(random.Next(5), random.Next(5), random.Next(10, 16), random.Next(1, 6), artillery, shields));
            fighters.Add(new TurretQuirkyGuard(random.Next(5), random.Next(5), random.Next(10, 16), random.Next(1, 6), random.Next(3, 5), artillery, shields));

            return fighters;
        }

        //Preconditions: The guards list is not empty
        //Postconditions: Each guard has had their blocking ability tested
        private static void TestGuards(List<IGuard> guards)
        {
            // For each guard, change mode and block an attack
            foreach (IGuard guard in guards)
            {
                string guardType = string.Empty;
                switch (guard)
                {
                    case QuirkyGuard:
                        guardType = "QuirkyGuard";
                        break;
                    case SkipGuard:
                        guardType = "SkipGuard";
                        break;
                    case Guard:
                        guardType = "Guard";
                        break;
                }

                if (!string.IsNullOrEmpty(guardType))
                {
                    guard.Block(random.Next(1, 5));
                    guard.Block(random.Next(1, 5));
                    guard.Block(random.Next(1, 5));
                    bool blockResult = guard.Block(random.Next(1, 5));
                    Console.WriteLine($"{guardType} has {(blockResult ? "Blocked the attack" : "Failed to block the attack")}");
                }
            }

            Console.WriteLine();
        }


        //TYPE CHECKING
        //Preconditions: The fighters list is not empty
        //Postconditions: Each fighter has had their targeting and blocking abilities tested
        private static void TestFighters(List<Fighter> fighters)
        {
            // For each composed types, test AttackAndBlock, target, and block

            foreach (Fighter fighter in fighters)
            {
                if (fighter is IGuard guardFighter)
                {
                    switch (guardFighter)
                    {
                        case InfantryGuard infantryGuard:
                            bool targetResult = fighter.Target(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"InfantryGuard has {(targetResult ? "Hit the target" : "Missed the target")}");
                            bool blockResult = guardFighter.Block(random.Next(1, 5));
                            Console.WriteLine($"InfantryGuard has {(blockResult ? "Blocked the attack" : "Failed to Block the Attack")}");
                            bool attackBlockResult = infantryGuard.AttackAndBlock(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"InfantryGuard has {(attackBlockResult ? "Successfully Attacked and Blocked" : "Failed to Attack and Block")}");
                            Console.WriteLine();
                            break;
                        case InfantrySkipGuard infantrySkipGuard:
                            targetResult = fighter.Target(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"InfantrySkipGuard has {(targetResult ? "Hit the target" : "Missed the target")}");
                            blockResult = guardFighter.Block(random.Next(1, 5));
                            Console.WriteLine($"InfantrySkipGuard has {(blockResult ? "Blocked the attack" : "Failed to Block the Attack")}");
                            attackBlockResult = infantrySkipGuard.AttackAndBlock(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"InfantrySkipGuard has {(attackBlockResult ? "Successfully Attacked and Blocked" : "Failed to Attack and Block")}");
                            Console.WriteLine();
                            break;
                        case TurretGuard turretGuard:
                            targetResult = fighter.Target(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"TurretGuard has {(targetResult ? "Hit the target" : "Missed the target")}");
                            blockResult = guardFighter.Block(random.Next(1, 5));
                            Console.WriteLine($"TurretGuard has {(blockResult ? "Blocked the attack" : "Failed to Block the Attack")}");
                            attackBlockResult = turretGuard.AttackAndBlock(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"TurretGuard has {(attackBlockResult ? "Successfully Attacked and Blocked" : "Failed to Attack and Block")}");
                            Console.WriteLine();
                            break;
                        case TurretSkipGuard turretSkipGuard:
                            targetResult = fighter.Target(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"TurretSkipGuard has {(targetResult ? "Hit the target" : "Missed the target")}");
                            blockResult = guardFighter.Block(random.Next(1, 5));
                            Console.WriteLine($"TurretSkipGuard has {(blockResult ? "Blocked the attack" : "Failed to Block the Attack")}");
                            attackBlockResult = turretSkipGuard.AttackAndBlock(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"TurretSkipGuard has {(attackBlockResult ? "Successfully Attacked and Blocked" : "Failed to Attack and Block")}");
                            Console.WriteLine();
                            break;
                        case InfantryQuirkyGuard infantryQuirkyGuard:
                            targetResult = fighter.Target(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"InfantryQuirkyGuard has {(targetResult ? "Hit the target" : "Missed the target")}");
                            blockResult = guardFighter.Block(random.Next(1, 5));
                            Console.WriteLine($"InfantryQuirkyGuard has {(blockResult ? "Blocked the attack" : "Failed to Block the Attack")}");
                            attackBlockResult = infantryQuirkyGuard.AttackAndBlock(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"InfantryQuirkyGuard has {(attackBlockResult ? "Successfully Attacked and Blocked" : "Failed to Attack and Block")}");
                            Console.WriteLine();
                            break;
                        case TurretQuirkyGuard turretQuirkyGuard:
                            targetResult = fighter.Target(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"TurretQuirkyGuard has {(targetResult ? "Hit the target" : "Missed the target")}");
                            blockResult = guardFighter.Block(random.Next(1, 5));
                            Console.WriteLine($"TurretQuirkyGuard has {(blockResult ? "Blocked the attack" : "Failed to Block the Attack")}");
                            attackBlockResult = turretQuirkyGuard.AttackAndBlock(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
                            Console.WriteLine($"TurretQuirkyGuard has {(attackBlockResult ? "Successfully Attacked and Blocked" : "Failed to Attack and Block")}");
                            Console.WriteLine();
                            break;
                    }
                }
            }
        }

    }
}
