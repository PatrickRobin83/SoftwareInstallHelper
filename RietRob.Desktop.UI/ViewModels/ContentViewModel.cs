/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ContentViewModel.cs
 *   Date			:   2020-08-11 15:43:48
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using RietRob.Desktop.UI.Enums;
using RietRob.Desktop.UI.Helper;
using RietRob.Desktop.UI.Models;

namespace RietRob.Desktop.UI.ViewModels
{
    public class ContentViewModel : Conductor<object>.Collection.OneActive
    {

        #region Fields

        private string _btnInstallIsVisible = "Hidden";
        private string _btnAddInstallerIsVisible = "Hidden";
        private string _btnRemoveInstallerIsVisible = "Hidden";
        private ObservableCollection<object> _availableFiles;
        private Installer _selectedAvailableInstaller;
        private ObservableCollection<object> _pickedInstallerList;
        private Installer _selectedPickedInstaller;

        #endregion

        #region Properties

        public string btn_InstallIsVisible
        {
            get { return _btnInstallIsVisible; }
            set
            {
                _btnInstallIsVisible = value;
                NotifyOfPropertyChange(() => btn_InstallIsVisible);
            }
        }

        public string btn_AddInstallerIsVisible
        {
            get { return _btnAddInstallerIsVisible; }
            set
            {
                _btnAddInstallerIsVisible = value;
                NotifyOfPropertyChange(() => btn_AddInstallerIsVisible);
            }
        }

        public string btn_RemoveInstallerIsVisible
        {
            get { return _btnRemoveInstallerIsVisible; }
            set
            {
                _btnRemoveInstallerIsVisible = value;
                NotifyOfPropertyChange(() => btn_RemoveInstallerIsVisible);
            }
        }

        public Installer SelectedAvailableInstaller
        {
            get { return _selectedAvailableInstaller; }
            set
            {
                _selectedAvailableInstaller = value;
                NotifyOfPropertyChange(() => SelectedAvailableInstaller);
                NotifyOfPropertyChange(() => Canbtn_AddInstaller);
            }
        }

        public ObservableCollection<object> PickedInstallerList
        {
            get { return _pickedInstallerList; }
            set
            {
                _pickedInstallerList = value;
                NotifyOfPropertyChange(() => PickedInstallerList);
                NotifyOfPropertyChange(() => btn_RemoveInstallerIsVisible);
            }
        }
        public ObservableCollection<object> AvailableFiles
        {
            get { return _availableFiles; }
            set
            {
                _availableFiles = value;
                NotifyOfPropertyChange(() => AvailableFiles);
                NotifyOfPropertyChange(() => Canbtn_AddInstaller);
            }
        }

        public Installer SelectedPickedInstaller
        {
            get { return _selectedPickedInstaller; }
            set
            {
                _selectedPickedInstaller = value;
                NotifyOfPropertyChange(() => SelectedPickedInstaller);
                NotifyOfPropertyChange(() => Canbtn_RemoveInstaller);
            }
        }
        public string Installerpath
        {
            get { return Environment.CurrentDirectory + @"\Installer\"; }
        }
        #endregion

        #region Constructor

        public ContentViewModel()
        {
            AvailableFiles = new ObservableCollection<object>();
            PickedInstallerList = new ObservableCollection<object>();
            GetAllInstaller();

        }

        #endregion

        #region Methods

        private ObservableCollection<object> GetAllInstaller()
        {
            DirectoryInfo di = new DirectoryInfo(Installerpath);
            Installer tmpInstaller;
            foreach (var fi in di.GetFiles())
            {
                 tmpInstaller = new Installer();
                 tmpInstaller.Filename = fi.Name;
                 tmpInstaller.FileExtension = fi.Extension;
                 tmpInstaller.FullFileName = fi.FullName;

                 AvailableFiles.Add(tmpInstaller);
            }

            btn_AddInstallerIsVisible = CheckList(AvailableFiles);
            
            return AvailableFiles;
        }

        private string CheckList(ObservableCollection<object> listToCheck)
        {
            string visibility = "hidden";

            if (listToCheck.Count > 0)
            {
                visibility = "Visible";
            }
            else
            {
                visibility = "Hidden";
            }

            return visibility;
        }

        public bool Canbtn_AddInstaller
        {
            get
            {
                bool result = false;

                if (SelectedAvailableInstaller != null)
                {
                    result = true;
                }

                return result;
            }
        }

        public bool Canbtn_RemoveInstaller
        {
            get
            {
                bool result = false;
                if (SelectedPickedInstaller != null)
                {
                    result = true;
                }
                return result;
            }
        }

        public void btn_AddInstaller()
        {
            LogHelper.WriteToLog($"{SelectedAvailableInstaller.Filename} was added to the List for installation", LogState.Info);
            PickedInstallerList.Add(SelectedAvailableInstaller);
            AvailableFiles.Remove(SelectedAvailableInstaller);
            btn_InstallIsVisible = CheckList(PickedInstallerList);
            btn_AddInstallerIsVisible = CheckList(AvailableFiles);
            btn_RemoveInstallerIsVisible = CheckList(PickedInstallerList);
        }

        public void btn_RemoveInstaller()
        {
            AvailableFiles.Add(SelectedPickedInstaller);
            PickedInstallerList.Remove(SelectedPickedInstaller);
            btn_InstallIsVisible = CheckList(PickedInstallerList);
            btn_AddInstallerIsVisible = CheckList(AvailableFiles);
            btn_RemoveInstallerIsVisible = CheckList(PickedInstallerList);

        }

        public void btn_Install()
        {
            LogHelper.WriteToLog("Installation of selected Programs started", LogState.Info);
            btn_InstallIsVisible = "Hidden";
            if (PickedInstallerList.Count > 0)
            {
                foreach (Installer installer in PickedInstallerList)
                {
                    InstallRunner.InstallProgram(installer);
                }
            }

            Console.WriteLine("Installation finished");
            //LogHelper.WriteToLog("Installation finished.", LogState.Info);
            foreach (Installer installer in PickedInstallerList)
            {
                AvailableFiles.Add(installer);
            }
            PickedInstallerList.Clear();
        }
        #endregion

    }
}