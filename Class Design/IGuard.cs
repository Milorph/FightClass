/*
 * IGuarded.cs
 * 
 * Implementation:
 * 
 * All classes that inherit the interface of guard will have to implement Block
 */

namespace FighterProgram
{
    public interface IGuard
    {
        bool Block(int x);
    }
}
