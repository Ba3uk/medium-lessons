using System;
using System.Collections.Generic;

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

    public class ReplenishCommand : ConsoleCommand
    {
        private int _contribution;
        private User _user;

        public ReplenishCommand(Bank bank)
        {
            Bank = bank;
        }

        public override void SettingData()
        {
            Console.WriteLine("Введите ID пользователя:");
            var userId = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите сумму начислений");
            var contribution = int.Parse(Console.ReadLine());
        }

        public override void DirectAction()
        {
            _user.Account.ReplenishAccount(_contribution);
        }

        public override void OppositeAction()
        {
            _user.Account.DebitTheAccount(_contribution);
        }
    }

    public class DebitCommand : ConsoleCommand
    {
        private int _amount;
        private User _user;
        private Bank _bank;

        public DebitCommand(int id, string description, Bank bank)
        {
            Id = id;
            Description = description;
            _bank = bank;
        }

        public override void SettingData()
        {
            Console.WriteLine("Укажите ID юзера:");
            string consoleInput = "";
            int id;
            while (int.TryParse(consoleInput, out id))
            {
                Console.WriteLine("Укажите ID юзера:");
                consoleInput = Console.ReadLine();
            }
        }

        public override void DirectAction()
        {
            _user.Account.DebitTheAccount(_amount);
        }

        public override void OppositeAction()
        {
            _user.Account.ReplenishAccount(_amount);
        }
    }

    public class CreateUser : ConsoleCommand
    {
        private User _user;

        public CreateUser(int id, string description, Bank bank)
        {
            Id = id;
            Description = description;
            Bank = bank;
        }

        public override void SettingData()
        {
            Console.WriteLine("Введите Имя:");
            var firstName = Console.ReadLine();

            Console.WriteLine("Введите Фамилию:");
            var lastName = Console.ReadLine();

            _user = new User(firstName, lastName);
        }

        public override void DirectAction()
        {
            _bank.CreateNewUser(_user);
        }

        public override void OppositeAction()
        {
            _bank.DeleteClient(_user);
        }
    }

    public class DeleteUser : ConsoleCommand
    {
        private User _user;

        public DeleteUser(int id, string description, Bank bank)
        {
            Id = id;
            Description = description;
            Bank = bank;
        }

        public override void SettingData()
        {
            Console.WriteLine("Укажите ID юзера:");
            string consoleInput = "";
            int id;
            while (int.TryParse(consoleInput, out id))
            {
                Console.WriteLine("Укажите ID юзера:");
                consoleInput = Console.ReadLine();
            }
        }

        public override void DirectAction()
        {
            _bank.CreateNewUser(_user);
        }

        public override void OppositeAction()
        {
            _bank.DeleteClient(_user);
        }
    }

    interface IConsoleCommand
    {
        void SettingData();
        void DirectAction();
        void OppositeAction();
    }

    public abstract class ConsoleCommand : IConsoleCommand
    {
        protected Bank Bank;
        public string Description { get; protected set; }
        public int Id { get; protected set; }

        public override string ToString()
        {
            return $"{Id}. {Description}";
        }

        public abstract void DirectAction();

        public abstract void OppositeAction();

        public abstract void SettingData();
    }

    public class UIConsoleCommand<T> where T : ConsoleCommand, new()
    {
        private static int _lastID { get; set; }
        public int ID { get; private set; }

        public UIConsoleCommand<T>

        public ConsoleCommand createComand() { return new T(); }
    }
}
