using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MW.DataHub.API.Net.WinAPI
{
    /// <summary>
    /// The <c>FtpConnection</c> class provides the ability to connect to FTP servers.
    /// </summary>
    public class FtpConnection : IDisposable
    {
        private bool _Passive = true;
        public bool Passive { get { return _Passive; } set { _Passive = value; } }




        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect to.</param>
        public FtpConnection(string host)
        {
            _host = host;
        }

        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect.</param>
        /// <param name="port">An <see cref="Int32"/> type representing the port on which to connect.</param>
        public FtpConnection(string host, int port)
        {
            _host = host;
            _port = port;
        }

        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect.</param>
        /// <param name="username">A <see cref="String"/> type representing the username with which to authenticate.</param>
        /// <param name="password">A <see cref="String"/> type representing the password with which to authenticate.</param>
        public FtpConnection(string host, string username, string password)
        {
            _host = host;
            _username = username;
            _password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect.</param>
        /// <param name="port">An <see cref="Int32"/> type representing the port on which to connect.</param>
        /// <param name="username">A <see cref="String"/> type representing the username with which to authenticate.</param>
        /// <param name="password">A <see cref="String"/> type representing the password with which to authenticate.</param>
        public FtpConnection(string host, int port, string username, string password)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
        }

        /// <summary>
        /// Establishes a connection to the host.
        /// </summary>
        /// <exception cref="ArgumentNullException">If Host is null or empty.</exception>
        public void Open()
        {
            if(String.IsNullOrEmpty(_host)) throw new ArgumentNullException("Host");

            _hInternet = WININET.InternetOpen(
                System.Environment.UserName,
                WININET.INTERNET_OPEN_TYPE_PRECONFIG,
                null,
                null,
                WININET.INTERNET_FLAG_SYNC);

            if (_hInternet == IntPtr.Zero)
            {
                Error();
            }
        }

        /// <summary>
        /// Logs into the host server using the credentials provided when the class was instantiated.
        /// </summary>
        public void Login()
        {
            Login(_username, _password);
        }

        /// <summary>
        /// Logs into the host server using the provided credentials.
        /// </summary>
        /// <exception cref="ArgumentNullException">If <paramref name="username"/> or <paramref name="password"/> are null.</exception>
        /// <param name="username">A <see cref="String" /> type representing the user name with which to authenticate.</param>
        /// <param name="password">A <see cref="String" /> type representing the password with which to authenticate.</param>
        public void Login(string username, string password)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (password == null) throw new ArgumentNullException("password");
            {
                
            }
            _hConnect = WININET.InternetConnect(_hInternet
                , _host, _port
                , username, password
                , WININET.INTERNET_SERVICE_FTP
                , Passive ? WININET.INTERNET_FLAG_PASSIVE : WININET.INTERNET_FLAG_PORT
                , IntPtr.Zero);

            if (_hConnect == IntPtr.Zero)
            {
                Error();
            }
        }

        /// <summary>
        /// Changes the current FTP working directory to the specified path.
        /// </summary>
        /// <exception cref="FtpException">If the directory does not exist on the FTP server.</exception>
        /// <param name="directory">A <see cref="String"/> representing the file path of the directory.</param>
        public void SetCurrentDirectory(string directory)
        {
            int ret = WININET.FtpSetCurrentDirectory(
                _hConnect,
                directory);

            if (ret == 0)
            {
                Error();
            }
        }

        /// <summary>
        /// Changes the local working directory to the specified path.
        /// </summary>
        /// <exception cref="InvalidDataException">If the directory does not exist on the local system.</exception>
        /// <param name="directory"></param>
        public void SetLocalDirectory(string directory)
        {
            if(Directory.Exists(directory))
                System.Environment.CurrentDirectory = directory;
            else
                throw new InvalidDataException(String.Format("{0} is not a directory!", directory));
        }

        /// <summary>
        /// Gets the current working FTP directory
        /// </summary>
        /// <returns>A <see cref="String"> representing the current working directory.</see></returns>
        public string GetCurrentDirectory()
        {
            int buffLength = WINAPI.MAX_PATH + 1;
            StringBuilder str = new StringBuilder(buffLength);
            int ret = WININET.FtpGetCurrentDirectory(_hConnect, str, ref buffLength);

            if (ret == 0)
            {
                Error();
                return null;
            }

            return str.ToString();
        }

        /// <summary>
        /// Get the current FtpDirectory information for the current working directory
        /// </summary>
        /// <returns>A <see cref="FtpDirectoryInfo"/> with available details about the current working directory.</returns>
        public FtpDirectoryInfo GetCurrentDirectoryInfo()
        {
            string dir = GetCurrentDirectory();
            return new FtpDirectoryInfo(this, dir);
        }

        /// <summary>
        /// Gets the specified file's size
        /// </summary>
        /// <param name="file">The file to get the size for</param>
        /// <returns>The file size in bytes</returns>
        public long GetFileSize(string file)
        {
            IntPtr hFile = new IntPtr(
                WININET.FtpOpenFile(_hConnect, file, WINAPI.GENERIC_READ, WININET.FTP_TRANSFER_TYPE_BINARY, IntPtr.Zero)
            );

            if(hFile == IntPtr.Zero)
            {
                Error();
            }
            else
            {
                try
                {
                    int sizeHigh = 0;
                    int sizeLo = WININET.FtpGetFileSize(hFile, ref sizeHigh);

                    long fileSize = ((long)sizeHigh << 32) | sizeLo;

                    return fileSize;
                }
                catch (Exception)
                {
                    Error();
                    
                }
                finally
                {
                    WININET.InternetCloseHandle(hFile);
                }
            }

            return 0;
        }

        /// <summary>
        /// Downloads a file from the FTP server to the local system
        /// </summary>
        /// <remarks>The file will be downloaded to the local working directory with the same name it has on the FTP server.</remarks>
        /// <exception cref="FtpException">If the file does not exist.</exception>
        /// <param name="remoteFile">A <see cref="String"/> representing the full or relative path to the file to download.</param>
        /// <param name="failIfExists">A <see cref="Boolean"/> that determines whether an existing local file should be overwritten.</param>
        public void GetFile(string remoteFile, bool failIfExists)
        {
            GetFile(remoteFile, remoteFile, failIfExists);
        }

        /// <summary>
        /// Downloads a file from the FTP server to the local system
        /// </summary>
        /// <exception cref="FtpException">If the file does not exist.</exception>
        /// <param name="remoteFile">A <see cref="String"/> representing the full or relative path to the file to download.</param>
        /// <param name="localFile">A <see cref="String"/> representing the local file path to save the file.</param>
        /// <param name="failIfExists">A <see cref="Boolean"/> that determines whether an existing local file should be overwritten.</param>
        public void GetFile(string remoteFile, string localFile, bool failIfExists)
        {
            int ret = WININET.FtpGetFile(_hConnect,
                 remoteFile,
                 localFile,
                 failIfExists,
                 WINAPI.FILE_ATTRIBUTE_NORMAL,
                 WININET.FTP_TRANSFER_TYPE_BINARY,
                 IntPtr.Zero);

            if (ret == 0)
            {
                Error();
            }
        }

        /// <summary>
        /// Uploads a file to the FTP server
        /// </summary>
        /// <param name="fileName">A <see cref="String"> representing the local file path to upload.</see></param>
        public void PutFile(string fileName)
        {
            PutFile(fileName, Path.GetFileName(fileName));
        }

        /// <summary>
        /// Uploads a file to the FTP server
        /// </summary>
        /// <param name="fileName">A <see cref="String"/> representing the local file path to upload.</param>
        /// <param name="localFile">A <see cref="String"/> representing the file path to save the file.</param>
        public void PutFile(string localFile, string remoteFile)
        {
            int ret = WININET.FtpPutFile(_hConnect,
                localFile,
                remoteFile,
                WININET.FTP_TRANSFER_TYPE_BINARY,
                IntPtr.Zero);

            if (ret == 0)
            {
                Error();
            }
        }

        /// <summary>
        /// Renames a file on the FTP server
        /// </summary>
        /// <param name="existingFile">A <see cref="String"/> representing the current file name</param>
        /// <param name="newFile">A <see cref="String"/> representing the new file name</param>
        public void RenameFile(string existingFile, string newFile)
        {
            int ret = WININET.FtpRenameFile(_hConnect, existingFile, newFile);

            if (ret == 0)
                Error();
        }

        /// <summary>
        /// Deletes a file from the FTP server
        /// </summary>
        /// <param name="fileName">A <see cref="String"/> representing the path of the file to delete.</param>
        public void RemoveFile(string fileName)
        {
            int ret = WININET.FtpDeleteFile(_hConnect, fileName);

            if (ret == 0)
            {
                Error();
            }
        }
        
        /// <summary>
        /// Deletes a directory from the FTP server
        /// </summary>
        /// <param name="directory">A <see cref="String"/> representing the path of the directory to delete.</param>
        public void RemoveDirectory(string directory)
        {
            int ret = WININET.FtpRemoveDirectory(_hConnect, directory);
            if (ret == 0)
                Error();
        } 

        /// <summary>
        /// Gets details of all files and their available FTP file information from the current working FTP directory.
        /// </summary>
        /// <returns>A <see cref="FtpFileInfo[]"/> representing the files in the current working directory.</returns>
        public FtpFileInfo[] GetFiles()
        {
            return GetFiles(GetCurrentDirectory()); 
        }

        /// <summary>
        /// Gets details of all files and their available FTP file information from the current working FTP directory that match the file mask.
        /// </summary>
        /// <param name="mask">A <see cref="String"/> representing the file mask to match files.</param>
        /// <returns>A <see cref="FtpFileInfo[]"/> representing the files in the current working directory.</returns>
        public FtpFileInfo[] GetFiles(string mask) 
        {
            WINAPI.WIN32_FIND_DATA findData = new WINAPI.WIN32_FIND_DATA();

            IntPtr hFindFile = WININET.FtpFindFirstFile(
                _hConnect,
                mask,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                List<FtpFileInfo> files = new List<FtpFileInfo>();
                if (hFindFile == IntPtr.Zero)
                {
                    if (Marshal.GetLastWin32Error() == WINAPI.ERROR_NO_MORE_FILES)
                    {
                        return files.ToArray();
                    }
                    else
                    {
                        Error();
                        return files.ToArray();
                    }
                }

                if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) != WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                {
                    FtpFileInfo file = new FtpFileInfo(this, new string(findData.fileName).TrimEnd('\0'));
                    file.LastAccessTime = findData.ftLastAccessTime.ToDateTime();
                    file.LastWriteTime = findData.ftLastWriteTime.ToDateTime();
                    file.CreationTime = findData.ftCreationTime.ToDateTime();
                    file.Attributes = (FileAttributes)findData.dfFileAttributes;
                    files.Add(file);
                }

                findData = new WINAPI.WIN32_FIND_DATA();
                while (WININET.InternetFindNextFile(hFindFile, ref findData) != 0)
                {
                    if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) != WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                    {
                        FtpFileInfo file = new FtpFileInfo(this, new string(findData.fileName).TrimEnd('\0'));
                        file.LastAccessTime = findData.ftLastAccessTime.ToDateTime();
                        file.LastWriteTime = findData.ftLastWriteTime.ToDateTime();
                        file.CreationTime = findData.ftCreationTime.ToDateTime();
                        file.Attributes = (FileAttributes)findData.dfFileAttributes;
                        files.Add(file);
                    }

                    findData = new WINAPI.WIN32_FIND_DATA();
                }

                if (Marshal.GetLastWin32Error() != WINAPI.ERROR_NO_MORE_FILES)
                    Error();

                return files.ToArray();
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }
        }

        /// <summary>
        /// Gets details of all directories and their available FTP directory information from the current working FTP directory.
        /// </summary>
        /// <returns>A <see cref="FtpDirectoryInfo[]"/> representing the directories in the current working directory.</returns>
        public FtpDirectoryInfo[] GetDirectories()
        {
            return GetDirectories(this.GetCurrentDirectory());
        }

        /// <summary>
        /// Gets details of all directories and their available FTP directory information from the current working FTP directory that match the directory mask.
        /// </summary>
        /// <returns>A <see cref="FtpDirectoryInfo[]"/> representing the directories in the current working directory that match the mask.</returns>
        public FtpDirectoryInfo[] GetDirectories(string path) 
        {
            WINAPI.WIN32_FIND_DATA findData = new WINAPI.WIN32_FIND_DATA();
            
            IntPtr hFindFile = WININET.FtpFindFirstFile(
                _hConnect,
                path,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                List<FtpDirectoryInfo> directories = new List<FtpDirectoryInfo>();

                if (hFindFile == IntPtr.Zero)
                {
                    if (Marshal.GetLastWin32Error() == WINAPI.ERROR_NO_MORE_FILES)
                    {
                        return directories.ToArray();
                    }
                    else
                    {
                        Error();
                        return directories.ToArray();
                    }
                }

                if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) == WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                {
                    FtpDirectoryInfo dir = new FtpDirectoryInfo(this, new string(findData.fileName).TrimEnd('\0'));
                    dir.LastAccessTime = findData.ftLastAccessTime.ToDateTime();
                    dir.LastWriteTime = findData.ftLastWriteTime.ToDateTime();
                    dir.CreationTime = findData.ftCreationTime.ToDateTime();
                    dir.Attributes = (FileAttributes)findData.dfFileAttributes;
                    directories.Add(dir);
                }

                findData = new WINAPI.WIN32_FIND_DATA();

                while (WININET.InternetFindNextFile(hFindFile, ref findData) != 0)
                {
                    if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) == WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                    {
                        FtpDirectoryInfo dir = new FtpDirectoryInfo(this, new string(findData.fileName).TrimEnd('\0'));
                        dir.LastAccessTime = findData.ftLastAccessTime.ToDateTime();
                        dir.LastWriteTime = findData.ftLastWriteTime.ToDateTime();
                        dir.CreationTime = findData.ftCreationTime.ToDateTime();
                        dir.Attributes = (FileAttributes)findData.dfFileAttributes;
                        directories.Add(dir);
                    }

                    findData = new WINAPI.WIN32_FIND_DATA();
                }

                if (Marshal.GetLastWin32Error() != WINAPI.ERROR_NO_MORE_FILES)
                    Error();

                return directories.ToArray();
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }
        }

        /// <summary>
        /// Creates a directory on the FTP server.
        /// </summary>
        /// <param name="path">A <see cref="String"/> representing the full or relative path of the directory to create.</param>
        public void CreateDirectory(string path)
        {
            if (WININET.FtpCreateDirectory(_hConnect, path) == 0)
            {
                Error();
            }
        }

        /// <summary>
        /// Checks if a directory exists.
        /// </summary>
        /// <param name="path">A <see cref="String"/> representing the path to check.</param>
        /// <returns>A <see cref="Boolean"/> indicating whether the directory exists.</returns>
        public bool DirectoryExists(string path)
        {
            WINAPI.WIN32_FIND_DATA findData = new WINAPI.WIN32_FIND_DATA();

            IntPtr hFindFile = WININET.FtpFindFirstFile(
                _hConnect,
                path,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                if (hFindFile == IntPtr.Zero)
                {
                    return false;
                }

                return true;
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }

        }

        /// <summary>
        /// Checks if a file exists.
        /// </summary>
        /// <param name="path">A <see cref="String"/> representing the path to check.</param>
        /// <returns>A <see cref="Boolean"/> indicating whether the file exists.</returns>
        public bool FileExists(string path)
        {
            WINAPI.WIN32_FIND_DATA findData = new WINAPI.WIN32_FIND_DATA();

            IntPtr hFindFile = WININET.FtpFindFirstFile(
                _hConnect,
                path,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                if (hFindFile == IntPtr.Zero)
                {
                    return false;
                }

                return true;
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }
        }

        /// <summary>
        /// Sends a command to the FTP server
        /// </summary>
        /// <param name="cmd">A <see cref="String"/> representing the command to send.</param>
        /// <returns>A <see cref="String"/> containing the server response.</returns>
        public string SendCommand(string cmd)
        {
            int result;
            IntPtr dataSocket = new IntPtr();
            switch(cmd)
            {
                case "PASV":
                    result = WININET.FtpCommand(_hConnect, false, WININET.FTP_TRANSFER_TYPE_ASCII, cmd, IntPtr.Zero, ref dataSocket);
                    break;
                default:
                    result = WININET.FtpCommand(_hConnect, false, WININET.FTP_TRANSFER_TYPE_ASCII, cmd, IntPtr.Zero, ref dataSocket);
                    break;
            }

            int BUFFER_SIZE = 8192;

            if(result == 0){
                Error();
            }
            else if(dataSocket != IntPtr.Zero)
            {
                StringBuilder buffer = new StringBuilder(BUFFER_SIZE);
                int bytesRead = 0;

                do
                {
                    result = WININET.InternetReadFile(dataSocket, buffer, BUFFER_SIZE, ref bytesRead);
                } while (result == 1 && bytesRead > 1);

                return buffer.ToString();
                
            }

            return "";
        }

        /// <summary>
        /// Closes the current FTP connection
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Retrieves error message text
        /// </summary>
        /// <param name="code">A <see cref="Int32"/> representing the system error code.</param>
        /// <returns>A <see cref="String"/> containing the error text.</returns>
        private string InternetLastResponseInfo(ref int code)
        {
            int BUFFER_SIZE = 8192;

            StringBuilder buff = new StringBuilder(BUFFER_SIZE);
            WININET.InternetGetLastResponseInfo(ref code, buff, ref BUFFER_SIZE);
            return buff.ToString();
        }

        /// <summary>
        /// Retrieves error text and throws an Exception.
        /// </summary>
        private void Error()
        {
            int code = Marshal.GetLastWin32Error();
            string errorText = "";
            switch (code)
            {
                #region Error Reason
                case 12001://     ERROR_INTERNET_OUT_OF_HANDLES
                    errorText = "No more handles could be generated at this time."; break;
                case 12002://     ERROR_INTERNET_TIMEOUT
                    errorText = "The request has timed out."; break;
                case 12003://     ERROR_INTERNET_EXTENDED_ERROR
                    errorText = InternetLastResponseInfo(ref code); break;
                case 12004://     ERROR_INTERNET_INTERNAL_ERROR
                    errorText = "An internal error has occurred."; break;
                case 12005://     ERROR_INTERNET_INVALID_URL
                    errorText = "The URL is invalid."; break;
                case 12006://     ERROR_INTERNET_UNRECOGNIZED_SCHEME
                    errorText = "The URL scheme could not be recognized or is not supported."; break;
                case 12007://     ERROR_INTERNET_NAME_NOT_RESOLVED
                    errorText = "The server name could not be resolved."; break;
                case 12008://     ERROR_INTERNET_PROTOCOL_NOT_FOUND
                    errorText = "The requested protocol could not be located."; break;
                case 12009://     ERROR_INTERNET_INVALID_OPTION
                    errorText = "A request to InternetQueryOption or InternetSetOption specified an invalid option value."; break;
                case 12010://     ERROR_INTERNET_BAD_OPTION_LENGTH
                    errorText = "The length of an option supplied to InternetQueryOption or InternetSetOption is incorrect for the type of option specified."; break;
                case 12011://     ERROR_INTERNET_OPTION_NOT_SETTABLE
                    errorText = "The request option cannot be set, only queried."; break;
                case 12012://     ERROR_INTERNET_SHUTDOWN
                    errorText = "The Win32 Internet function support is being shut down or unloaded."; break;
                case 12013://     ERROR_INTERNET_INCORRECT_USER_NAME
                    errorText = "The request to connect and log on to an FTP server could not be completed because the supplied user name is incorrect."; break;
                case 12014://     ERROR_INTERNET_INCORRECT_PASSWORD
                    errorText = "The request to connect and log on to an FTP server could not be completed because the supplied password is incorrect."; break;
                case 12015://     ERROR_INTERNET_LOGIN_FAILURE
                    errorText = "The request to connect to and log on to an FTP server failed."; break;
                case 12016://     ERROR_INTERNET_INVALID_OPERATION
                    errorText = "The requested operation is invalid."; break;
                case 12017://     ERROR_INTERNET_OPERATION_CANCELLED
                    errorText = "The operation was canceled, usually because the handle on which the request was operating was closed before the operation completed."; break;
                case 12018://     ERROR_INTERNET_INCORRECT_HANDLE_TYPE
                    errorText = "The type of handle supplied is incorrect for this operation."; break;
                case 12019://     ERROR_INTERNET_INCORRECT_HANDLE_STATE
                    errorText = "The requested operation cannot be carried out because the handle supplied is not in the correct state."; break;
                case 12020://     ERROR_INTERNET_NOT_PROXY_REQUEST
                    errorText = "The request cannot be made via a proxy."; break;
                case 12021://     ERROR_INTERNET_REGISTRY_VALUE_NOT_FOUND
                    errorText = "A required registry value could not be located."; break;
                case 12022://     ERROR_INTERNET_BAD_REGISTRY_PARAMETER
                    errorText = "A required registry value was located but is an incorrect type or has an invalid value."; break;
                case 12023://     ERROR_INTERNET_NO_DIRECT_ACCESS
                    errorText = "Direct network access cannot be made at this time."; break;
                case 12024://     ERROR_INTERNET_NO_CONTEXT
                    errorText = "An asynchronous request could not be made because a zero context value was supplied."; break;
                case 12025://     ERROR_INTERNET_NO_CALLBACK
                    errorText = "An asynchronous request could not be made because a callback function has not been set."; break;
                case 12026://     ERROR_INTERNET_REQUEST_PENDING
                    errorText = "The required operation could not be completed because one or more requests are pending."; break;
                case 12027://     ERROR_INTERNET_INCORRECT_FORMAT
                    errorText = "The format of the request is invalid."; break;
                case 12028://     ERROR_INTERNET_ITEM_NOT_FOUND
                    errorText = "The requested item could not be located."; break;
                case 12029://     ERROR_INTERNET_CANNOT_CONNECT
                    errorText = "The attempt to connect to the server failed."; break;
                case 12030://     ERROR_INTERNET_CONNECTION_ABORTED
                    errorText = "The connection with the server has been terminated."; break;
                case 12031://     ERROR_INTERNET_CONNECTION_RESET
                    errorText = "The connection with the server has been reset."; break;
                case 12032://     ERROR_INTERNET_FORCE_RETRY
                    errorText = "Calls for the Win32 Internet function to redo the request."; break;
                case 12033://     ERROR_INTERNET_INVALID_PROXY_REQUEST
                    errorText = "The request to the proxy was invalid."; break;
                case 12036://     ERROR_INTERNET_HANDLE_EXISTS
                    errorText = "The request failed because the handle already exists."; break;
                case 12037://     ERROR_INTERNET_SEC_CERT_DATE_INVALID
                    errorText = "SSL certificate date that was received from the server is bad. The certificate is expired."; break;
                case 12038://     ERROR_INTERNET_SEC_CERT_CN_INVALID
                    errorText = "SSL certificate common name (host name field) is incorrect. For example, if you entered www.server.com and the common name on the certificate says www.different.com."; break;
                case 12039://     ERROR_INTERNET_HTTP_TO_HTTPS_ON_REDIR
                    errorText = "The application is moving from a non-SSL to an SSL connection because of a redirect."; break;
                case 12040://     ERROR_INTERNET_HTTPS_TO_HTTP_ON_REDIR
                    errorText = "The application is moving from an SSL to an non-SSL connection because of a redirect."; break;
                case 12041://     ERROR_INTERNET_MIXED_SECURITY
                    errorText = "Indicates that the content is not entirely secure. Some of the content being viewed may have come from unsecured servers."; break;
                case 12042://     ERROR_INTERNET_CHG_POST_IS_NON_SECURE
                    errorText = "The application is posting and attempting to change multiple lines of text on a server that is not secure."; break;
                case 12043://     ERROR_INTERNET_POST_IS_NON_SECURE
                    errorText = "The application is posting data to a server that is not secure."; break;
                case 12110://     ERROR_FTP_TRANSFER_IN_PROGRESS
                    errorText = "The requested operation cannot be made on the FTP session handle because an operation is already in progress."; break;
                case 12111://     ERROR_FTP_DropPED
                    errorText = "The FTP operation was not completed because the session was aborted."; break;
                default:
                    throw new Win32Exception(code);
                #endregion Error Reason
            }
            throw new FtpException(code, errorText);
        }

        /// <summary>
        /// The Port used to connect
        /// </summary>
        public int Port
        {
            get { return _port; }
        }

        /// <summary>
        /// The host used to connect.
        /// </summary>
        public string Host
        {
            get { return _host; }
        }

        private IntPtr _hInternet;
        private IntPtr _hConnect;

        private string _host;
        private string _username = "";
        private string _password = "";
        private int _port = WININET.INTERNET_DEFAULT_FTP_PORT;

        private bool _disposed = false;

        #region IDisposable Members

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_hConnect != IntPtr.Zero)
                    WININET.InternetCloseHandle(_hConnect);

                if (_hInternet != IntPtr.Zero)
                    WININET.InternetCloseHandle(_hInternet);

                _hInternet = IntPtr.Zero;
                _hConnect = IntPtr.Zero;

                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        ~FtpConnection()
        {
            Dispose();
        }
    }
}
