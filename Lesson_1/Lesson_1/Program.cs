using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2_2
{
    public class User
    {
        private static int _lastID { get; set; }

        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Account Account { get; private set; }

        public User(string firstName, string lastName)
        {
            Id = _lastID++;
            FirstName = firstName;
            LastName = lastName;
            Account = new Account();
        }
    }

    public class Account
    {
        private static int _lastID { get; set; }

        public int Id { get; private set; }
        public int Currency { get; private set; }

        public Account()
        {
            Id = _lastID++;
            Currency = 0;
        }

        public void ReplenishAccount(int contribution)
        {
            if (contribution > 0)
            {
                Currency += contribution;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void DebitTheAccount(int amount)
        {
            if (amount > 0)
            {
                Currency -= amount;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }

    public class Bank
    {
        private List<User> _users;

        public Bank()
        {
            _users = new List<User>();
        }

        public void CreateNewUser(User user)
        {
            _users.Add(user);
        }

        public void DeleteClient(User user)
        {
            if (user == null) return;

            if (_users.Contains(user))
                _users.Remove(user);
        }

        public void TransferCurrency(User sender, User recipient, int replenish)
        {
            if (sender == null || recipient == null) throw new ArgumentNullException();
            if (replenish < 0) throw new ArgumentException();

            sender.Account.DebitTheAccount(replenish);
            recipient.Account.ReplenishAccount(replenish);
        }
    }

    public class ReplenishCommand : IConsoleCommand
    {
        public string Description { get; set; }
        public int Id { get; set; }
        private int _contribution;
        private User _user;

        public ReplenishCommand(User user, int contribution)
        {
            _user = user;
            _contribution = contribution;
        }

        public void DirectAction()
        {
            _user.Account.ReplenishAccount(_contribution);
        }

        public void OppositeAction()
        {
            _user.Account.DebitTheAccount(_contribution);
        }
    }
    public class DebitCommand:IConsoleCommand
    {
        public string Description { get; set; }
        public int Id { get; set; }
        private int _amount;
        private User _user;

        

        public void DirectAction()
        {
            _user.Account.DebitTheAccount(_amount);            
        }

        public void OppositeAction()
        {
            _user.Account.ReplenishAccount(_amount);
        }
    }
    public class CreateUser : IConsoleCommand
    {
        public string Description { get; set; }
        public int Id { get; set; }

        private User _user;
        private Bank _bank;

        public CreateUser(Bank bank)
        {
            _bank = bank;
        }

        public void SettingData()
        {
            Console.WriteLine("Введите Имя:");
            var firstName = Console.ReadLine();

            Console.WriteLine("Введите Фамилию:");
            var lastName = Console.ReadLine();

            _user = new User(firstName, lastName);
        }

        public void DirectAction()
        {
            _bank.CreateNewUser(_user);
        }

        public void OppositeAction()
        {
            _bank.DeleteClient(_user);
        }
    }

    public class DeleteUser : IConsoleCommand
    {
        public string Description { get; set; }
        public int Id { get; set; }

        private User _user;
        private Bank _bank;

        public DeleteUser(Bank bank)
        {
            _bank = bank;
        }

        public void SettingData()
        {
            Console.WriteLine("Укажите ID юзера:");
            string consoleInput = "";
            int id;
            while (int.TryParse(consoleInput , out id))
            {
                Console.WriteLine("Укажите ID юзера:");
                consoleInput = Console.ReadLine();
            }        
        }

        public void DirectAction()
        {
            _bank.CreateNewUser(_user);
        }

        public void OppositeAction()
        {
            _bank.DeleteClient(_user);
        }
    }


    interface IConsoleCommand
    {
        string Description { get; set; }
        int Id { get; set; }
        void SettingData();
        void DirectAction();
        void OppositeAction();
    }
}