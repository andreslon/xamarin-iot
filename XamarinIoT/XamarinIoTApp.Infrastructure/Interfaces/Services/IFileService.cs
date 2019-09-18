using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace XamarinIoTApp.Infrastructure.Interfaces.Services
{
    public interface IFileService
    {
        string FolderPath { get; }

        Task DeleteFile(string fileName, string directoryName = null);
        Task DeleteFiles(string directoryName = null);
        Task<bool> FileExists(string fileName, string directoryName = null);
        Task<T> LoadFile<T>(string fileName, string directoryName = null);
        Task SaveFile<T>(string fileName, T content, string directoryName = null);
        Task<string> SaveFileFromBytes(byte[] bytes, string fileName, string directoryName = null);
        Task<string> GetFilePath(string fileName, string directoryName = null);
    }
}
