using FileProcessingAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessingAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FileManagerController : ControllerBase
	{
		private readonly IManageImage _manageImage;

		public FileManagerController(IManageImage manageImage)
        {
			_manageImage = manageImage;
		}

		[HttpPost]
		[Route("uploadfile")]
		public async Task<IActionResult> UploadFile(IFormFile _IFormFile)
		{
			var result = await _manageImage.UploadFile(_IFormFile);
			return Ok(result);
		}

		[HttpGet]
		[Route("downloadfile")]
		public async Task<IActionResult> DownloadFile(string FileName)
		{
			var result = await _manageImage.DownloadFile(FileName);
			return File(result.Item1, result.Item2, result.Item3);
		}

	}
}
