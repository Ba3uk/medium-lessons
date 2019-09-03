using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_1_1
{
    public class User
    {
        private static int _lastId = 0;

        readonly int _id;

        public int ID { get => _id; }

        public string Name;
        public string LastName;
        public int Salary;

        public User(string name, string lastName, int salary)
        {
            _id = _lastId++;
            Name = name;
            LastName = lastName;
            Salary = salary;
        }

        public override string ToString()
        {
            return string.Format($"ID:{ID} Сотрудник {LastName} {Name} получает зарплату: {Salary}");
        }
    }

    public class UserList
    {
        private List<User> _usersList;

        public UserList(List<User> users)
        {
            _usersList = users;
        }

        public void AddUserToList(User user)
        {
            _usersList.Add(user);
        }

        public User GetUserById(int userId)
        {
           return _usersList.Find(user => user.ID == userId);
        }

        public User GetUserByName(string userName)
        {
            return _usersList.Find(user => user.Name == userName);
        }

        public List<User> GetUsersWithSalaryLess(int targetSalary)
        {
            return _usersList.FindAll(user => user.Salary < targetSalary);
        }

        public List<User> GetUsersWithSalaryMore(int targetSalary)
        {
            return _usersList.FindAll(user => user.Salary > targetSalary);
        }

        public List<User> GetUsersWithSalaryBetween(int minSalary , int maxSalary)
        {
            return _usersList.FindAll(user => user.Salary > minSalary && user.Salary < maxSalary);
        }
    }

    public class Progamm
    {
        static void Main(string[] args)
        {
            var user1 = new User("Andru", "Lazarev", 1);
            var user2 = new User("Andru1", "Lazarev2", 2);
            var user3 = new User("Andru2", "Lazarev2", 3);
            var user4 = new User("Andru3", "Lazarev2", 4);
            var user5 = new User("Andru4", "Lazarev2", 5);

            var userList = new UserList(new List<User>
            {
                user1,
                user2,
                user3,
                user4,
                user5
            });

            Console.WriteLine("Сотрудник у которого id = 1");
            Console.WriteLine(userList.GetUserById(1).ToString());

            Console.WriteLine("Сотрудник у которого имя - Andru4");
            Console.WriteLine( userList.GetUserByName("Andru4").ToString());

            Console.WriteLine("Сотрудник у которого имя - зп меньше 2");
            
            foreach(var user in userList.GetUsersWithSalaryLess(2))
            {
               Console.WriteLine( user.ToString());
            }


            Console.WriteLine("Сотрудник у которого имя - зп больше 4");
            foreach (var user in userList.GetUsersWithSalaryMore(4))
            {
                Console.WriteLine(user.ToString());
            }

            Console.WriteLine("Сотрудник у которого имя - зп между 2 и 4");
            foreach (var user in userList.GetUsersWithSalaryBetween(2,4))
            {
                Console.WriteLine(user.ToString());
            }

            Console.ReadKey();
        }
    }

}
