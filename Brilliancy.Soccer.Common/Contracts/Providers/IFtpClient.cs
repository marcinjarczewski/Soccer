using System.IO;

namespace Brilliancy.Soccer.Common.Contracts.Providers
{
    public interface IFtpClient
    {
        string UploadFile(Stream fileStream, string fileName);

        bool CreateNewDirectory(string path);
    }
}
