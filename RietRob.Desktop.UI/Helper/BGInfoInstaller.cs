/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   BGInfoInstaller.cs
 *   Date			:   2020-10-02 10:14:49
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using RietRob.Desktop.UI.Models;

namespace RietRob.Desktop.UI.Helper
{
    public static class BGInfoInstaller
    {
        

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public static void installBGInfo(Installer bginfoInstaller)
        {
            if (Directory.Exists($@"{Environment.SpecialFolder.ProgramFiles}\{bginfoInstaller.Filename.Substring(0, 6)}\"))
            {
                if (Directory.GetFiles($@"{Environment.SpecialFolder.ProgramFiles}\{bginfoInstaller.Filename.Substring(0, 6)}\").Length > 0)
                {
                    DirectoryInfo di = new DirectoryInfo($@"{Environment.SpecialFolder.ProgramFiles}\{bginfoInstaller.Filename.Substring(0, 6)}\");
                    foreach (FileInfo fileInfo in di.GetFiles())
                    {
                        File.Delete(fileInfo.FullName);
                    }
                }
                Directory.Delete($@"{Environment.SpecialFolder.ProgramFiles}\{bginfoInstaller.Filename.Substring(0, 6)}\");
            }

            Directory.CreateDirectory($@"C:\Program Files\{bginfoInstaller.Filename.Substring(0, 6)}\");
            ZipFile.ExtractToDirectory(bginfoInstaller.FullFileName,
                $@"C:\Program Files\{bginfoInstaller.Filename.Substring(0, 6)}\");
            File.Copy($@"C:\Program Files\BGInfo\BGInfo.exe", $@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\BGInfo.exe", true);
            File.Copy($@"C:\Program Files\BGInfo\I7_Settings.bgi", $@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\I7_Settings.bgi", true);

            ProcessStartInfo startinfo = new ProcessStartInfo();
            startinfo.WindowStyle = ProcessWindowStyle.Hidden;
            startinfo.FileName = "cmd.exe";
            startinfo.Arguments = "/K \"C:\\Program Files\\BGInfo\\bg.bat\"";
            Process process = new Process();
            process.StartInfo = startinfo;
            process.Start();
            Thread.Sleep(5000);
            process.WaitForExit();
        }

        #endregion

    }
}