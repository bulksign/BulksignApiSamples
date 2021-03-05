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

				AuthorizationApiModel token = new ApiKeys().GetAuthorizationToken();

				if (string.IsNullOrEmpty(token.UserToken))
				{
					Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
					return;
				}


				BulkSignApi api = new BulkSignApi();

				EnvelopeApiModel bb = new EnvelopeApiModel();
				bb.DaysUntilExpire = 10;
				bb.DisableSignerEmailNotifications = false;
				bb.ReminderOptions = new ReminderOptionsApiModel()
				{
					EnableReminders = true,
					RecurrentEachDays = 2
				};

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
				document.NewAnnotations = new[]
				{
					//width,height, left and top values are in pixels
					new NewAnnotationApiModel
					{
						PageIndex = 1,
						Left = 10,
						Top = 650,
						FontSize = 28,
						Type = AnnotationTypeApi.SenderCustom,
						CustomText = "Annotation with custom text spanning multiple lines of text because the text is too long"
					},

					new NewAnnotationApiModel
					{
						PageIndex = 1,
						Left = 10,
						Top = 900,
						FontSize = 28,
						Type = AnnotationTypeApi.SenderName
					},

					new NewAnnotationApiModel
					{
						PageIndex = 1,
						Left = 10,
						Top = 940,
						FontSize = 28,
						Type = AnnotationTypeApi.SenderOrganizationName
					}

			};


				bb.Documents = new[]
				{
					new DocumentApiModel()
					{
						FileContentByteArray = new FileContentByteArray()
						{
							ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\forms.pdf")
						},
						FileName = "forms.pdf"
					}
			};


				BulksignResult<SendEnvelopeResultApiModel> result = api.SendEnvelope(token, bb);

				if (result.IsSuccessful)
				{
					Console.WriteLine("Access code for recipient " + result.Response.RecipientAccess[0].RecipientEmail + " is " + result.Response.RecipientAccess[0].AccessCode);
					Console.WriteLine("Bundle id is : " + result.Response.EnvelopeId);
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