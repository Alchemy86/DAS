using System;
using System.Windows.Forms;
using Ninject;

namespace ServiceTestApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IKernel kernel = new StandardKernel(new Bindings());
            Application.Run(new Form1(kernel));
        }
    }
}
