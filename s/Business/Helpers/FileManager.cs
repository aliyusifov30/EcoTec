using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Helpers
{
	public class FileManager
	{
		public IWebHostEnvironment _webHostEnvironment;
		public FileManager(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		private static async Task<bool> CopyFileAsync(string path, IFormFile file)
		{
			try
			{
				await using FileStream stream = new(path, FileMode.Create);
				await file.CopyToAsync(stream);
				await stream.FlushAsync();
				return true;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public async Task<bool> DeleteAsync(string path, string fileName)
		{
			string pathDelete = _webHostEnvironment.WebRootPath + path +  fileName;
			File.Delete(pathDelete);
			return true;
		}

		public static bool HasFile(string path, string fileName)
		{
			return File.Exists($"{path}\\{fileName}");
		}

		public async Task<List<(string fileName, string pathOrContainerName)>> UploadRangeAsync(string path, IFormFileCollection files)
		{
			var uploadPath = _webHostEnvironment.WebRootPath + path;
			if (!Directory.Exists(uploadPath))
			{
				Directory.CreateDirectory(uploadPath);
			}

			List<(string fileName, string path)> datas = new();
			List<bool> results = new();

			string fileNewName;
			foreach (IFormFile file in files)
			{
				fileNewName = await FileRenameAsync(uploadPath, file.FileName);
				var result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
				datas.Add((fileNewName, $"{path}\\{fileNewName}"));
				results.Add(result);
			}
			if (results.TrueForAll(r => r.Equals(true)))
			{
				return datas;
			}
			return null;
		}

        public async Task<(string fileName, string pathOrContainerName)> UploadAsync(string path, IFormFile file)
        {
            var uploadPath = _webHostEnvironment.WebRootPath + path;
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> datas = new();
            List<bool> results = new();

            string fileNewName;
            fileNewName = await FileRenameAsync(uploadPath, file.FileName);
            var result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
  
            results.Add(result);
          
            return new (fileNewName, $"{path}\\{fileNewName}");
        }

        //todo must be refactor
        private static async Task<string> FileRenameAsync(string path, string fileName)
		{
			var result = Task.Run<string>(() =>
			{

				string extension = Path.GetExtension(fileName);

				string oldName = Path.GetFileNameWithoutExtension(fileName);

				Regex regex = new("[ *'\",+-._&#^@|/<>~]");

				string ceoFriendlyName = regex.Replace(oldName, "-");

				var files = Directory.GetFiles(path, ceoFriendlyName + "*");

				if (files.Length == 0) return ceoFriendlyName + "-1" + extension;

				int[] fileNumbers = new int[files.Length];

				int lastHyphenIndex;
				for (int i = 0; i < fileNumbers.Length; i++)
				{
					lastHyphenIndex = files[i].LastIndexOf("-");
					fileNumbers[i] = int.Parse(files[i].Substring(lastHyphenIndex + 1, files[i].Length - extension.Length - lastHyphenIndex - 1));
				}
				var biggestNumber = fileNumbers.Max();
				biggestNumber += 1;
				return ceoFriendlyName + "-" + biggestNumber + extension;
			});
			return await result;
		}
	}
}
