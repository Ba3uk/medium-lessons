using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1_4

{
    class Vector2
    {
        public float X;
        public float Y;
    }

    class Motor
    {
        public float Speed { get; private set; }
        public Vector2 MovementDirection { get; private set; }
    }

    class Weapon
    {
        public string Name { get; private set; }

        public int Damage { get; private set; }
        public float WeaponCooldown { get; private set; }

        public bool IsReloading()
        {
            throw new NotImplementedException();
        }
    }

    class Player
    {
        public string Name { get; private set; }

        public int Age { get; private set; }  
        
        public Weapon Weapon { get; private set; }

        public Motor PlayerMotor { get; private set; }


        public void Move()
        {
            //Do move
        }

        public void Attack()
        {
            if (!Weapon.IsReloading())
            {
                //attack
            }
        }
    }
}
