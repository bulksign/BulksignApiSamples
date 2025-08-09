
using System;
using Bulksign.Api;

namespace Bulksign.ApiSamples
{
    public class UpdateDraft
    {
        public void RunSample()
        {
            AuthenticationApiModel token = new Authentication().GetAuthenticationModel();

            if (string.IsNullOrEmpty(token.Key))
            {
                Console.WriteLine("Please edit Authentication.cs and set your own API key there");
                return;
            }

            string draftId = "";

            try
            {
                // Now, update the draft
                BulksignApiClient client = new BulksignApiClient();

                UpdateDraftApiModel draftToUpdate = new UpdateDraftApiModel()
                {
                    DraftId = draftId,
                    Name = "Updated Draft Name"
                };

                ApiResult<string> updateResult = client.UpdateDraftSettings(token, draftToUpdate);

                if (updateResult.IsSuccess)
                {
                    Console.WriteLine($"Successfully updated draft with ID: {draftId}");
                }
                else
                {
                    FailedRequestHandler.HandleFailedRequest(updateResult, "UpdateDraft");
                }
            }
            catch (Exception ex)
            {
                FailedRequestHandler.HandleException(ex, "UpdateDraft");
            }
        }
    }
}
