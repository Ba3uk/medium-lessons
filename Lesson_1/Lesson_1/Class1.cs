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
            public static bool EqualsPlayerPosition(Player player_1, Player player_2)
            {
                return player_1.Position.X == player_2.Position.X && player_1.Position.Y == player_2.Position.Y;
            }

            private static Random _random = new Random();
            private static int _lastId = 0;

            public int ID { private set; get; }
            public bool IsAlive { private set; get; }
            public Vector2 Position { private set; get; }

            public Player(Vector2 position)
            {
                ID = _lastId++;
                IsAlive = true;
                Position = position;
            }

            public void KillPlayer()
            {
                IsAlive = false;
            }

            public void AddRandomVectorOffset()
            {
                Position.X += _random.Next(-1, 1);
                Position.Y += _random.Next(-1, 1);
            } 
        }
       public class Vector2
       {
            private int _x;
            public int X
            {
                set
                {
                    _x = value;

                    if (_x < 0)
                        _x = 0;
                }
                get { return _x; ; }
            }

            private int _y;
            public int Y
            {
                set
                {
                    _y = value;

                    if (_y < 0)
                        _y = 0;
                }
                get { return _y; ; }
            }

            public Vector2(int x , int y)
            {
                X = x;
                Y = y;               
            }   
        }

        public class GameLobby
        {
            private List<Player> _allPayers;
            private List<Player> _alivePlayers;

            private bool isEndGame
            {
                get => _allPayers.Count == 1;
            }

            public GameLobby(List<Player> players)
            {
                _allPayers = players;
            }

            public void PlayGame()
            {
                while (!isEndGame)
                {
                    PlaySession();
                    PurposeOfProgress();
                    PrintAlivePlayer();
                }
                Console.ReadKey();
            }

            private void PlaySession()
            {
                _alivePlayers = _allPayers;
                for (int i = 0; i < _allPayers.Count; i++)
                {
                    for (int j = i + 1; j < _allPayers.Count; j++)
                    {
                        if (Player.EqualsPlayerPosition(_allPayers[i], _allPayers[j]))
                        {
                            _allPayers[i].KillPlayer();
                            _allPayers[j].KillPlayer();
                            
                            _alivePlayers.RemoveAll(player =>
                            {
                                return player.ID == _allPayers[i].ID || player.ID == _allPayers[j].ID;
                            });
                        }
                    }
                }
                _allPayers = _alivePlayers;
            }

            private void PurposeOfProgress()
            {
                foreach(var player in _alivePlayers)
                {
                    player.AddRandomVectorOffset();
                }
            }

            private void PrintAlivePlayer()
            {
                foreach (var player in _alivePlayers)
                {
                    ConsoleWriter.PrintPlayerProgress(player);
                }
            }
        }

        public static class ConsoleWriter
        {
            public static void PrintPlayerProgress(Player player)
            {
                Console.SetCursorPosition(player.Position.X, player.Position.Y);
                Console.Write($"{player.ID}");
            }
        }

        class Program
        {
            public static void Main(string[] args)
            {
                Player player1 = new Player(new Vector2(5, 5));
                Player player2 = new Player(new Vector2(10, 10));
                Player player3 = new Player(new Vector2(15, 15));

                var lobby = new GameLobby(new List<Player> { player1, player2, player3 });

                lobby.PlayGame();
            }
        }
    }
}
