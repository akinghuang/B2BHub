using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Net.FtpClient;

namespace MW.DataHub.API.Net
{
    public enum FtpTools
    {
        FTPNet, FTPAPI
    }
    public enum FtpDirection
    {
        Download, Upload
    }
    public enum FtpMessageType
    {
        Start, End, Success, Fail, Message
    }
    public class FtpLogin
    {
        private string _UserID = "anonymous";
        public string UserName { get { return _UserID; } set { _UserID = value; } }
        private string _Password = "a@D.com";
        public string Password { get { return _Password; } set { _Password = value; } }
        private string _FtpIP = "localhost";
        public string Server { get { return _FtpIP; } set { _FtpIP = value; } }
        private int _Port = 21;
        public int Port { get { return _Port; } set { _Port = value; } }
        private bool _Passive = true;
        /// <summary>
        /// Use Passive to send and receive the Data under fireware.
        /// </summary>
        public bool Passive { get { return _Passive; } set { _Passive = value; } }

        private string _Project = "";
        public string Project { get { return _Project; } set { _Project = value; } }
        /// <summary>
        /// 定义这个确定FTP是上传还是下载.
        /// Default: FtpDirection.Download
        /// </summary>
        public FtpDirection Direction { get { return _direction; } set { _direction = value; } }
        private FtpDirection _direction = FtpDirection.Download;

        /// <summary>
        /// 定义要上传还是下载的文件类型.
        /// Default: FtpDirection.Download
        /// </summary>
        public string FileExtension { get { return _FileExtension; } set { _FileExtension = value; } }
        private string _FileExtension = "*.*";
        /// <summary>
        /// 定义本地要上传还是下载的文件夹
        /// Default: ""
        /// </summary>
        [Description("Local File Path")]
        public string LocalFolder { get { return _LocalFolder; } set { _LocalFolder = value; } }
        private string _LocalFolder = "";

        /// <summary>
        /// 定义本地上传文件后备份的目录，
        /// Default: "BAK"
        /// </summary>
        [Description("Bakup Path under current folder")]
        public string LocalBakFolder { get { return _LocalBakFolder; } set {if(!string.IsNullOrEmpty(value)) _LocalBakFolder = value; } }
        private string _LocalBakFolder = "BAK";
        /// <summary>
        /// 定义FTP服务器上要上传还是下载的文件夹
        /// </summary>
        [Description("FTP Path which Dimerco send file to Agent")]
        public string RemoteFolder { get { return _RemoteFolder; } set { _RemoteFolder = value; } }
        private string _RemoteFolder = "";

        /// <summary>
        /// 定义是否需要SSL, FTPS
        /// Default: false
        /// </summary>
        [Description("Defined that whethere need SSL")]
        public bool UseSsl { get { return _SslEnabled; } set { _SslEnabled = value; } }
        private bool _SslEnabled = false;


        [Description("Defined that what FTP Tools Used")]
        public FtpTools FTPTool { get { return _FTPTool; } set { _FTPTool = value; } }
        private FtpTools _FTPTool = FtpTools.FTPNet;

    }


    public delegate void MessageChangedHandler(FtpMessageType msgType, string msg);
    public delegate void FileProcessCompletedHandler(string FileFullName, string msg);
    public interface IFTPHandler
    {
        event MessageChangedHandler MessageChanged;
        event FileProcessCompletedHandler FileProcessCompleted;
        bool doFTP(FtpLogin ftpInfo);
    }
    public class clsFTPHandler : IFTPHandler
    {
        public virtual event MessageChangedHandler MessageChanged = null;
        public virtual event FileProcessCompletedHandler FileProcessCompleted = null;
        public virtual bool doFTP(FtpLogin ftpInfo)
        {
            if (ftpInfo.FTPTool == FtpTools.FTPAPI)
            {
                clsAPIFTP ftp = new clsAPIFTP();
                ftp.MessageChanged += new MessageChangedHandler(ftp_MessageChanged);
                ftp.FileProcessCompleted += new FileProcessCompletedHandler(ftp_FileProcessCompleted);
                return ftp.doFTP(ftpInfo);
            }
            else
            {
                clsNetFTP ftp = new clsNetFTP();
                ftp.MessageChanged += new MessageChangedHandler(ftp_MessageChanged);
                ftp.FileProcessCompleted += new FileProcessCompletedHandler(ftp_FileProcessCompleted);
                return ftp.doFTP(ftpInfo);
            }
        }

        void ftp_FileProcessCompleted(string FileFullName, string msg)
        {
            if (FileProcessCompleted != null) FileProcessCompleted(FileFullName, msg);
        }

        void ftp_MessageChanged(FtpMessageType msgType, string msg)
        {
            if (MessageChanged != null) MessageChanged(msgType, msg);
        }
    }

    class FTPBase
    {
        public virtual event MessageChangedHandler MessageChanged = null;
        public virtual event FileProcessCompletedHandler FileProcessCompleted = null;
        protected FtpLogin ftpInfo = null;
        protected string _Root = "/";
        /// <summary>
        /// Backup the upload的文件
        /// </summary>
        /// <param name="file"></param>
        protected void BackupFile(FileInfo file)
        {

            //定义到具体的每天一个folder
            string strFileBakPath = string.Format("{0}\\{1}\\{2}\\"
                ,file.Directory.FullName, ftpInfo.LocalBakFolder , DateTime.Now.ToString("yyyyMMdd"));

            if (!Directory.Exists(strFileBakPath))
                Directory.CreateDirectory(strFileBakPath);
            strFileBakPath += file.Name;

            if (File.Exists(strFileBakPath))
                File.Delete(strFileBakPath);
            file.MoveTo(strFileBakPath);
            if (FileProcessCompleted != null) 
                FileProcessCompleted(file.FullName, file.Name);
        }

        private string GetExtension(string fileName)
        {
            string[] fs = fileName.Split('.');
            if (fs.Length > 1)
                return fs[fs.Length - 1];
            else
                return "";
        }

        protected string GetRemoteRootFolder(string RemoteFolder)
        {
            if (RemoteFolder == null)
                return _Root;

            if (RemoteFolder.IndexOf('/') == 0)
                return RemoteFolder;
            else if (_Root != "/")
                return _Root + "/" + RemoteFolder;
            else
                return _Root + RemoteFolder;
        }

        /// <summary>
        /// 用来检查需要上传和下载的文件名是否和FTPLogin的fileExtension一致
        /// </summary>
        /// <param name="fileName">检查的文件名</param>
        /// <returns></returns>
        protected bool IsAuthExtension(string fileName)
        {
            string ext = ftpInfo.FileExtension;
            if (string.IsNullOrEmpty(ext) || ext.Trim() == "*.*")
                return true;

            string[] fs = ext.Split('|');
            string fileext = GetExtension(fileName).ToUpper();

            foreach (string file in fs)
            {
                if (GetExtension(file).ToUpper() == fileext)
                    return true;
            }
            return false;
        }
    }

    class clsNetFTP : FTPBase
    {
        public override event MessageChangedHandler MessageChanged = null;
        public bool doFTP(FtpLogin login)
        {
            bool bRet = true;
            ftpInfo = login;

            if (MessageChanged != null)
                MessageChanged(FtpMessageType.Start, string.Format("Start doFTP {1} for FTP server:{0}", ftpInfo.Server, ftpInfo.Direction));
            using (FtpClient ftp = new FtpClient(ftpInfo.UserName, ftpInfo.Password, ftpInfo.Server, ftpInfo.Port))
            {
                if (ftpInfo.Passive == false)
                    ftp.DefaultDataMode = FtpDataMode.Active;
                ftp.UseSsl = ftpInfo.UseSsl;
                _Root = ftp.CurrentDirectory.FullName;
                try
                {
                    if (ftpInfo.Direction == FtpDirection.Download)
                    {
                        DownloadData(ftp, ftpInfo);
                    }
                    if (ftpInfo.Direction == FtpDirection.Upload)
                    {
                        UploadData(ftp, ftpInfo);
                    }

                    bRet = true;
                    if (MessageChanged != null)
                        MessageChanged(FtpMessageType.Success, string.Format("Success to {0} all files", ftpInfo.Direction));
                }
                catch (Exception ex)
                {
                    bRet = false;
                    if (MessageChanged != null)
                        MessageChanged(FtpMessageType.Fail, string.Format("Failed to {0} file, Reason:{1}", ftpInfo.Direction, ex.Message));
                }
                finally
                {
                    ftp.Disconnect();
                }
            }
            if (MessageChanged != null)
                MessageChanged(FtpMessageType.End, string.Format("End doFTP {1} for FTP server:{0}", ftpInfo.Server, ftpInfo.Direction));
            return bRet;
        }

        private void DownloadData(FtpClient ftp, FtpLogin ftpInfo)
        {

            string strLocalPath = ftpInfo.LocalFolder;
            if (strLocalPath == null) strLocalPath = "";
            if (!Directory.Exists(strLocalPath))
                Directory.CreateDirectory(strLocalPath);

            string strRemotePath = this.GetRemoteRootFolder(ftpInfo.RemoteFolder);
            if (strRemotePath != "")
                try
                {
                    ftp.SetWorkingDirectory(strRemotePath);
                }
                catch
                {
                    throw new Exception(string.Format("The remote download folder:{0} is wrong or not existed in FTP server:{1}", strRemotePath, ftpInfo.Server));
                }

            foreach (FtpFile file in ftp.CurrentDirectory.Files)
            {
                //如果不是需要的文件后缀类型，就不处理
                if (!IsAuthExtension(file.FullName))
                    continue;
                string strFile = string.Format("Download file {0},{1} from FTP:{2}", strLocalPath + "\\" + file.Name, file.Length, file.FullName);
                if (MessageChanged != null) MessageChanged(FtpMessageType.Message, strFile);
                System.Windows.Forms.Application.DoEvents();
                file.Download(strLocalPath + "\\" + file.Name);
                file.Delete();
            }

            //return Directory.GetFiles(strLocalPath);
        }
        /// <summary>
        /// Process to upload the file
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="ftpInfo"></param>
        /// <returns></returns>
        private void UploadData(FtpClient ftp, FtpLogin ftpInfo)
        {
            string strLocalPath = ftpInfo.LocalFolder;
            if (strLocalPath == null) strLocalPath = "";
            if (!Directory.Exists(strLocalPath))
                throw new Exception(string.Format("The Local Upload folder:{0} did not exist.", ftpInfo.LocalFolder));

            string strRemotePath = this.GetRemoteRootFolder(ftpInfo.RemoteFolder);
            if (strRemotePath != "")
                try
                {
                    ftp.SetWorkingDirectory(strRemotePath);
                }
                catch
                {
                    throw new Exception(string.Format("The remote download folder:{0} is wrong or not existed in FTP server:{1}", strRemotePath, ftpInfo.Server));
                }

            string[] strFiles = Directory.GetFiles(strLocalPath);
            foreach (string file in strFiles)
            {
                //如果不是需要的文件后缀类型，就不处理
                if (!IsAuthExtension(file))
                    continue;

                FileInfo fileInfo = new FileInfo(file);
                string strFile = string.Format("Upload file {0},{1} to FTP:{2}", file, fileInfo.Length, ftpInfo.RemoteFolder);
                if (MessageChanged != null) MessageChanged(FtpMessageType.Message, strFile);
                System.Windows.Forms.Application.DoEvents();
                ftp.Upload(file);
                BackupFile(fileInfo);

            }
        }

    }

    class clsAPIFTP : FTPBase
    {
        public override event MessageChangedHandler MessageChanged = null;
        public bool doFTP(FtpLogin login)
        {
            bool bRet = true;
            ftpInfo = login;

            if (MessageChanged != null)
                MessageChanged(FtpMessageType.Start, string.Format("Start doFTP {1} for FTP server:{0}", ftpInfo.Server, ftpInfo.Direction));
            using (WinAPI.FtpConnection ftp = new WinAPI.FtpConnection(ftpInfo.Server, ftpInfo.Port,ftpInfo.UserName, ftpInfo.Password))
            {
                ftp.Passive = login.Passive;
                ftp.Open();
                ftp.Login();

                _Root = ftp.GetCurrentDirectory();
                //ftp.UseSsl = ftpInfo.UseSsl;
                try
                {
                    if (ftpInfo.Direction == FtpDirection.Download)
                    {
                        DownloadData(ftp, ftpInfo);
                    }
                    if (ftpInfo.Direction == FtpDirection.Upload)
                    {
                        UploadData(ftp, ftpInfo);
                    }

                    bRet = true;
                    if (MessageChanged != null)
                        MessageChanged(FtpMessageType.Success, string.Format("Success to {0} all files", ftpInfo.Direction));
                }
                catch (Exception ex)
                {
                    bRet = false;
                    if (MessageChanged != null)
                        MessageChanged(FtpMessageType.Fail, string.Format("Failed to {0} file, Reason:{1}", ftpInfo.Direction, ex.Message));
                }
                finally
                {
                    ftp.Close();
                }
            }
            if (MessageChanged != null)
                MessageChanged(FtpMessageType.End, string.Format("End doFTP {1} for FTP server:{0}", ftpInfo.Server, ftpInfo.Direction));
            return bRet;
        }

        private void DownloadData(WinAPI.FtpConnection ftp, FtpLogin ftpInfo)
        {

            string strLocalPath = ftpInfo.LocalFolder;
            if (strLocalPath == null) strLocalPath = "";
            if (!Directory.Exists(strLocalPath))
                Directory.CreateDirectory(strLocalPath);

            string strRemotePath = this.GetRemoteRootFolder(ftpInfo.RemoteFolder);
            if (strRemotePath != "")
                try
                {
                    ftp.SetCurrentDirectory(strRemotePath);
                }
                catch
                {
                    throw new Exception(string.Format("The remote download folder:{0} is wrong or not existed in FTP server:{1}", strRemotePath, ftpInfo.Server));
                }

            foreach (WinAPI.FtpFileInfo file in ftp.GetFiles())
            {
                //如果不是需要的文件后缀类型，就不处理
                if (!IsAuthExtension(file.Name))
                    continue;
                long fileLength = ftp.GetFileSize(file.Name);
                string strFile = string.Format("Download file {0},{1} from FTP:{2}", strLocalPath + "\\" + file.Name, fileLength, file.Name);
                if (MessageChanged != null) MessageChanged(FtpMessageType.Message, strFile);
                System.Windows.Forms.Application.DoEvents();
                ftp.GetFile(file.Name, strLocalPath + "\\" + file.Name, false);
                ftp.RemoveFile(file.Name);                
                //file.Delete();
            }

            //return Directory.GetFiles(strLocalPath);
        }
        /// <summary>
        /// Process to upload the file
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="ftpInfo"></param>
        /// <returns></returns>
        private void UploadData(WinAPI.FtpConnection ftp, FtpLogin ftpInfo)
        {
            string strLocalPath = ftpInfo.LocalFolder;
            if (strLocalPath == null) strLocalPath = "";
            if (!Directory.Exists(strLocalPath))
                throw new Exception(string.Format("The Local Upload folder:{0} did not exist.", ftpInfo.LocalFolder));

            string strRemotePath = this.GetRemoteRootFolder(ftpInfo.RemoteFolder);
            if (strRemotePath != "")
                try
                {
                    ftp.SetCurrentDirectory(strRemotePath);
                }
                catch
                {
                    throw new Exception(string.Format("The remote download folder:{0} is wrong or not existed in FTP server:{1}", strRemotePath, ftpInfo.Server));
                }

            string[] strFiles = Directory.GetFiles(strLocalPath);
            foreach (string file in strFiles)
            {
                //如果不是需要的文件后缀类型，就不处理
                if (!IsAuthExtension(file))
                    continue;

                FileInfo fileInfo = new FileInfo(file);
                string strFile = string.Format("Upload file {0},{1} to FTP:{2}", file, fileInfo.Length, ftpInfo.RemoteFolder);
                if (MessageChanged != null) MessageChanged(FtpMessageType.Message, strFile);
                System.Windows.Forms.Application.DoEvents();
                ftp.PutFile(file);
                BackupFile(fileInfo);
            }
        }

    }

    class clsWebFTP : FTPBase
    {
        public override event MessageChangedHandler MessageChanged = null;
        public bool doFTP(FtpLogin login)
        {
            ftpInfo = login;
            bool bRet = true;
            if (MessageChanged != null)
                MessageChanged(FtpMessageType.Start, string.Format("Start doFTP {1} for FTP server:{0}", ftpInfo.Server, ftpInfo.Direction));

            try
            {
                if (ftpInfo.Direction == FtpDirection.Download)
                {
                    DownloadData();
                }
                if (ftpInfo.Direction == FtpDirection.Upload)
                {
                    UploadData(ftpInfo);
                }

                bRet = true;
                if (MessageChanged != null)
                    MessageChanged(FtpMessageType.Success, string.Format("Success to {0} all files", ftpInfo.Direction));
            }
            catch (Exception ex)
            {
                bRet = false;
                if (MessageChanged != null)
                    MessageChanged(FtpMessageType.Fail, string.Format("Failed to {0} file, Reason:{1}", ftpInfo.Direction, ex.Message));
            }


            if (MessageChanged != null)
                MessageChanged(FtpMessageType.End, string.Format("End doFTP {1} for FTP server:{0}", ftpInfo.Server, ftpInfo.Direction));
            return bRet;
        }

        public string GetFTPURI()
        {
            string ftpURI = "";
            if (ftpInfo.Port == 21)
                ftpURI = string.Format("ftp://{0}/", ftpInfo.Server);
            else
                ftpURI = string.Format("ftp://{0}:{1}/", ftpInfo.Server, ftpInfo.Port);
            if (!string.IsNullOrEmpty(ftpInfo.RemoteFolder))
                ftpURI += string.Format("{0}/", ftpInfo.RemoteFolder);
            return ftpURI;
        }
        public FtpWebRequest Connect()
        {
            FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.GetFTPURI()));
            reqFTP.Credentials = new NetworkCredential(ftpInfo.UserName, ftpInfo.Password);
            reqFTP.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
            return reqFTP;
        }

        public void DisConnect()
        {
            FtpWebRequest reqFTP = Connect();
            reqFTP.KeepAlive = false;
        }


        private void DownloadData()
        {

            string strLocalPath = ftpInfo.LocalFolder;
            if (strLocalPath == null) strLocalPath = "";
            if (!Directory.Exists(strLocalPath))
                Directory.CreateDirectory(strLocalPath);



            //            foreach (FtpFile file in ftp.CurrentDirectory.Files)
            //            {
            //                string strFile = string.Format("Download file {0},{1}", file.FullName, file.Length);
            //                if (MessageChanged != null) MessageChanged(FtpMessageType.Message, strFile);
            //                System.Windows.Forms.Application.DoEvents();
            //                file.Download(strLocalPath + "/" + file.Name);
            //#if !DEBUG
            //                file.Delete();
            //#endif
            //            }

            //return Directory.GetFiles(strLocalPath);
        }
        /// <summary>
        /// Process to upload the file
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="ftpInfo"></param>
        /// <returns></returns>
        private void UploadData(FtpLogin ftpInfo)
        {
            string strLocalPath = ftpInfo.LocalFolder;
            if (strLocalPath == null) strLocalPath = "";
            if (!Directory.Exists(strLocalPath))
                throw new Exception(string.Format("The Local Upload folder:{0} did not exist.", ftpInfo.LocalFolder));

            string[] strFiles = Directory.GetFiles(strLocalPath);

            foreach (string file in strFiles)
            {
                string strFile = string.Format("Upload file {0}", file);
                if (MessageChanged != null) MessageChanged(FtpMessageType.Message, strFile);
                System.Windows.Forms.Application.DoEvents();
                FileInfo fileInfo = new FileInfo(strFile);
                Upload(fileInfo);

                BackupFile(fileInfo);
            }
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="filename"></param>
        private bool Upload(FileInfo fileInfo)
        {
            FtpWebRequest reqFTP = Connect();
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInfo.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInfo.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
