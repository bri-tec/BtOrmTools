using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loggers
{
    public class ConsoleLogger : ILogger
    {
        public bool Log(DateTime logDateTime, string message, object source)
        {
            Console.WriteLine(logDateTime.ToString() + $": ({source.GetType().Name}) " + message);
            return true;
        }
    }
}
