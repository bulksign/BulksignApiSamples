﻿using System;
using System.IO;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class DocumentTimestamping
	{
		public void SendEnvelope()
		{

			AuthenticationApiModel token = new ApiKeys().GetAuthentication();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulkSignApi api = new BulkSignApi();

			EnvelopeApiModel envelope = new EnvelopeApiModel();
			envelope.DaysUntilExpire = 10;
			envelope.EmailMessage = "Please sign this document";
			envelope.EmailSubject = "Please Bulksign this document";
			envelope.Name = "Test envelope";

			envelope.Recipients = new[]
			{
				new RecipientApiModel()
				{
					Name = "Bulksign Test",
					Email = "contact@bulksign.com",
					Index = 1,
					RecipientType = RecipientTypeApi.Signer
				}
			};

			envelope.Documents = new[]
			{
				new DocumentApiModel()
				{
					Index = 1,
					FileName = "test.pdf",
					FileContentByteArray = new FileContentByteArray()
					{
						ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf")
					}
				}
			};


			//Timestamping must be enabled from Settings\Signing for this to work

			//enable timestamping once per document after all signers have signed the document(s)
			envelope.DocumentTimestampType = DocumentTimestampTypeApi.Envelope;

			//enable timestamping after each signer finishes signing the document(s)
			//envelope.DocumentTimestampType = DocumentTimestampTypeApi.Recipient;


			BulksignResult<SendEnvelopeResultApiModel> result = api.SendEnvelope(token, envelope);


			if (result.IsSuccessful)
			{
				Console.WriteLine("Access code for recipient " + result.Response.RecipientAccess[0].RecipientEmail + " is " + result.Response.RecipientAccess[0].AccessCode);
				Console.WriteLine("Envelope id is : " + result.Response.EnvelopeId);
			}
			else
			{
				Console.WriteLine($"Request failed : ErrorCode '{result.ErrorCode}' , Message {result.ErrorMessage}");
			}

		}
	}
}