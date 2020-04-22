using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Infrastructure
{
	public interface IUnitOfWork
	{
		void UploadImage(IFormFile file);

		void UploadFile(IFormFile file);
	}

}
