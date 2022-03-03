using Brilliancy.Soccer.Common.Dtos.Authentication;
using Brilliancy.Soccer.Common.Dtos.File;
using Brilliancy.Soccer.Common.Dtos.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface IFileModule
    {
        FileDto AddPhoto(MemoryStream file, string fileExtension);
    }
}
