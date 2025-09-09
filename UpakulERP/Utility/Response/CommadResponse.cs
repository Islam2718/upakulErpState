using System.Net;

namespace Utility.Response
{
    public record CommadResponse(string Message, HttpStatusCode StatusCode, int? ReturnId=0,object? Returnobj = null) { }
}
