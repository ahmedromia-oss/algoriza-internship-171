using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IFileOperation
    {
        public string AddFile(IFormFile file , string path);
        public bool RemoveFile(string path);
        

    }
}
