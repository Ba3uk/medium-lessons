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
            public static bool operator ==(Player p1, Player p2)
            {
                return p1.Position == p2.Position;
            }

            public static bool operator !=(Player p1, Player p2)
            {
                return p1.Position != p2.Position;
            }

            public override bool Equals(object obj)
            {
                var player = obj as Player;
                return player != null &&
                       ID == player.ID &&
                       IsAlive == player.IsAlive &&
                       Position == player.Position;
            }

            private static Random _random = new Random();
            private static int _lastId = 0;

            public int ID { get; private set; }
            public bool IsAlive { get; private set; }

            public Vector2 Position { get; private set; }

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
                Position.X = AddRandomIntOffset(Position.X);
                Position.Y = AddRandomIntOffset(Position.Y);
            }

            private int AddRandomIntOffset(int currnetValue)
            {
                currnetValue += _random.Next(-1, 1);
                if (currnetValue < 0)
                    currnetValue = 0;
                return currnetValue;
            }

 

            public class Vector2
            {
                private int _x;
                public int X
                {
                    set
                    {
                        _x = value;
                    }
                    get { return _x; ; }
                }

                private int _y;
                public int Y
                {
                    set
                    {
                        _y = value;
                    }
                    get { return _y; ; }
                }

                public Vector2(int x, int y)
                {
                    X = x;
                    Y = y;
                }

                public static bool operator ==(Vector2 v1, Vector2 v2)
                {
                    return v1.X == v2.X && v2.Y == v1.Y;
                }

                public static bool operator !=(Vector2 v1, Vector2 v2)
                {
                    return v1.X != v2.X || v2.Y != v1.Y;
                }

                public override bool Equals(object obj)
                {
                    var vector = obj as Vector2;
                    return vector != null &&
                           _x == vector._x &&
                           X == vector.X &&
                           _y == vector._y &&
                           Y == vector.Y;
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
                            if (_allPayers[i] == _allPayers[j])
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
                    foreach (var player in _alivePlayers)
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
}
