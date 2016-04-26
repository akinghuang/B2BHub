using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;
using System.IO;

namespace MW.DataHub.API.Net
{
    public class SFTP
    {
        private Session m_session;
        private Channel m_channel;
        private ChannelSftp m_sftp;

        //host:sftp地址   user：用户名   pwd：密码        
        public SFTP(string host, string user, string pwd)
        {
            string[] arr = host.Split(':');
            string ip = arr[0];
            int port = 22;
            if (arr.Length > 1) port = Int32.Parse(arr[1]);

            JSch jsch = new JSch();
            m_session = jsch.getSession(user, ip, port);
            MyUserInfo ui = new MyUserInfo();
            ui.setPassword(pwd);
            m_session.setUserInfo(ui);

        }
        public SFTP(string host, string user, string pwd,int port)
        {
            string[] arr = host.Split(':');
            string ip = arr[0];
            if (arr.Length > 1) port = Int32.Parse(arr[1]);

            JSch jsch = new JSch();
            m_session = jsch.getSession(user, ip, port);
            MyUserInfo ui = new MyUserInfo();
            ui.setPassword(pwd);
            m_session.setUserInfo(ui);

        }
        //SFTP连接状态        
        public bool Connected { get { return m_session.isConnected(); } }

        //连接SFTP        
        public bool Connect()
        {
            //try
            //{
                if (!Connected)
                {
                    m_session.connect();
                    m_channel = m_session.openChannel("sftp");
                    m_channel.connect();
                    m_sftp = (ChannelSftp)m_channel;
                }
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        //断开SFTP        
        public void Disconnect()
        {
            if (Connected)
            {
                m_channel.disconnect();
                m_session.disconnect();
            }
        }

        //SFTP存放文件        
        public bool Put(string localPath, string remotePath)
        {
            //try
            //{
                Tamir.SharpSsh.java.String src = new Tamir.SharpSsh.java.String(localPath);
                Tamir.SharpSsh.java.String dst = new Tamir.SharpSsh.java.String(remotePath);
                m_sftp.put(src, dst);
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        //SFTP获取文件        
        public bool Get(string remotePath, string localPath)
        {
            try
            {
                Tamir.SharpSsh.java.String src = new Tamir.SharpSsh.java.String(remotePath);
                Tamir.SharpSsh.java.String dst = new Tamir.SharpSsh.java.String(localPath);
                m_sftp.get(src, dst);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //删除SFTP文件
        public bool Delete(string remoteFile)
        {
            try
            {
                m_sftp.rm(remoteFile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //获取SFTP文件列表        
        public ArrayList GetFileList2(string remotePath, string fileType)
        {
            try
            {
                Tamir.SharpSsh.java.util.Vector vvv = m_sftp.ls(remotePath);
                ArrayList objList = new ArrayList();
                foreach (Tamir.SharpSsh.jsch.ChannelSftp.LsEntry qqq in vvv)
                {
                    string sss = qqq.getFilename();
                    if (fileType == "*.*")
                        objList.Add(sss);
                    else
                    {
                        if (sss.Length > (fileType.Length + 1) && fileType == "*"+sss.Substring(sss.Length - fileType.Length+1))
                        { objList.Add(sss); }
                        else { continue; }
                    }
                }

                return objList;
            }
            catch
            {
                return null;
            }
        }
        public List<String> GetFileList(string remotePath, string fileType)
        {
            try
            {
                Tamir.SharpSsh.java.util.Vector vvv = m_sftp.ls(remotePath);
                List<String> objList = new List<String>();
                foreach (Tamir.SharpSsh.jsch.ChannelSftp.LsEntry qqq in vvv)
                {
                    string sss = qqq.getFilename();
                    if (fileType == "*.*")
                        objList.Add(sss);
                    else
                    {
                        if (sss.Length > (fileType.Length + 1) && fileType == "*" + sss.Substring(sss.Length - fileType.Length + 1))
                        { objList.Add(sss); }
                        else { continue; }
                    }
                }

                return objList;
            }
            catch
            {
                return null;
            }
        }

        public void DownloadData(string localFolder, string fileExt, string remoteFolder)
        {

            string strLocalPath = localFolder;
            if (strLocalPath == null) strLocalPath = "";
            if (!Directory.Exists(strLocalPath))
                Directory.CreateDirectory(strLocalPath);

            string strRemotePath = remoteFolder;
            if (strRemotePath == null) strRemotePath = "";
            //if (strRemotePath != "")
            //    try
            //    {
            //        m_sftp.cd(new Tamir.SharpSsh.java.String(remoteFolder));
            //    }
            //    catch
            //    {
            //        throw new Exception(string.Format("The remote download folder:{0} is wrong or not existed in FTP server.", remoteFolder));
            //    }
            List<String> remoteFiles = this.GetFileList(strRemotePath, fileExt);
            foreach (String file in remoteFiles)
            {
                this.Get(remoteFolder + "\\" + file.ToString(), localFolder + "\\" +file.ToString());
                this.Delete(remoteFolder + "\\" + file.ToString());
            }
            
            //return Directory.GetFiles(strLocalPath);
        }
        /// <summary>
        /// Process to upload the file
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="ftpInfo"></param>
        /// <returns></returns>
        public void UploadData(string localFolder, string fileExt,string remoteFolder)
        {
            string strLocalPath = localFolder;
            if (strLocalPath == null) strLocalPath = "";
            if (!Directory.Exists(strLocalPath))
                throw new Exception(string.Format("The Local Upload folder:{0} did not exist.", strLocalPath));

            //string strRemotePath = remoteFolder;
            //if (strRemotePath == null) strRemotePath = "";
            //if (strRemotePath != "")
            //    try
            //    {
            //        m_sftp.cd(new Tamir.SharpSsh.java.String(remoteFolder));
            //    }
            //    catch
            //    {
            //        throw new Exception(string.Format("The remote download folder:{0} is wrong or not existed in FTP server.", strRemotePath));
            //    }

            string[] strFiles = Directory.GetFiles(strLocalPath,fileExt);
            foreach (string file in strFiles)
            {
                //如果不是需要的文件后缀类型，就不处理
                //if (!IsAuthExtension(file))
                //    continue;

                FileInfo fileInfo = new FileInfo(file);
                try
                {
                    this.Put(file, remoteFolder + "/" + fileInfo.Name);
                    
                }
                catch(Exception ex)
                {
                    throw new Exception("Upload to sFTP failed." + remoteFolder + "/" + fileInfo.Name+"."+ex.ToString(), ex.InnerException);
                    continue;
                }
                BackupFile(fileInfo);
                
            }
        }
        protected void BackupFile(FileInfo file)
        {

            //定义到具体的每天一个folder
            string strFileBakPath = string.Format("{0}\\{1}\\{2}\\"
                , file.Directory.FullName, "SentBackup", DateTime.Now.ToString("yyyyMMdd"));

            if (!Directory.Exists(strFileBakPath))
                Directory.CreateDirectory(strFileBakPath);
            strFileBakPath += file.Name;

            if (File.Exists(strFileBakPath))
                File.Delete(strFileBakPath);
            file.MoveTo(strFileBakPath);
            //file.Delete();
        }
        //登录验证信息        
        public class MyUserInfo : UserInfo
        {
            String passwd;
            public String getPassword() { return passwd; }
            public void setPassword(String passwd) { this.passwd = passwd; }

            public String getPassphrase() { return null; }
            public bool promptPassphrase(String message) { return true; }

            public bool promptPassword(String message) { return true; }
            public bool promptYesNo(String message) { return true; }
            public void showMessage(String message) { }
        }


    }
}
