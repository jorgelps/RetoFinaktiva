using App.Common.Extensions;
using App.Common.Resources;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Threading.Tasks;

namespace App.Web.Middlewares
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ResponseMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            await _requestDelegate(context);
            var statusCode = context.Response.StatusCode;

            if (statusCode != (int)HttpStatusCode.OK)
            {
                var respuesta = ResponseExtension.AsResponse<string>(null, (int)HttpStatusCode.Unauthorized, TypeMessage.Error, Messages.M001);
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(respuesta, jsonSerializerSettings));
            }
        }
    }
}
