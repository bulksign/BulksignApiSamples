using Bulksign.Api;

namespace Bulksign.ApiSamples
{
   public class ApiKeys
   {
       //to authenticate with a user key, just set UserEmail to a empty string
       //to authenticate with an organization key, please set both Key and UserEmail

       public const string API_KEY = "";
       public const string EMAIL = "";

       public AuthenticationApiModel GetAuthentication()
       {
           return new AuthenticationApiModel()
           {
               UserEmail = EMAIL,
               Key = API_KEY
           };
       }

   }
}