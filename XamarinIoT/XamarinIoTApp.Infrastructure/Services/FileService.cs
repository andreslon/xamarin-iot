using Newtonsoft.Json;
using XamarinIoTApp.Infrastructure.Interfaces;
using XamarinIoTApp.Infrastructure.Interfaces.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace XamarinIoTApp.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public string FolderPath => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public async Task SaveFile<T>(string fileName, T content, string directoryName = null)
        {
            await Task.Run(() =>
            {
                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                if (!string.IsNullOrEmpty(directoryName))
                {
                    var folders = directoryName.Split('/');

                    foreach (var item in folders)
                    {
                        documentsPath = Path.Combine(documentsPath, item);

                        if (!Directory.Exists(documentsPath))
                            Directory.CreateDirectory(documentsPath);
                    }
                }

                var filePath = Path.Combine(documentsPath, fileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                string result = JsonConvert.SerializeObject(content);
                File.WriteAllText(filePath, result);
            });
        }

        public async Task<T> LoadFile<T>(string fileName, string directoryName = null)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (await FileExists(fileName, directoryName))
                    {
                        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                        if (!string.IsNullOrEmpty(directoryName))
                        {
                            var folders = directoryName.Split('/');

                            foreach (var item in folders)
                            {
                                documentsPath = Path.Combine(documentsPath, item);

                                if (!Directory.Exists(documentsPath))
                                    Directory.CreateDirectory(documentsPath);
                            }
                        }

                        var filePath = Path.Combine(documentsPath, fileName);

                        string result = System.IO.File.ReadAllText(filePath);
                        if (result.Equals("{}") || string.IsNullOrEmpty(result.Trim()) || result.Equals("\"\""))
                        {
                            return default(T);
                        }
                        else
                        {
                            T serializedResponse = JsonConvert.DeserializeObject<T>(result);
                            return serializedResponse;
                        }
                    }
                    else
                        return default(T);
                });
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async Task<bool> FileExists(string fileName, string directoryName = null)
        {
            return await Task.Run(() =>
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                if (!string.IsNullOrEmpty(directoryName))
                {
                    var folders = directoryName.Split('/');

                    foreach (var item in folders)
                    {
                        documentsPath = Path.Combine(documentsPath, item);

                        if (!Directory.Exists(documentsPath))
                            return false;
                    }
                }

                var filename = Path.Combine(documentsPath, fileName);
                return File.Exists(filename);
            });
        }
        public async Task<string> GetFilePath(string fileName, string directoryName = null)
        {
            return await Task.Run(() =>
            {
                string filePath;
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                if (!string.IsNullOrEmpty(directoryName))
                {
                    var folders = directoryName.Split('/');

                    foreach (var item in folders)
                    {
                        documentsPath = Path.Combine(documentsPath, item);
                    }
                    filePath = Path.Combine(documentsPath, fileName);
                    return filePath;
                }
                filePath = Path.Combine(documentsPath, fileName);
                return filePath;
            });
        }
        public async Task DeleteFiles(string directoryName = null)
        {
            await Task.Run(() =>
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                if (!string.IsNullOrEmpty(directoryName))
                {
                    var folders = directoryName.Split('/');

                    foreach (var item in folders)
                    {
                        documentsPath = Path.Combine(documentsPath, item);

                        if (!Directory.Exists(documentsPath))
                            return;
                    }
                }

                var files = Directory.GetFiles(documentsPath);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }
            });
        }

        public async Task DeleteFile(string fileName, string directoryName = null)
        {
            await Task.Run(() =>
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                if (!string.IsNullOrEmpty(directoryName))
                {
                    var folders = directoryName.Split('/');

                    foreach (var item in folders)
                    {
                        documentsPath = Path.Combine(documentsPath, item);

                        if (!Directory.Exists(documentsPath))
                            return;
                    }
                }

                var filePath = Path.Combine(documentsPath, fileName);

                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
            });
        }

        public async Task<string> SaveFileFromBytes(byte[] bytes, string fileName, string directoryName = null)
        {
            return await Task.Run(() =>
            {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                if (directoryName != null)
                {
                    var folders = directoryName.Split('/').ToList();

                    foreach (var folderName in folders)
                    {
                        if (folderName != "")
                        {
                            var documentsPath = Path.Combine(folder, folderName);

                            if (!Directory.Exists(documentsPath))
                            {
                                Directory.CreateDirectory(documentsPath);
                            }

                            folder = Path.Combine(folder, folderName);
                        }
                    }
                }

                var filePath = Path.Combine(folder, fileName);
                File.WriteAllBytes(filePath, bytes);

                return filePath;
            });
        }
    }
}
