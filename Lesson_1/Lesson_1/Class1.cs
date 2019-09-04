using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1
{
    class Clas
    {
        public class Player
        {
            public bool isalive;
            public Vector2 Position;            

            public Player(Vector2 position)
            {
                isalive = true;
                Position = position;
            }

            public static bool EqualsPlayerPosition(Player player_1 , Player player_2)
            {
                return player_1.Position.Equals(player_2.Position);
            }
        }
       public class Vector2
       {
            private int _x;
            private int _y;

            public Vector2(int x , int y)
            {
                _x = x;
                _y = y;               
            }

            Random random = new Random();


            public bool Equals(Vector2 vector)
            {
                return vector._x == this._x && vector._y == this._y;
            }

            public void AddRandomOffset()
            {
                _x += random.Next(-1, 1);
                _y += random.Next(-1, 1);

                if (_x < 0)
                    _x = 0;
                
                if (_y < 0)
                    _y = 0;
            }
        }
        class Program
        {
            public static void Main(string[] args)
            {
                Player player1 = new Player(new Vector2(5, 5));
                Player player2 = new Player(new Vector2(10, 10));
                Player player3 = new Player(new Vector2(15, 15));


                Random random = new Random();

                while (true)
                {
                    if (Player.EqualsPlayerPosition(player1, player2))
                    {
                        player1.isalive = false;
                        player2.isalive = false;
                    }

                    if (Player.EqualsPlayerPosition(player1, player3))
                    {
                        player1.isalive = false;
                        player3.isalive = false;
                    }

                    if (Player.EqualsPlayerPosition(player2, player3))
                    {
                        player2.isalive = false;
                        player3.isalive = false;
                    }
                    player1.Position.AddRandomOffset();
                    player2.Position.AddRandomOffset();
                    player3.Position.AddRandomOffset();

                    if (player1.isalive)
                    {
                        Console.SetCursorPosition(obj1x, obj1y);
                        Console.Write("1");
                    }

                    if (player2.isalive)
                    {
                        Console.SetCursorPosition(obj2x, obj2y);
                        Console.Write("2");
                    }

                    if (player3.isalive)
                    {
                        Console.SetCursorPosition(obj3x, obj3y);
                        Console.Write("3");
                    }
                }
            }
        }
    }
}
