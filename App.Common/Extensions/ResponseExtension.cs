using System;
using System.Collections.Generic;
using System.Text;

namespace App.Common.Extensions
{
    public static class ResponseExtension
    {
        public static ServiceResponse<T> AsResponse<T>(this T result, int code, TypeMessage messageType, string messageText = "")
        {
            ServiceResponse<T> response = new ServiceResponse<T>();
            if (code >= 200 && code < 300)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }           

            response.Code = code;
            response.MessageType = messageType;
            response.Message = messageText;
            response.Result = result;

            return response;
        }
    }
}
