using Core.Services;
using System;

namespace Vezeeta.Helpers
{
    public class FileOperation : IFileOperation
    {
        private readonly IWebHostEnvironment environment;

        public FileOperation(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public string AddFile(IFormFile file, string path)
        {

            string UploadsFolder = Path.Combine(environment.WebRootPath, path);
            string newFile = CreateUniqueName(fileName: file.FileName);
            string filepath = Path.Combine(UploadsFolder,newFile);
            file.CopyTo(new FileStream(filepath, FileMode.Create));
            return newFile;

        }
        private string CreateUniqueName(string fileName)
        {
            fileName = Guid.NewGuid().ToString() + "_" + fileName;
            return fileName;
        }


        public bool RemoveFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
