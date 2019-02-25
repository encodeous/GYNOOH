using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYNOOHLIB.IO
{
    public class Logger
    {
        public static void Log(string status,string msg)
        {
            Debug.WriteLine($"[{status}] {msg}");
        }
        public static void Log(string msg)
        {

        }
    }
}
