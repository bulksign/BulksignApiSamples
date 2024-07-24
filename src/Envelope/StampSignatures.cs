using System;
using System.IO;
using System.Linq;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class StampSignatures
	{
		public void RunSample()
		{
			AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit Authentication.cs and set your own API key there");
				return;
			}

			BulksignApiClient client = new BulksignApiClient();

			BulksignResult<string[]> stamps = client.GetSignatureStamps(token);

			//for this sample we require to define at least 1 signature stamp

			if (stamps.Result.Length == 0)
			{
				Console.WriteLine("This sample requires to have at least 1 signature stamp. ");
				return;
			}

			//load the imprints too
			BulksignResult<string[]> imprints = client.GetSignatureImprints(token);


			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.EnvelopeType = EnvelopeTypeApi.Serial;
			envelope.DaysUntilExpire = 10;
			envelope.DisableRecipientNotifications = false;

			envelope.Recipients = new[]
			{
				new RecipientApiModel
				{
					Name = "Bulksign Test",
					Email = "contact@bulksign.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				}
			};

			envelope.Documents = new[]
			{
				new DocumentApiModel
				{
					Index = 1,
					FileName = "singlepage.pdf",
					FileContentByteArray = new FileContentByteArray
					{
						ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\singlepage.pdf")
					},
					NewSignatures = new[]
					{
						//see https://bulksign.com/docs/howdoi.htm#is-there-a-easy-way-to-determine-the-position-top-and-left-of-the-new-form-fields-on-the-page-
						//about how to set the signature field at a fixed position

						new NewSignatureApiModel
						{
							//width,height, left and top values are in pixels
							Height = 100,
							Width = 250,
							PageIndex = 1,
							Left = 20,
							Top = 30,
							SignatureType = SignatureTypeApi.Stamp,
							AssignedToRecipientEmail = envelope.Recipients[0].Email,
							StampSignatureConfiguration = new StampSignatureConfigurationApiModel
							{
								//user must upload the signature image
								StampType = StampSignatureTypeApi.UserProvided
							}
						},
						new NewSignatureApiModel
						{
							Height = 100,
							Width = 250,
							PageIndex = 1,
							Left = 20,
							Top = 70,
							SignatureType = SignatureTypeApi.Stamp,
							AssignedToRecipientEmail = envelope.Recipients[0].Email,
							StampSignatureConfiguration = new StampSignatureConfigurationApiModel
							{
								//user must upload the signature image
								StampType = StampSignatureTypeApi.PredefinedStamp,

								//we'll use the first stamp defined in the organization
								StampName = stamps.Result.FirstOrDefault(),

								//apply an imprint too for this signature
								ImprintName = imprints.Result.FirstOrDefault()

							}
						}
					}
				}
			};

			try
			{
				BulksignResult<SendEnvelopeResultApiModel> result = client.SendEnvelope(token, envelope);

				if (result.IsSuccessful)
				{
					Console.WriteLine("Access code for recipient " + result.Result.RecipientAccess[0].RecipientEmail + " is " + result.Result.RecipientAccess[0].AccessCode);
					Console.WriteLine("EnvelopeId is : " + result.Result.EnvelopeId);
				}
				else
				{
					Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
				}
			}
			catch (BulksignException bex)
			{
				//handle failed request here. See
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}