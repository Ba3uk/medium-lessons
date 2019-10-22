using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_4_1
{
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
        private ILogger _logger;
        public virtual void WriteLog(string message)
        {
            Console.WriteLine(message);
        }

    }
        public class SecureConsoleLogWritter : ConsoleLogWriter
    {
        private DayOfWeek _targetDayWeek;

        public SecureConsoleLogWritter(DayOfWeek dayOfWeek)
        {
            _targetDayWeek = dayOfWeek;
        }

        public override void WriteLog(string message)
        {
            if (DateTime.Now.DayOfWeek == _targetDayWeek)
            {
                base.WriteLog(message);
            }
        }
    }

    public class FileLogWriter : ILogger
    {
        public void WriteLog(string message)
        {
            throw new NotImplementedException();
        }
    }
}
