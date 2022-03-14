using Brilliancy.Soccer.Common.Dtos.File;
using System.IO;

namespace Brilliancy.Soccer.Common.Contracts.Modules
{
    public interface IFileModule
    {
        FileDto AddPhoto(MemoryStream file, string fileExtension);
    }
}
