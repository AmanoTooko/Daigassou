using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daigassou.Utils
{
    public static class FFProcess
    {
        public  static List<Process>  FindFFXIVProcess()
        {

            var processes = new List<Process>();
            processes.AddRange(Process.GetProcessesByName("ffxiv"));
            processes.AddRange(Process.GetProcessesByName("ffxiv_dx11"));
            return processes;

        }

    }
}
