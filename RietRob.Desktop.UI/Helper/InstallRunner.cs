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
using System.IO;
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

        public static void InstallProgram(Installer fileToInstall)
        {
            Console.WriteLine($"{fileToInstall.Filename} wird installiert.");
        }
        #endregion

        #region EventHandler

        #endregion


    }
}