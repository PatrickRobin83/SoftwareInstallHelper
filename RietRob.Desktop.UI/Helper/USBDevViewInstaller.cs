/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   USBDevViewInstaller.cs
 *   Date			:   2020-10-02 10:04:56
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System.IO;
using System.IO.Compression;
using RietRob.Desktop.UI.Models;

namespace RietRob.Desktop.UI.Helper
{
    public static class USBDevViewInstaller
    {
        

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public static void installUSBDevWiew(Installer usbDevViewInstaller )
        {
            if (Directory.Exists($@"C:\Program Files\{usbDevViewInstaller.Filename.Substring(0, 12)}\"))
            {
                if (Directory.GetFiles($@"C:\Program Files\{usbDevViewInstaller.Filename.Substring(0, 12)}\").Length > 0)
                {
                    DirectoryInfo di = new DirectoryInfo($@"C:\Program Files\{usbDevViewInstaller.Filename.Substring(0, 12)}\");
                    foreach (FileInfo fileInfo in di.GetFiles())
                    {
                        File.Delete(fileInfo.FullName);
                    }
                }
                Directory.Delete($@"C:\Program Files\{usbDevViewInstaller.Filename.Substring(0, 12)}\");
            }
            Directory.CreateDirectory($@"C:\Program Files\{usbDevViewInstaller.Filename.Substring(0, 12)}\");
            ZipFile.ExtractToDirectory(usbDevViewInstaller.FullFileName,
                $@"C:\Program Files\{usbDevViewInstaller.Filename.Substring(0, 12)}\");
        }

        #endregion

    }
}