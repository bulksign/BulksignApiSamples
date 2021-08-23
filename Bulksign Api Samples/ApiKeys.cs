using Bulksign.Api;

namespace Bulksign.ApiSamples
{
   public class ApiKeys
   {
       //to authenticate with a user personal token, just set UserEmail to a empty string

       public const string TOKEN = "";
       public const string EMAIL = "";

       public AuthenticationApiModel GetAuthorizationToken()
       {
           return new AuthenticationApiModel()
           {
               UserEmail = EMAIL,
               Token = TOKEN
           };
       }

   }
}