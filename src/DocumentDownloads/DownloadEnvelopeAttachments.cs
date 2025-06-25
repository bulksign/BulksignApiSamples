using Bulksign.Api;

namespace Bulksign.ApiSamples;

public class DownloadEnvelopeAttachments
{
	public void RunSample()
	{
		const string ENVELOPE_ID = "your_envelope_id";


		AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

		if (string.IsNullOrEmpty(token.Key))
		{
			Console.WriteLine("Please edit Authentication.cs and set your own API key there");
			return;
		}

		BulksignApiClient client = new BulksignApiClient();

		try
		{
			ApiResult<byte[]> result = client.DownloadEnvelopeCompletedAttachments(token, ENVELOPE_ID);

			if (result.IsSuccess)
			{
				//the result here will by a byte[] of a zip file which contains ALL PDF attachments from all the envelope documents
				Console.WriteLine($"File size :  {result.Result.Length}");


				//zip file can be unzipped like this
				Dictionary<string,byte[]> files = new Utilities().UnzipFiles(result);


				//if you need to match the attachment files with the recipient who attach them


				//call GetEnvelopeDetails to obtain the envelope information
				ApiResult<EnvelopeDetailsResultApiModel> envelopeResult = client.GetEnvelopeDetails(token,ENVELOPE_ID);

				if (!envelopeResult.IsSuccess)
				{
					Console.WriteLine("ERROR : " + envelopeResult.ErrorCode + " " + envelopeResult.ErrorMessage);
				}

				foreach (EnvelopeRecipientResultApiModel recipient in envelopeResult.Result.Recipients)
				{
					if (recipient.Attachments != null)
					{
						foreach (CompletedAttachmentApiModel a in recipient.Attachments)
						{
							//find the file based on the file name which will be unique
							KeyValuePair<string,byte[]> pair = files.FirstOrDefault(f => f.Key.ToLower() == a.FileName.ToLower());
						}
					}
				}

			}
			else
			{
				FailedRequestHandler.HandleFailedRequest(result, nameof(client.GetEnvelopeDetails));
			}
		}
		catch (Exception ex)
		{
			FailedRequestHandler.HandleException(ex, nameof(client.GetEnvelopeDetails));
		}
	}
}