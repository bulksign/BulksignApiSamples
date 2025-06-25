using Bulksign;

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