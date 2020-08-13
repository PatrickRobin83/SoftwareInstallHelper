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

using Caliburn.Micro;
using RietRob.Desktop.UI.Enums;
using RietRob.Desktop.UI.Helper;
using RietRob.Desktop.UI.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace RietRob.Desktop.UI.ViewModels
{
    public class ContentViewModel : Conductor<object>.Collection.OneActive
    {
        #region Fields

        private string _btnInstallIsVisible = "Hidden";
        private string _btnAddInstallerIsVisible = "Hidden";
        private string _btnAddAllInstallerIsVisible = "Hidden";
        private string _btnRemoveInstallerIsVisible = "Hidden";
        private string _btnRemoveAllInstallerIsVisible = "Hidden";
        private ObservableCollection<Installer> _availableFiles;
        private Installer _selectedAvailableInstaller;
        private ObservableCollection<Installer> _pickedInstallerList;
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
        public string btn_AddAllInstallerIsVisible
        {
            get { return _btnAddAllInstallerIsVisible; }
            set
            {
                _btnAddAllInstallerIsVisible = value;
                NotifyOfPropertyChange(() => btn_AddAllInstallerIsVisible);
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
        public string btn_RemoveAllInstallerIsVisible
        {
            get { return _btnRemoveAllInstallerIsVisible; }
            set
            {
                _btnRemoveAllInstallerIsVisible = value;
                NotifyOfPropertyChange(() => btn_RemoveAllInstallerIsVisible);
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
        public ObservableCollection<Installer> PickedInstallerList
        {
            get { return _pickedInstallerList; }
            set
            {
                _pickedInstallerList = value;
                NotifyOfPropertyChange(() => PickedInstallerList);
                NotifyOfPropertyChange(() => btn_RemoveInstallerIsVisible);
                NotifyOfPropertyChange(() => btn_RemoveAllInstallerIsVisible);
                NotifyOfPropertyChange(() => Canbtn_RemoveAllInstaller);
            }
        }
        public ObservableCollection<Installer> AvailableFiles
        {
            get { return _availableFiles; }
            set
            {
                _availableFiles = value;
                NotifyOfPropertyChange(() => AvailableFiles);
                NotifyOfPropertyChange(() => Canbtn_AddInstaller);
                NotifyOfPropertyChange(() => Canbtn_AddAllInstaller);
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
            AvailableFiles = new ObservableCollection<Installer>();
            PickedInstallerList = new ObservableCollection<Installer>();
            GetAllInstaller();

        }

        #endregion

        #region Methods

        private ObservableCollection<Installer> GetAllInstaller()
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
            btn_AddAllInstallerIsVisible = CheckList(AvailableFiles);
            
            return AvailableFiles;
        }
        private string CheckList(ObservableCollection<Installer> listToCheck)
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
        public bool Canbtn_AddAllInstaller
        {
            get
            {
                bool result = false;

                if (AvailableFiles.Count > 0)
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
        public bool Canbtn_RemoveAllInstaller
        {
            get
            {
                bool result = false;
                if (PickedInstallerList.Count > 0)
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
            btn_AddAllInstallerIsVisible = CheckList(AvailableFiles);
            btn_RemoveInstallerIsVisible = CheckList(PickedInstallerList);
            btn_RemoveAllInstallerIsVisible = CheckList(PickedInstallerList);
        }
        public void btn_AddAllInstaller()
        {
            foreach (Installer installer in AvailableFiles)
            {
                PickedInstallerList.Add(installer);
                LogHelper.WriteToLog($"{installer.Filename} added to the List for installation", LogState.Info);
            }
            AvailableFiles.Clear();
            btn_InstallIsVisible = CheckList(PickedInstallerList);
            btn_AddInstallerIsVisible = CheckList(AvailableFiles);
            btn_AddAllInstallerIsVisible = CheckList(AvailableFiles);
            btn_RemoveInstallerIsVisible = CheckList(PickedInstallerList);
            btn_RemoveAllInstallerIsVisible = CheckList(PickedInstallerList);
            NotifyOfPropertyChange(() => Canbtn_RemoveAllInstaller);

        }
        public void btn_RemoveInstaller()
        {
            AvailableFiles.Add(SelectedPickedInstaller);
            PickedInstallerList.Remove(SelectedPickedInstaller);
            btn_InstallIsVisible = CheckList(PickedInstallerList);
            btn_AddInstallerIsVisible = CheckList(AvailableFiles);
            btn_AddAllInstallerIsVisible = CheckList(AvailableFiles);
            btn_RemoveInstallerIsVisible = CheckList(PickedInstallerList);
            btn_RemoveAllInstallerIsVisible = CheckList(PickedInstallerList);

        }
        public void btn_RemoveAllInstaller()
        {
            foreach (Installer installer in PickedInstallerList)
            {
                AvailableFiles.Add(installer);
            }
            PickedInstallerList.Clear();
            btn_InstallIsVisible = CheckList(PickedInstallerList);
            btn_AddInstallerIsVisible = CheckList(AvailableFiles);
            btn_AddAllInstallerIsVisible = CheckList(AvailableFiles);
            btn_RemoveInstallerIsVisible = CheckList(PickedInstallerList);
            btn_RemoveAllInstallerIsVisible = CheckList(PickedInstallerList);

        }
        public void btn_Install()
        {
            LogHelper.WriteToLog("Installation of selected Programs started", LogState.Info);
            btn_InstallIsVisible = "Hidden";
            if (PickedInstallerList.Count > 0)
            {
                InstallRunner.InstallPrograms(PickedInstallerList);
            }
            LogHelper.WriteToLog("Installation finished.", LogState.Info);
            AvailableFiles.Clear();
            GetAllInstaller();
            PickedInstallerList.Clear();
        }
        #endregion
    }
}