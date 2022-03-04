using Brilliancy.Soccer.Common.Dtos.Configuration;
using Brilliancy.Soccer.Common.Exceptions;
using System;
using System.IO;
using System.Net;

namespace Brilliancy.Soccer.Common.Helpers
{

    public class FtpClient
    {
        #region Properties
        public string _ftpUploadRoot { get;  }

        public string _ftpDownloadRoot { get; }

        public string _ftpPhotoSubfolder { get; }

        public string _ftpUser { get; }

        public string _ftpPassword { get; }

        #endregion Properties

        public FtpClient(ConfigurationDto configuration)
        {
            if (configuration == null)
            {
                throw new Exception(Common.Translations.CommonTranslations.FtpClient_NoConfig);
            }

            this._ftpUploadRoot = configuration.FTP_UploadDirRoot;
            this._ftpDownloadRoot = configuration.FTP_DownloadDirRoot;
            this._ftpPhotoSubfolder = configuration.FTP_SubfolderForImages;
            this._ftpUser = configuration.FTP_Login;
            this._ftpPassword = configuration.FTP_Password;
        }

        public string UploadFile(Stream fileStream, string fileName)
        {   
            try
            {             
                this.UploadFile(fileStream, _ftpPhotoSubfolder, fileName);
            }
            catch (Exception ex)
            {
                throw new UserDataException("a");
            }

            return Path.Combine(this._ftpDownloadRoot, _ftpPhotoSubfolder, fileName);
        }

        private string UploadFile(Stream fileStream, string dir, string fileName)
        {
            if (fileStream != null)
            {
                var uri = new Uri(_ftpUploadRoot + dir + fileName);
                var request = (FtpWebRequest)FtpWebRequest.Create(uri);

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                request.UseBinary = true;
                request.KeepAlive = false;
                request.ReadWriteTimeout = 10000;

                using (Stream requestStream = request.GetRequestStream())
                {
                    fileStream.CopyTo(requestStream);
                    requestStream.Close();
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return dir + fileName;
                }
            }

            return null;
        }

        private bool CreateNewDirectory(string path)
        {
            try
            {
                var uri = new Uri(_ftpUploadRoot + path);

                var request = (FtpWebRequest)FtpWebRequest.Create(uri);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                request.UseBinary = true;
                request.KeepAlive = false;

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    var status = response.StatusCode;

                    return status == FtpStatusCode.PathnameCreated
                        || status == FtpStatusCode.FileActionOK;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;

                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
