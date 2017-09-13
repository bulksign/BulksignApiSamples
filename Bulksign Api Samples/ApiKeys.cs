using Bulksign.Api;

namespace Bulksign_Api_Samples
{
	public class ApiKeys
	{
	    public const string TOKEN = "";
	    public const string EMAIL = "";

	    public BulksignAuthorization GetAuthorizationToken()
	    {
	        return new BulksignAuthorization()
	        {
	            UserEmail = EMAIL,
	            UserToken = TOKEN
	        };
	    }

	}
}