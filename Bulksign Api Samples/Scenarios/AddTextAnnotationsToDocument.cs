using System;
using System.IO;
using Bulksign.Api;
using Bulksign.Api.Enums;
using Bulksign_Api_Samples;

namespace Bulksign.ApiSamples.Scenarios
{
    public class AddTextAnnotationsToDocument
    {
        public void SendBundle()
        {
            try
            {
                BulkSignApi api = new BulkSignApi();

                BulksignBundle bb = new BulksignBundle();
                bb.DaysUntilExpire = 10;
                bb.DisableNotifications = false;
                bb.NotificationOptions = new BulksignNotificationOptions();

                BulksignRecipient recipient = new BulksignRecipient();

                bb.Recipients = new[]
                {
                    new BulksignRecipient()
                    {
                        Name = "Bulksign Test", 
                        Email = "contact@bulksign.com",
                        Index = 1,
                        RecipientType = BulksignApiRecipientType.Signer
                    }
                };


                BulksignDocument document = new BulksignDocument();
                document.Index = 1;
                document.FileName = "singlepage.pdf";


                document.NewSignatures = new BulksignNewSignature[]
                {
                    new BulksignNewSignature()
                    {
                        Height = 100,
                        Width = 250,
                        PageIndex = 1,
                        Left = 100,
                        Top = 500
                    }
                };

             
                //add new text annotations
                document.NewAnnotations = new BulksignNewAnnotation[3];

                //width,height, left and top values are in pixels
                BulksignNewAnnotation annCustom = new BulksignNewAnnotation
                {
                    Height = 300,
                    PageIndex = 1,
                    Left = 10,
                    Top = 650,
                    FontSize = 28,
                    Type = BulksignAnnotationType.Custom,
                    CustomText = "Annotation with custom text spaning multiple lines of text because the text is too long"
                };

                BulksignNewAnnotation annSenderName = new BulksignNewAnnotation
                {
                    Height = 100,
                    PageIndex = 1,
                    Left = 10,
                    Top = 900,
                    FontSize = 28,
                    Type = BulksignAnnotationType.SenderName
                };

                BulksignNewAnnotation annOrganization = new BulksignNewAnnotation
                {
                    Height = 100,
                    PageIndex = 1,
                    Left = 10,
                    Top = 940,
                    FontSize = 28,
                    Type = BulksignAnnotationType.OrganizationName
                };

                document.NewAnnotations = new[]
                {
                    annCustom, annSenderName, annOrganization
                };

                document.ContentBytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\Files\forms.pdf");
                bb.Documents = new[]
                {
                    document
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