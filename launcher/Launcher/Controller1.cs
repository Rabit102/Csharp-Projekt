using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{
	public Class1()
	{
        string pathPlatformer = Path.GetFullPath(@"..\..\..\..\unity\Games\Platformer Game.exe");
        string pathTower = Path.GetFullPath(@"..\..\..\..\unity\Games\heartgame \2d simple game nr 1.exe");
        string pathCasino;

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
