using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Diagnostics;
namespace MW.DataHub.BO.Common
{
    public class LocalLog
    {
        private string strlogpath = "Log";
        public string strLogPath
        {
            set { this.strLogPath = value; }
            get { return this.strlogpath; }
        }

        public LocalLog()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public LocalLog(string logFolderName)
        {
            this.strlogpath = logFolderName;
        }
        /// <summary>
        /// Write Log
        /// </summary>
        /// <param name="Msg"></param>
        public void wLog(string Msg)
        {
            string filePath;
            string mon = System.DateTime.Now.Month.ToString();
            string day = System.DateTime.Now.Day.ToString();
            if (mon.Length == 1) mon = "0" + mon;
            if (day.Length == 1) day = "0" + day;

            string fileName = System.DateTime.Now.Year.ToString() + mon + day + ".txt";

            if (!System.IO.Directory.Exists(this.strlogpath)) System.IO.Directory.CreateDirectory(this.strlogpath);
            filePath = this.strlogpath + fileName;
            System.IO.StreamWriter swriter;

            if (!File.Exists(filePath))
            {
                swriter = File.CreateText(filePath);
            }
            else
            {
                swriter = File.AppendText(filePath);
            }

            swriter.WriteLine(System.DateTime.Now.ToString() + " | " + Msg);
            swriter.Close();

        }
        /// <summary>
        /// View log
        /// </summary>
        /// <param name="logName">If logName is null, view current log</param>
        public bool vLog(string logName)
        {
            bool runFlag = true;

            if (logName == "")
            {
                logName = System.DateTime.Now.Year.ToString();
                logName += System.DateTime.Now.Month.ToString();
                logName += System.DateTime.Now.Day.ToString() + ".txt";
            }
            string logPath = this.strlogpath + logName;
            if (File.Exists(logPath))
            {
                try
                {
                    Process.Start("NOTEPAD.exe", logPath);
                }
                catch
                {
                    runFlag = false;
                }
            }
            else
            {
                runFlag = false;
            }
            return runFlag;
        }

        /// <summary>
        /// Get All Log files name
        /// </summary>
        /// <returns></returns>
        public ArrayList getLogs()
        {
            ArrayList _logFiles = new ArrayList();
            System.IO.DirectoryInfo dirInfo = new DirectoryInfo(this.strlogpath);

            foreach (System.IO.FileInfo fileInfo in dirInfo.GetFiles("*.txt"))
            {
                _logFiles.Add(fileInfo.Name);
            }
            this.sortingAL(ref _logFiles);

            return _logFiles;
        }
        private void sortingAL(ref ArrayList _logFiles)
        {
            //bubble sorting
            for (int i = 0; i < _logFiles.Count; i++)
            {
                string str;
                for (int j = _logFiles.Count - 1; j > i; j--)
                {
                    string fn1 = _logFiles[j].ToString().Substring(0, _logFiles[j].ToString().Length - 4);
                    string fn2 = _logFiles[j - 1].ToString().Substring(0, _logFiles[j - 1].ToString().Length - 4);

                    if (Convert.ToInt32(fn1) > Convert.ToInt32(fn2))
                    {
                        str = _logFiles[j].ToString();
                        _logFiles[j] = _logFiles[j - 1];
                        _logFiles[j - 1] = str;

                    }
                }

            }
        }
    }
}
