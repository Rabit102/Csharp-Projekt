using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Launcher
{
    public class Controller
    {
        string pathPlatformer = Path.GetFullPath(@"..\..\..\..\unity\Games\Platformer Game.exe");
        string pathTower = Path.GetFullPath(@"..\..\..\..\unity\Games\heartgame\2d simple game nr 1.exe");
        string pathSiteHost = Path.GetFullPath(@"..\..\..\..\What2Do2Day");
        string pathSite = ("http://localhost:5086/");
        string pathCasino = Path.GetFullPath(@"..\..\..\..\unity\Games\casino\casino.txt");

        public void OpenSite() 
        {
            var serverProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "run",
                    WorkingDirectory = pathSiteHost,
                    CreateNoWindow = false,
                    UseShellExecute = true
                }
            };

            serverProcess.Start();
            Thread.Sleep(10000);

            try
            {
                Process.Start(new ProcessStartInfo
                {

                    FileName = pathSite,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler!");
            }
        }

        public void OpenPlatformer()
        {
            Console.WriteLine(pathPlatformer);
            try
            {
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

        public void OpenTowerDef()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {

                    FileName = pathTower,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler!");
            }
        }

        public void OpenCasino()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {

                    FileName = pathCasino,
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