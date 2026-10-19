using System;
using System.Windows.Forms;
using Cau4_QuanLyTraSua.Services;

namespace Cau4_QuanLyTraSua
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            LoggerService.Initialize();

            try
            {
                Application.Run(new FormMainTraSua());
            }
            finally
            {
                LoggerService.CloseAndFlush();
            }
        }
    }
}
