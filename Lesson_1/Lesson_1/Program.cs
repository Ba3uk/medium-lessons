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

        public User GetUserById(int id) => _users.Find(user => user.Id == id);

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

    public class BankConsoleSystem
    {
        private List<ConsoleCommand> _executedCommands= new List<ConsoleCommand>();

        public BankConsoleSystem(Bank bank)
        {

        }

        private void PrintAllCommands()
        {
            Console.WriteLine("1.Создать пользователя");
            Console.WriteLine("2.Удалить пользователя");
            Console.WriteLine("3.Пополнить счет");

            if (_executedCommands.Count > 0)
                Console.WriteLine("4.Возврат последней операции");
        }
        private void ReadKey()
        {
           int userInput = ConsoleHelper.GetValidIntValue("Введите номер команды");
            switch (userInput)
            {
                case 1:

                    break;


                case 2:

                    break;

                case 3:

                    break;



            }
        }
    }

    public static class ConsoleHelper
    {
        public static int GetValidIntValue(string header)
        {
            int resultValue;
            string consoleInput;
            while (true)
            {
                Console.WriteLine($"{header}: ");
                consoleInput = Console.ReadLine();
                if (int.TryParse(consoleInput, out resultValue))
                {
                    return resultValue;
                }
                else
                {
                    Console.WriteLine($"Ошибка, ожидается целочисленное значение. Попробуйте еще раз.");
                }
            }
           
        }

        public static string GetValidStringValue(string header)
        {
            Console.WriteLine($"{header}: ");
            string consoleInput = Console.ReadLine();

            return consoleInput;
        }
    }

    public class ReplenishCommand : ConsoleCommand
    {
        private int _contribution;
        private User _user;

        public ReplenishCommand(Bank bank) : base(bank) { }

        public override bool SettingData()
        {
            var userId = ConsoleHelper.GetValidIntValue("Введите ID пользователя:");
            _contribution = ConsoleHelper.GetValidIntValue("Введите сумму начислений");
            _user = Bank.GetUserById(userId);
            return _user != null;                
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

        public DebitCommand(Bank bank) : base(bank) { }

        public override bool SettingData()
        {
            var userId = ConsoleHelper.GetValidIntValue("Введите ID пользователя:");
            _user = Bank.GetUserById(userId);
            return _user != null;
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

    public class CreateUserCommand : ConsoleCommand
    {
        private User _user;

        public CreateUserCommand(Bank bank) : base(bank) { }

        public override bool SettingData()
        {
            var firstName = ConsoleHelper.GetValidStringValue("Введите имя: ");
            var lastName = ConsoleHelper.GetValidStringValue("Введите Фамилию: ");
            _user = new User(firstName, lastName);
            return _user != null;

        }

        public override void DirectAction()
        {
            Bank.CreateNewUser(_user);
        }

        public override void OppositeAction()
        {
            Bank.DeleteClient(_user);
        }
    }

    public class DeleteUserCommand : ConsoleCommand
    {
        private User _user;

        public DeleteUserCommand(Bank bank) : base(bank) { }

        public override bool SettingData()
        {
            var userId = ConsoleHelper.GetValidIntValue("Введите ID пользователя:");
            _user = Bank.GetUserById(userId);
            return _user != null;
        }

        public override void DirectAction()
        {
            Bank.CreateNewUser(_user);
        }

        public override void OppositeAction()
        {
            Bank.DeleteClient(_user);
        }
    }

    interface IConsoleCommand
    {
        bool SettingData();
        void DirectAction();
        void OppositeAction();
    }

    public abstract class ConsoleCommand : IConsoleCommand
    {
        protected Bank Bank;
        public int Id { get; private set; }

        public ConsoleCommand(Bank bank)
        {
            Bank = bank;
        }

        public abstract void DirectAction();

        public abstract void OppositeAction();

        public abstract bool SettingData();
    }



    public class programm
    {
        public static void Main(string[] args)
        {
            Bank bank = new Bank();

            BankConsoleSystem consoleSystem = new BankConsoleSystem(bank);
            consoleSystem.PrintAllCommands();
            Console.ReadKey();
        }


    }
}


