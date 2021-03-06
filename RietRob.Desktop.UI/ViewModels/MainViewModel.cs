﻿/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   MainViewModel.cs
 *   Date			:   2020-08-11 10:24:32
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Reflection;
using Caliburn.Micro;
using RietRob.Desktop.UI.Enums;
using RietRob.Desktop.UI.Helper;

namespace RietRob.Desktop.UI.ViewModels
{
    public class MainViewModel : Conductor<object>.Collection.OneActive
    {

        #region Fields

        private string _title = "SoftwareInstallHelper";
        private bool _contentIsVisible = true;
        private ContentViewModel _contentViewModel;
        private string _loggerText;
        #endregion

        #region Properties
        public string Title
        {
            get { return _title; }
        }

        public string LoggerText
        {
            get { return _loggerText; }
            set
            {
                _loggerText = value; 
                NotifyOfPropertyChange(() => LoggerText);
            }

        }
        public ContentViewModel ContentViewModel
        {
            get { return _contentViewModel; }
            set
            {
                _contentViewModel = value;
                NotifyOfPropertyChange(() => ContentViewModel);
            }
        }
        public bool ContentIsVisible
        {
            get { return _contentIsVisible; }
            set
            {
                _contentIsVisible = value;
                NotifyOfPropertyChange(() => ContentIsVisible);
            }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            LogHelper.CreateLogFile();
            LogHelper.WriteLogOnStartup();
            LogHelper.WriteToLog("Application started", LogState.Info);
            LoggerText = "1223";
            ContentViewModel = new ContentViewModel();
            ContentIsVisible = true;
            NotifyOfPropertyChange(() => ContentIsVisible);
            NotifyOfPropertyChange(() => ContentViewModel);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Writes a Info LogMessage and exits the Application
        /// </summary>
        public void Exit()
        {
            LogHelper.WriteToLog("Application stopped by User", LogState.Info);
            Environment.Exit(0);
        }
        #endregion
    }
}