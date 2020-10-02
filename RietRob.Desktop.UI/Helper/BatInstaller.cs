/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   BatInstaller.cs
 *   Date			:   2020-10-02 10:10:57
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System.Diagnostics;
using System.IO;
using System.Threading;
using RietRob.Desktop.UI.Models;

namespace RietRob.Desktop.UI.Helper
{
    public static class BatInstaller
    {
        

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public static void installBat(Installer batInstaller)
        {
            if (File.Exists(@"C:\Users\Public\Documents\" + batInstaller.Filename))
            {
                File.Delete(@"C:\Users\Public\Documents\" + batInstaller.Filename);
            }
            File.Copy(batInstaller.FullFileName, @"C:\Users\Public\Documents\" + batInstaller.Filename);

            ProcessStartInfo startinfo = new ProcessStartInfo();
            startinfo.WindowStyle = ProcessWindowStyle.Normal;
            startinfo.FileName = "cmd.exe";
            startinfo.Arguments = "/K \"schtasks.exe /Create /SC BEIMSTART /TN ChangeWindowsSettings /TR C:\\USERS\\PUBLIC\\Public\\Test.bat\"";
            Process process = new Process();
            process.StartInfo = startinfo;
            process.Start();
            Thread.Sleep(2000);
            //process.WaitForExit();

        }

        #endregion

    }
}