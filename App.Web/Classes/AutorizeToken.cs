using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Classes
{
    public class AutorizeToken: ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            string token = context.HttpContext.Request.Headers.FirstOrDefault(x=>x.Key== "Authorization").Value.FirstOrDefault();
     
          
            TestJwtSecurityTokenHandler(token);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context); 
        }
        public void TestJwtSecurityTokenHandler(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            token = token.Replace("Bearer","").Trim();
           var jwtSecurityToken = handler.ReadJwtToken(token);
            if (jwtSecurityToken.Issuer != "Finaktiva") 
            {
                 throw new Exception("Not authorized");
            }
        }

    }
}
