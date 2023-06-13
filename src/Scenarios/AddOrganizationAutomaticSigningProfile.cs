using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
	public class AddOrganizationAutomaticSigningProfile
	{

		public void RunSample()
		{
			AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

			if (string.IsNullOrEmpty(token.Key))
			{
				Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
				return;
			}

			BulksignApiClient client = new BulksignApiClient();

			var newProfile = new AutomaticSigningProfileApiModel();
			newProfile.Name = "My Profile";

			//we'll use the default organization certificate 
			newProfile.CertificateTypeApi = AutomaticSigningProfileCertificateTypeApi.Default;
			
			//set the base64 encoded signature image here
			newProfile.SignatureImageBase64 = "...............";

			//if we want to use a specific imprint for the signature, we have to set the name here
			//you can call GetSignatureImprints here, see sample from SigningImprints.cs
			newProfile.SignatureImprintName = "";

			try
			{
				BulksignResult<string> result = client.AddOrganizationAutomaticSigningProfile(token, newProfile);

				if (result.IsSuccessful)
				{
					Console.WriteLine($"Signing profile was successfully created ");
				}
				else
				{
					Console.WriteLine("ERROR : " + result.ErrorCode + " " + result.ErrorMessage);
				}
			}
			catch (BulksignException bex)
			{
				//handle failed request here
				Console.WriteLine($"Exception {bex.Message}, response is {bex.Response}");
			}
		}
	}
}