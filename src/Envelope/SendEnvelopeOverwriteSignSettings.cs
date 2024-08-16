using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SendEnvelopeOverwriteSignSettings
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

			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.EnvelopeType                    = EnvelopeTypeApi.Serial;
			envelope.DisableRecipientNotifications = false;

			envelope.Recipients = new[]
			{
				new RecipientApiModel
				{
					Name          = "Recipient First",
					Email         = "add_email_address_here",
					Index         = 1,
					RecipientType = RecipientTypeApi.Signer
				}
			};

			envelope.Documents = new[]
			{
				new DocumentApiModel
				{
					Index    = 1,
					FileName = "singlepage.pdf",
					FileContentByteArray = new FileContentByteArray
					{
						ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_sample.pdf")
					}
				}
			};


			//please note that you need to overwrite ALL settings
			envelope.OverwriteSignSettings = new SignSettingsApiModel()
			{
				ForceSignerToReadDocument = true,
				EnableBrowserGeolocationRequest = false,
				AllowRejectWithoutRejectionText = true,
				ShowSignerIntroductionDetailsPage = false,
				AutoNavigationOnOpen = false,
				AutomaticFinishAfterSigning = true,
				SigningConfirmationForProfileAndClickToSign = true,
				AllowSignerToUploadImageForDrawToSign = false,

				DocumentDownload = SignerDownloadDocumentActionTypeApi.RedirectToUrl,
				DocumentDownloadRedirectUrl = "https://mywebsite.com"
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
				//handle failed request here
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}" );	
			}
		}
	}
}