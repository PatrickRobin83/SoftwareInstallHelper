/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   LogHelper.cs
 *   Date			:   2020-08-11 11:11:22
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using log4net;
using log4net.Config;
using RietRob.Desktop.UI.Enums;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace RietRob.Desktop.UI.Helper
{
    public static class LogHelper
    {

        #region Fields
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger
        //      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets the Logger to Log to
        /// </summary>
        private static readonly ILog Log = LogHelper.GetLogger();

        #endregion

        #region Properties
        /// <summary>
        /// LogFileName to Log to
        /// </summary>
        public static string LogFileName { get; } = Environment.CurrentDirectory + @"\Logs\" + DateTime.Now.ToString("yyyy-MM-dd")+ "_Logfile";
        #endregion

        #region Constructor

        #endregion

        #region Methods


        /// <summary>
        /// Gets the Logger and overgive a filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            return LogManager.GetLogger(filename);
        }

        public static void CreateLogFile()
        {

            if (!Directory.Exists(Environment.CurrentDirectory + @"\Logs\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Logs\");
            }

            if (!File.Exists(LogFileName + ".log"))
            {
                using (FileStream fs = new FileStream($@"{LogFileName}.log", FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        /// <summary>
        /// Writes the Logfile and fill some standard information at startup.
        /// </summary>
        public static void WriteLogOnStartup()
        {
            bool notLocked = false;
            while (!notLocked)
            {
                try
                {
                    FileStream fs = File.Open($"{LogFileName}.log", FileMode.OpenOrCreate,
                        FileAccess.ReadWrite, FileShare.None);
                    fs.Close();

                    notLocked = true;
                }
                catch (IOException ex)
                {
                    notLocked = false;
                }
            }
            using (StreamWriter sw = File.AppendText(LogFileName + ".log"))
            {
                sw.WriteLine("--------------------------------------------------------------------------------");
                sw.WriteLine($"Softwareversion = {typeof(LogHelper).Assembly.GetName().Version}");
                sw.WriteLine($"Installationpath = {Environment.CurrentDirectory}");
                sw.WriteLine($"Computername = {Environment.MachineName}");
                sw.WriteLine($"OS Version = {Environment.OSVersion}");
                sw.WriteLine($"Username LoggedIn = {Environment.UserName}");
                sw.WriteLine($"OS is 64 Bit = {Environment.Is64BitOperatingSystem}");
                sw.WriteLine("--------------------------------------------------------------------------------");
                sw.Close();
                sw.Dispose();
            }
        }

        /// <summary>
        /// Writes the given string and LogState to the LogFile 
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logState"></param>
        public static void WriteToLog(string logMessage, LogState logState)
        {
            GlobalContext.Properties["LogFile"] = LogFileName;
            string s = new Uri(Path.Combine(Path.GetDirectoryName(Assembly.
                GetExecutingAssembly().CodeBase), "log4net.config")).LocalPath;
            XmlConfigurator.Configure();
            switch (logState)
            {
                case LogState.Debug:
                {
                    Log.Debug(logMessage);
                    break;
                }
                case LogState.Info:
                {
                    Log.Info(logMessage);
                    break;
                }
                case LogState.Warn:
                {
                    Log.Warn(logMessage);
                    break;
                }
                case LogState.Error:
                {
                    Log.Error(logMessage);
                    break;
                }
                case LogState.Fatal:
                {
                    Log.Fatal(logMessage);
                    break;
                }
                default:
                {
                    Log.Debug(logMessage);
                    break;
                }
            }
        }

        #endregion
    }
}