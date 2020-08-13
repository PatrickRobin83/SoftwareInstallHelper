/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   Bootstrapper.cs
 *   Date			:   2020-06-15 08:33:34
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using RietRob.Desktop.UI.Helper;
using RietRob.Desktop.UI.ViewModels;

namespace RietRob.Desktop.UI
{
    public class Bootstrapper : BootstrapperBase
    {

        #region Fields

        #endregion

        #region Properties
        #endregion

        #region Constructor

        public Bootstrapper()
        {
            Initialize();
            LogHelper.CreateLogFile();
        }
        #endregion

        #region Methods

        

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
        #endregion
    }
}