using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples.Scenarios
{
	public class CustomDocumentAccess
	{
		public void SendBundle()
		{

			AuthorizationApiModel token = new ApiKeys().GetAuthorizationToken();

			if (string.IsNullOrEmpty(token.UserToken))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}


			BulkSignApi api = new BulkSignApi();

			BundleApiModel bundle = new BundleApiModel();
			bundle.DaysUntilExpire = 10;
			bundle.Message = "Please sign this document";
			bundle.Subject = "Please Bulksign this document";
			bundle.Name = "Test bundle";

			bundle.Recipients = new[]
			{
				new RecipientApiModel()
				{
					Name = "Bulksign Test",
					Email = "contact@bulksign.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				},

				new RecipientApiModel()
				{
					Name = "Second Recipient",
					Email = "second@bulksign.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				}
			};



			bundle.Documents = new[]
			{
				new DocumentApiModel()
				{
					Index = 1,
					FileName = "bulksign_test_Sample.pdf",
					FileContentByteArray = new FileContentByteArray()
					{
						ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf")
					}
				},
				new DocumentApiModel()
				{
					Index = 2,
					FileName = "forms.pdf",
					FileContentByteArray = new FileContentByteArray()
					{
						ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\forms.pdf")
					}
				}
			};

			bundle.FileAccessMode = FileAccessModeApi.Custom;

			//assign different files to different recipients

			bundle.CustomFileAccess = new[]
			{
				//assign first file to first recipient
				new CustomFileAccessApiModel()
				{
					RecipientEmail = bundle.Recipients[0].Email,
					FileNames = new []{ "bulksign_test_Sample.pdf" }
				},

				//assign first file to first recipient
				new CustomFileAccessApiModel()
				{
					RecipientEmail = bundle.Recipients[1].Email,
					FileNames = new[]
					{
						"forms.pdf"
					}
				}
			};

			BulksignResult<SendBundleResultApiModel> result = api.SendBundle(token, bundle);


			if (result.IsSuccessful)
			{
				Console.WriteLine("Access code for recipient " + result.Response.AccessCodes[0].RecipientName + " is " + result.Response.AccessCodes[0].AccessCode);
				Console.WriteLine("Bundle id is : " + result.Response.BundleId);
			}
			else
			{
				Console.WriteLine($"Request failed : ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
			}


		}
	}
}