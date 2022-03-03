using Brilliancy.Soccer.Common.Dtos.Team;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;

namespace Brilliancy.Soccer.Common.Dtos.Configuration
{
    public class ConfigurationDto
    {
        public string FTP_UploadDirRoot { get; set; }

        public string FTP_DownloadDirRoot { get; set; }

        public string FTP_SubfolderForImages { get; set; }

        public string FTP_Login { get; set; }

        public string FTP_Password { get; set; }
    }
}
