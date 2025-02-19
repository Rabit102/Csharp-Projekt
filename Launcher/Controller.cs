using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launcher
{
    public class Controller
    {
        public void Open()
        {
            try
            {
                /*Process.Start(new ProcessStartInfo
                {
                    FileName = "..\\..\\..\\what2doAPI\\bin\\Debug\\net9.0\\what2doAPI.exe",
                    UseShellExecute = true
                });*/

                Process.Start(new ProcessStartInfo {

                    FileName = "..\\..\\..\\what2doAPI\\wwwroot\\index.html",
                    UseShellExecute = true
                });
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler!");
            }
        }

    }
}