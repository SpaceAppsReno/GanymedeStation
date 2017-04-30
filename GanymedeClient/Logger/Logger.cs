using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ganymede.Logger
{
    public static class Logger
    {
        private static FileStream fileStream;

        public static void Write(string msg)
        {
            if(fileStream == null)
            {
                fileStream = new FileStream("..\\GanymedeClientLog.txt", FileMode.Append);
            }

            fileStream.Write(Encoding.ASCII.GetBytes(msg), 0, msg.Length);
            fileStream.Flush();
        }
    }
}
