using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace FlowTürk_Görsel_Programlama_Simülatörü
{
    static class Program
    {      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 0)
                Application.Run(new FrmAna());

            if (args.Length == 1) 
                Application.Run(new FrmAna(args[0])); 
        }
    }
}
