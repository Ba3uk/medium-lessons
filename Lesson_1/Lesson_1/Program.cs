using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2_1
{
    abstract class GameEntity
    {
        public int Health;

        public void TakeDamage(int damage)
        {
            CalculateDamage(damage);
            if (Health <= 0)
            {
                Console.WriteLine("Я умер");
            }
        }
        public abstract void CalculateDamage(int damage);
    }

    class Human: GameEntity
    {
        public int Agility;

        public override void CalculateDamage(int damage)
        {
            Health -= damage / Agility;
        }
    }

    class Wombat : GameEntity
    {
        public int Armor;

        public override void CalculateDamage(int damage)
        {
            Health -= damage - Armor;
        }
    }
}
