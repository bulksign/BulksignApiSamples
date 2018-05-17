using System;
using System.IO;
using Bulksign.Api;
using Bulksign_Api_Samples;

namespace Bulksign.ApiSamples.Scenarios
{
   public class CustomDocumentAccess
   {
      public void SendBundle()
      {

         BulkSignApi api = new BulkSignApi();

         BulksignBundle bb = new BulksignBundle();
         bb.DaysUntilExpire = 10;
         bb.DisableNotifications = false;
         bb.NotificationOptions = new BulksignNotificationOptions();
         bb.Message = "Please sign this document";
         bb.Subject = "Please Bulksign this document";
         bb.Name = "Test bundle";

         BulksignRecipient firstRecipient = new BulksignRecipient();
         firstRecipient.Name = "Bulksign Test";
         firstRecipient.Email = "contact@bulksign.com";
         firstRecipient.Index = 1;
         firstRecipient.RecipientType = BulksignApiRecipientType.Signer;

         BulksignRecipient secondRecipient = new BulksignRecipient();
         secondRecipient.Name = "Second Recipient";
         secondRecipient.Email = "second@bulksign.com";
         secondRecipient.Index = 1;
         secondRecipient.RecipientType = BulksignApiRecipientType.Signer;

         bb.Recipients = new[]
         {
                firstRecipient, secondRecipient
         };

         BulksignDocument firstDocument = new BulksignDocument();
         firstDocument.Index = 1;
         firstDocument.FileName = "bulksign_test_Sample.pdf";
         firstDocument.ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf");

         BulksignDocument secondDocument = new BulksignDocument();
         secondDocument.Index = 2;
         secondDocument.FileName = "forms.pdf";
         secondDocument.ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\forms.pdf");

         bb.Documents = new[]
         {
            firstDocument, secondDocument
         };

         bb.FileAccessMode = BulksignApiFileAccessMode.Custom;

         //assign different files to different recipients

         bb.CustomFileAccess = new BulksignFileAccess[2];

         //assign first file to first recipient
         bb.CustomFileAccess[0] = new BulksignFileAccess()
         {
            FileIndex = 1,
            RecipientIndex = 1
         };

         //assign first file to first recipient
         bb.CustomFileAccess[1] = new BulksignFileAccess()
         {
            FileIndex = 2,
            RecipientIndex = 2
         };


         BulksignAuthorization token = new ApiKeys().GetAuthorizationToken();

         if (string.IsNullOrEmpty(token.UserToken))
         {
            Console.WriteLine("Please edit APiKeys.cs and put your own token/email");
            return;
         }


         BulksignResult<BulksignSendBundleResult> result = api.SendBundle(token, bb);

         Console.WriteLine("Api call is successfull: " + result.IsSuccessful);

         if (result.IsSuccessful)
         {
            Console.WriteLine("Access code for recipient " + result.Response.AccessCodes[0].RecipientName + " is " + result.Response.AccessCodes[0].AccessCode);
            Console.WriteLine("Bundle id is : " + result.Response.BundleId);
         }

      }
   }
}