using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LsqParserEngine.WebApi.Models
{
    public class MessageObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status200OK;

        public MessageObjectResult(string value)
            : base(value)
        {
            StatusCode = DefaultStatusCode;
        }
    }
}