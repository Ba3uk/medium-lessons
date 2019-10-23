using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson_4_1
{
    public class Program
    {
        public static void Main(string[] arrs)
        {
            ILogger consoleLogWriter = new ConsoleLogWriter();
            ILogger fileLogWriter = new FileLogWriter();

            ILogger secureConsoleLogWritter = new SecureLogWritter(consoleLogWriter);
            ILogger secureFileLogWritter = new SecureLogWritter(fileLogWriter);

            // лог в консоль и файл
            ILogger commonLogger = new CommonLogger(consoleLogWriter, fileLogWriter);

            // лог в консоль и в файл в четные дни
            ILogger secureCommonLogWritter = new SecureLogWritter(fileLogWriter, consoleLogWriter);

            ClassA objectOne = new ClassA(consoleLogWriter);
            objectOne.DoSomething("Сообщение в консоль - 1");

            ClassA objectTwo = new ClassA(fileLogWriter);
            objectTwo.DoSomething("Сообщение в файл - 2");

            ClassA objectThree = new ClassA(secureFileLogWritter);
            objectThree.DoSomething("Сообщение в файл в специальный день - 3");

            ClassA objectFour = new ClassA(commonLogger);
            objectFour.DoSomething("Сообщение в файл и консоль - 4");

            ClassA objectFive = new ClassA(commonLogger);
            objectFive.DoSomething("Сообщение в консоль и в файл только в чётный день недели - 5");

            Console.ReadKey();
        }
    }
    public interface ILogger
    {
        void WriteLog(string message);
    }

    public class ConsoleLogWriter : ILogger
    {
        public virtual void WriteLog(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class SecureLogWritter : ConsoleLogWriter
    {
        private List<ILogger> _loggers;

        public SecureLogWritter (params ILogger[] logger)
        {
            _loggers = logger.ToList();
        }
        public override void WriteLog(string message)
        {
            if ((int)(DateTimeProvider.Get().DayOfWeek) % 2 == 0)
            {
                _loggers.ForEach(logger => logger.WriteLog(message));
            }
        }
    }

    public class FileLogWriter : ILogger
    {
        public void WriteLog(string message)
        {
            File.AppendAllText(FileLogConfig.LogNameFile, message + "\n");
        }
    }

    public class CommonLogger : ILogger
    {
        private List<ILogger> _loggers;

        public CommonLogger(params ILogger[] logger)
        {
            _loggers = logger.ToList();
        }
        public void WriteLog(string message)
        {
            _loggers.ForEach(logger => logger.WriteLog(message));
        }
    }

    public static class FileLogConfig
    {
        public static readonly string LogNameFile = "Log.txt";
    }

    public static class DateTimeProvider
    {
        private static DateTime? _dateTime = null;
        public static DateTime Get() => _dateTime ?? DateTime.Now;
        public static void Set(DateTime? dateTime) => _dateTime = dateTime;
    }

    public class ClassA
    {
        private ILogger _logger;
        public ClassA (ILogger logger)
        {
            _logger = logger;
        }

        public void DoSomething(string message)
        {
            if (_logger == null)
                throw new NullReferenceException();

            _logger.WriteLog(message);           
        }
    }
}
