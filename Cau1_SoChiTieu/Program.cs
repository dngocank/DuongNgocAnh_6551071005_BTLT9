using System;
using System.Windows.Forms;
using Cau1_SoChiTieu.Services;

namespace Cau1_SoChiTieu
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
                Application.Run(new FormChiTieu());
            }
            finally
            {
                LoggerService.CloseAndFlush();
            }
        }
    }
}
