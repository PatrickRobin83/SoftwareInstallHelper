/*
*----------------------------------------------------------------------------------
*          Filename:	InstallRunner.cs
*          Date:        2020.08.11 22:02:59
*          All rights reserved
*
*----------------------------------------------------------------------------------
* @author Patrick Robin <support@rietrob.de>
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using RietRob.Desktop.UI.Enums;
using RietRob.Desktop.UI.Models;

namespace RietRob.Desktop.UI.Helper

{
    public static class InstallRunner
    {

        #region Fields

        

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public static void InstallPrograms(ObservableCollection<Installer> installerCollection)
        {
            foreach (Installer fileToInstall in installerCollection)
            {
                LogHelper.WriteToLog($"{fileToInstall.Filename} installation started", LogState.Info);
                string substring = fileToInstall.Filename.Substring(0, 4);
                string fileToInstallArguments = "";
                string command = "";
                
                if (substring.Equals("usbd"))
                {
                    if (Directory.Exists($@"C:\Program Files\{fileToInstall.Filename.Substring(0, 12)}\"))
                    {
                        if (Directory.GetFiles($@"C:\Program Files\{fileToInstall.Filename.Substring(0, 12)}\").Length > 0)
                        {
                            DirectoryInfo di = new DirectoryInfo($@"C:\Program Files\{fileToInstall.Filename.Substring(0, 12)}\");
                            foreach (FileInfo fileInfo in di.GetFiles())
                            {
                                File.Delete(fileInfo.FullName);
                            }
                        }
                        Directory.Delete($@"C:\Program Files\{fileToInstall.Filename.Substring(0, 12)}\");
                    }
                    Directory.CreateDirectory($@"C:\Program Files\{fileToInstall.Filename.Substring(0, 12)}\");
                    ZipFile.ExtractToDirectory(fileToInstall.FullFileName,
                        $@"C:\Program Files\{fileToInstall.Filename.Substring(0, 12)}\");
                }
                else if (substring.Equals("BGIn"))
                {
                    if (Directory.Exists($@"C:\Program Files\{fileToInstall.Filename.Substring(0, 6)}\"))
                    {
                        if (Directory.GetFiles($@"C:\Program Files\{fileToInstall.Filename.Substring(0, 6)}\").Length > 0)
                        {
                            DirectoryInfo di = new DirectoryInfo($@"C:\Program Files\{fileToInstall.Filename.Substring(0, 6)}\");
                            foreach (FileInfo fileInfo in di.GetFiles())
                            {
                                File.Delete(fileInfo.FullName);
                            }
                        }
                        Directory.Delete($@"C:\Program Files\{fileToInstall.Filename.Substring(0, 6)}\");
                    }

                    Directory.CreateDirectory($@"C:\Program Files\{fileToInstall.Filename.Substring(0, 6)}\");
                    ZipFile.ExtractToDirectory(fileToInstall.FullFileName,
                        $@"C:\Program Files\{fileToInstall.Filename.Substring(0, 6)}\");
                    File.Copy($@"C:\Program Files\BGInfo\BGInfo.exe", $@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\BGInfo.exe",true);
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
                else if (substring.Equals("User"))
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
                        $@"C:\Program Files\UserBenchMark\UserBenchMark.exe",true);
                }

                else
                {
                    switch (substring)
                    {
                        case "CCle":
                            fileToInstallArguments = "/S";
                            break;
                        case "Gree":
                            fileToInstallArguments = "/VERYSILENT /NORESTART /SUPPRESSMSGBOXES";
                            break;
                        case "K-Li":
                            fileToInstallArguments = "/verysilent";
                            break;
                        case "Note":
                            fileToInstallArguments = "/S";
                            break;
                        case "Tree":
                            fileToInstallArguments = "/VERYSILENT";
                            break;
                        case "Ultr":
                            fileToInstallArguments = "/verysilent";
                            break;
                        case "vlc-":
                            fileToInstallArguments = "/S";
                            break;
                        case "Team":
                            fileToInstallArguments = "/S";
                            break;
                    }

                    command = $"/C {fileToInstall.FullFileName} {fileToInstallArguments}";

                    ProcessStartInfo startinfo = new ProcessStartInfo();
                    startinfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startinfo.FileName = "cmd.exe";
                    startinfo.Arguments = command;
                    Process process = new Process();
                    process.StartInfo = startinfo;
                    process.Start();
                    process.WaitForExit();
                }

                LogHelper.WriteToLog($"{fileToInstall.Filename} installation finished", LogState.Info);
            }
        }

        #endregion

        #region EventHandler

        #endregion
    }
}