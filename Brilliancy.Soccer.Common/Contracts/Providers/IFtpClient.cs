using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.File;
using Brilliancy.Soccer.Common.Dtos.User;
using Brilliancy.Soccer.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Brilliancy.Soccer.Common.Contracts.Providers
{
    public interface IFtpClient
    {
        string UploadFile(Stream fileStream, string fileName);

        bool CreateNewDirectory(string path);
    }
}
