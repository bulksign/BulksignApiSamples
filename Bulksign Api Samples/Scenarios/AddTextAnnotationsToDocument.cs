using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples.Scenarios
{
	public class AddTextAnnotationsToDocument
	{
		public void SendBundle()
		{
			try
			{
				BulkSignApi api = new BulkSignApi();

				BundleApiModel bb = new BundleApiModel();
				bb.DaysUntilExpire = 10;
				bb.DisableNotifications = false;
				bb.ReminderOptions = new ReminderOptionsApiModel()
				{
					EnableReminders = true,
					RecurrentEachDays = 2
				};

				RecipientApiModel recipient = new RecipientApiModel();

				bb.Recipients = new[]
				{
						  new RecipientApiModel()
						  {
								Name = "Bulksign Test",
								Email = "contact@bulksign.com",
								Index = 1,
								RecipientType = RecipientTypeApi.Signer
						  }
					 };


				DocumentApiModel document = new DocumentApiModel();
				document.Index = 1;
				document.FileName = "singlepage.pdf";


				document.NewSignatures = new[]
				{
						  new NewSignatureApiModel()
						  {
								Height = 100,
								Width = 250,
								PageIndex = 1,
								Left = 100,
								Top = 500
						  }
				};


				//add new text annotations
				document.NewAnnotations = new NewAnnotationApiModel[3];

				//width,height, left and top values are in pixels
				NewAnnotationApiModel annCustom = new NewAnnotationApiModel
				{
					Height = 300,
					PageIndex = 1,
					Left = 10,
					Top = 650,
					FontSize = 28,
					Type = AnnotationTypeApi.Custom,
					CustomText = "Annotation with custom text spanning multiple lines of text because the text is too long"
				};

				NewAnnotationApiModel annSenderName = new NewAnnotationApiModel
				{
					Height = 100,
					PageIndex = 1,
					Left = 10,
					Top = 900,
					FontSize = 28,
					Type = AnnotationTypeApi.SenderName
				};

				NewAnnotationApiModel annOrganization = new NewAnnotationApiModel
				{
					Height = 100,
					PageIndex = 1,
					Left = 10,
					Top = 940,
					FontSize = 28,
					Type = AnnotationTypeApi.OrganizationName
				};

				document.NewAnnotations = new[] {annCustom, annSenderName, annOrganization};

				document.FileContentByteArray = new FileContentByteArray()
				{
					ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\forms.pdf")
				};

				bb.Documents = new[] {document};

				AuthorizationApiModel token = new ApiKeys().GetAuthorizationToken();

				if (string.IsNullOrEmpty(token.UserToken))
				{
					Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
					return;
				}

				BulksignResult<SendBundleResultApiModel> result = api.SendBundle(token, bb);

				Console.WriteLine("Api call is successfull: " + result.IsSuccessful);

				if (result.IsSuccessful)
				{
					Console.WriteLine("Access code for recipient " + result.Response.AccessCodes[0].RecipientName + " is " + result.Response.AccessCodes[0].AccessCode);
					Console.WriteLine("Bundle id is : " + result.Response.BundleId);
				}
				else
				{
					Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
				}
			}
			catch (BulksignException bex)
			{
				Console.WriteLine(bex.Message + Environment.NewLine + bex.Response);
			}
		}
	}
}