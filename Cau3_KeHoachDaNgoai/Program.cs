using System;
using System.Windows.Forms;
using Cau3_KeHoachDaNgoai.Services;

namespace Cau3_KeHoachDaNgoai
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
                Application.Run(new FormChuyenDi());
            }
            finally
            {
                LoggerService.CloseAndFlush();
            }
        }
    }
}
