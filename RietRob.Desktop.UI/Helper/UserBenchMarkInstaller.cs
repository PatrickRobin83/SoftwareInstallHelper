/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   UserBenchMarkInstaller.cs
 *   Date			:   2020-10-02 10:18:11
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.IO;
using RietRob.Desktop.UI.Models;

namespace RietRob.Desktop.UI.Helper
{
    public static class UserBenchMarkInstaller
    {
        

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public static void installUserBenchmark()
        {
            if (Directory.Exists($@"C:\Program Files\UserBenchMark\"))
            {
                if (Directory.GetFiles($@"C:\Program Files\UserBenchMark\").Length > 0)
                {
                    DirectoryInfo di = new DirectoryInfo($@"C:\Program Files\UserBenchMark\");
                    foreach (FileInfo fileInfo in di.GetFiles())
                    {
                        File.Delete(fileInfo.FullName);
                    }
                }
                Directory.Delete($@"C:\Program Files\UserBenchMark\");
            }

            Directory.CreateDirectory($@"C:\Program Files\UserBenchMark\");

            File.Copy($@"{Environment.CurrentDirectory}\Installer\UserBenchMark.exe",
                $@"C:\Program Files\UserBenchMark\UserBenchMark.exe", true);
        }

        #endregion

    }
}