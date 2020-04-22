﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SeeSharpLMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SeeSharpLMS.Services
{
	public class UnitOfWork : IUnitOfWork
	{
		[Obsolete]
		private IHostingEnvironment _hostingEnvironment;
		public UnitOfWork(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}


		public async void UploadImage(IFormFile file)
		{
			long totalBytes = file.Length;
			string filename = file.FileName.Trim('"');
			filename = EnsureFileName(filename);
			byte[] buffer = new byte[16 * 1024];
			using (FileStream output = File.Create(GetpathAndFileName(filename)))
			{
				using (Stream input = file.OpenReadStream())
				{
					int readBytes;
					try
					{
						while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
						{

							await output.WriteAsync(buffer, 0, readBytes);
							totalBytes += readBytes;
						}
					}
					catch 
					{
						Console.WriteLine("Not valid");
					}
				}
			}
		}

		public async void UploadFile(IFormFile file)
		{
			long totalBytes = file.Length;
			string filename = file.FileName.Trim('"');
			filename = EnsureFileName(filename);
			byte[] buffer = new byte[16 * 1024];
			using (FileStream output = File.Create(GetpathAndFileName_Assignment(filename)))
			{
				using (Stream input = file.OpenReadStream())
				{
					int readBytes;
					try
					{
						while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
						{

							await output.WriteAsync(buffer, 0, readBytes);
							totalBytes += readBytes;
						}
					}
					catch
					{
						Console.WriteLine("Not valid");
					}
				}
			}
		}

		private string GetpathAndFileName(string filename)
		{
			

			string path = _hostingEnvironment.WebRootPath + "\\uploads\\";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			return path + filename;
		}

		private string GetpathAndFileName_Assignment(string filename)
		{


			string path = _hostingEnvironment.WebRootPath + "\\FileUploads\\";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			return path + filename;
		}


		private string EnsureFileName(string filename)
		{
			if (filename.Contains("\\"))
			{
				filename = filename.Substring(filename.LastIndexOf("\\") + 1);
			}
			return filename;
		}
	}
}
