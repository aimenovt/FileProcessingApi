using FileProcessingAPI.Helper;
using Microsoft.AspNetCore.StaticFiles;

namespace FileProcessingAPI.Services
{
	public class ManageImage : IManageImage
	{
        public async Task<string> UploadFile(IFormFile _IFormFile)
		{
			string FileName = "";

			try
			{
				FileInfo fileInfo = new FileInfo(_IFormFile.FileName);
				FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + fileInfo.Extension;
				var _GetFilePath = Common.GetFilePath(FileName);

				using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
				{
					await _IFormFile.CopyToAsync(_FileStream);
				}

				return FileName;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<(byte[], string, string)> DownloadFile(string FileName)
		{
			try
			{
				var _GetFilePath = Common.GetFilePath(FileName);
				var provider = new FileExtensionContentTypeProvider();

				if (!provider.TryGetContentType(_GetFilePath, out var contentType))
				{
					contentType = "application/octet-stream";
				}

				var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
				return (_ReadAllBytesAsync, contentType, Path.GetFileName(_GetFilePath));
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
