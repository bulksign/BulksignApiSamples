using Bulksign;
using Bulksign.DomainLogic.Api;

namespace Bulksign.ApiSamples;

public class FailedRequestHandler
{
    public static void HandleFailedRequest<T>(ApiResult<T> result, string name)
    {
        if (result == null)
        {
            Console.WriteLine($"ERROR : Request to '{name}' failed, result is null");
        }
        
        if (result.IsSuccess == false)
        {
            Console.WriteLine($"ERROR : Request to '{name}' failed, errorCode : {result.ErrorCode}, message : {result.ErrorMessage}, requestId : {result.RequestId} ");
            
            //you can now also handle specific API error results here
            switch (result.ErrorCode)
            {
                case ApiErrorCode.API_ERROR_CODE_NO_RECIPIENTS:
                    //do something here
                    break;
					
                case ApiErrorCode.API_ERROR_CODE_FIELD_ASSIGNMENT_INVALID_RECIPIENT_IDENTIFIER:
                    //do something here
                    break;
                
            }
        }
    }

    public static void HandleException(Exception ex, string name)
    {
        //write the status code
        if (ex is HttpRequestException httpRequestException)
        {
            Console.WriteLine($"ERROR : Request to '{name}' failed, status code {httpRequestException?.StatusCode.ToString()} ");
            return;
        }

        if (ex is BulksignApiException apiException)
        {
            Console.WriteLine($"ERROR : Request to '{name}' failed, response : '{apiException.Response}', message {apiException.Message} ");
            return;
        }
        
        Console.WriteLine($"ERROR : Request to '{name}' failed, message : {ex.Message} ");
    }
    
}