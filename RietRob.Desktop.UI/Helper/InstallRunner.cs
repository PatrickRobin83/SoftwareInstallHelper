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
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;
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
                LogHelper.WriteToLog($"{fileToInstall.Filename} wird installiert.", LogState.Info);
                string substring = fileToInstall.Filename.Substring(0, 4);
                string fileToInstallArguments = "";
                string command = "";
                if (substring.Equals("usbd"))
                {
                    ZipFile.ExtractToDirectory(fileToInstall.FullFileName,$"C:\\{Environment.SpecialFolder.Programs}\\{fileToInstall.Filename.Substring(0,17)}\\");
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
                }
                ProcessStartInfo startinfo = new ProcessStartInfo();
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;
                startinfo.FileName = "cmd.exe";
                startinfo.Arguments = command;
                Process process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit();
            }
        }

        #endregion

        #region EventHandler

        #endregion
    }
}