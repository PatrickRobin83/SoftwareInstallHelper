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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="installerCollection"></param>
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
                    USBDevViewInstaller.installUSBDevWiew(fileToInstall);
                }
                else if (fileToInstall.FileExtension == ".bat")
                {
                    BatInstaller.installBat(fileToInstall);
                }
                else if (substring.Equals("BGIn"))
                {
                    BGInfoInstaller.installBGInfo(fileToInstall);
                }
                else if (substring.Equals("User"))
                {
                    UserBenchMarkInstaller.installUserBenchmark();
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