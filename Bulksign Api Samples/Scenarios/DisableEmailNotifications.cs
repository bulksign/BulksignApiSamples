﻿using System;
using System.IO;
using Bulksign.Api;
using Bulksign_Api_Samples;

namespace Bulksign.ApiSamples
{
    public class DisableEmailNotifications
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

            //this will disable notifications for this bundle
            bb.DisableNotifications = true;

            BulksignRecipient recipient = new BulksignRecipient();
            recipient.Name = "Bulksign Test";
            recipient.Email = "contact@bulksign.com";
            recipient.Index = 1;
            recipient.RecipientType = BulksignApiRecipientType.Signer;

            bb.Recipients = new[]
            {
                recipient
            };

            BulksignDocument document = new BulksignDocument();
            document.Index = 1;
            document.FileName = "test.pdf";
            document.ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\bulksign_test_Sample.pdf");
            bb.Documents = new[] { document };

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