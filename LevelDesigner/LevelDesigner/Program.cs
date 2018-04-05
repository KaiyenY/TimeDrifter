using System;
using System.Windows.Forms;

namespace LevelDesigner
{
#if WINDOWS || LINUX
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartUp());
        }
    }
#endif
}