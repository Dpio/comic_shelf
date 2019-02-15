﻿using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace ComicShelf.Api
{
		public class FileUploadOperation : IOperationFilter
		{
			public void Apply(Operation operation, OperationFilterContext context)
			{
				if (operation.OperationId.ToLower() == "apisampledatauploadpost")
				{
					operation.Parameters.Clear();
					operation.Parameters.Add(new NonBodyParameter
					{
						Name = "uploadedFile",
						In = "formData",
						Description = "Upload File",
						Required = true,
						Type = "file"
					});
					operation.Consumes.Add("multipart/form-data");
				}
			}
		}
	}


