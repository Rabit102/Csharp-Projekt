using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launcher
{
    public class Controller
    {
        string pathPlatformer = Path.GetFullPath(@"..\..\..\..\unity\Games\Platformer Game.exe");
        string pathTower = Path.GetFullPath(@"..\..\..\..\unity\Games\heartgame \2d simple game nr 1.exe");

        public void Open()
        {
            Console.WriteLine(pathPlatformer);
            try
            {
                /*Process.Start(new ProcessStartInfo
                {
                    FileName = "..\\..\\..\\what2doAPI\\bin\\Debug\\net9.0\\what2doAPI.exe",
                    UseShellExecute = true
                });*/

                Process.Start(new ProcessStartInfo
                {

                    FileName = pathPlatformer,
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