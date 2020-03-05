using Bulksign.Api;

namespace Bulksign.ApiSamples
{
   public class ApiKeys
   {
       public const string TOKEN = "";
       public const string EMAIL = "";

       public AuthorizationApiModel GetAuthorizationToken()
       {
           return new AuthorizationApiModel()
           {
               UserEmail = EMAIL,
               UserToken = TOKEN
           };
       }

   }
}