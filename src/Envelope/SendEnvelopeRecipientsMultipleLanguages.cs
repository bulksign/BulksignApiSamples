using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class SendEnvelopeRecipientsMultipleLanguages
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

			//define recipients, each recipient has assigned a different language 
			envelope.Recipients = new[]
			{
				new RecipientApiModel
				{
					Name          = "Recipient First",
					Email         = "first_recipient_email", //replace with proper email address
					Index         = 1,
					RecipientType = RecipientTypeApi.Signer,
					Language = "en-US"
				},
				new RecipientApiModel
				{
					Name          = "Recipient Second",
					Email         = "second_recipient_email", //replace with proper email address,
					Index         = 2,
					RecipientType = RecipientTypeApi.Signer,
					Language      = "bg-BG"
				},
				new RecipientApiModel
				{
					Name          = "Recipient Third",
					Email         = "third_recipient_email", //replace with proper email address
					Index         = 3,
					RecipientType = RecipientTypeApi.Signer,
					Language      = "es-ES"
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
					},
					NewSignatures = new []
					{
						new NewSignatureApiModel()
						{
							AssignedToRecipientEmail = "first_recipient_email",
							Height                   = 10,
							Width                    = 10,
							PageIndex                = 1,
							SignatureType            = SignatureTypeApi.ClickToSign,
							Left                     = 10,
							Top                      = 3
						},
						new NewSignatureApiModel()
						{
							AssignedToRecipientEmail = "second_recipient_email",
							Height                   = 10,
							Width                    = 10,
							PageIndex                = 1,
							SignatureType            = SignatureTypeApi.ClickToSign,
							Left                     = 10,
							Top                      = 13
						},
						new NewSignatureApiModel()
						{
							AssignedToRecipientEmail = "third_recipient_email",
							Height                   = 10,
							Width                    = 10,
							PageIndex                = 1,
							SignatureType            = SignatureTypeApi.ClickToSign,
							Left                     = 10,
							Top                      = 33
						}
					}
				}
			};

			
			//define custom email messages for all 3 required languages
			envelope.Messages = new MessageApiModel[]
			{
				new MessageApiModel()
				{
					Language = "en-US",
					Subject  = "Please sign the enclosed envelope",
					Message  = "Dear {{#RecipientName#}},\r\nPlease review and sign the envelope {{#EnvelopeName#}}\r\nEnvelope will expire at {{#ExpirationDate#}}"
				},
				new MessageApiModel()
				{
					Language = "bg-BG",
					Subject  = "Моля подпишете приложения плик",
					Message  = "скъпи {{#RecipientName#}},\r\nМоля, прегледайте и подпишете плика {{#EnvelopeName#}}\r\nВалидността на плика ще изтече на {{#ExpirationDate#}}"
				},
				new MessageApiModel()
				{
					Language = "es-ES",
					Subject  = "Por favor firme el sobre adjunto",
					Message  = "Estimado {{#RecipientName#}},\r\nPor favor revise y firme el sobre {{#EnvelopeName#}}\r\nEl sobre vencerá en {{#ExpirationDate#}}"
				}
			};
			
			try
			{
				ApiResult<SendEnvelopeResultApiModel> result = client.SendEnvelope(token, envelope);

				if (result.IsSuccess)
				{
					Console.WriteLine("Access code for recipient " + result.Result.RecipientAccess[0].RecipientEmail + " is " + result.Result.RecipientAccess[0].AccessCode);
					Console.WriteLine("EnvelopeId is : " + result.Result.EnvelopeId);
				}
				else
				{
					Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
				}
			}
			catch (BulksignApiException bex)
			{
				//handle failed request here
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}" );	
			}
		}
	}
}