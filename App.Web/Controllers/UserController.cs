using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Common.DTO;
using App.Web.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using App.Domain.Services;
using App.Common.Extensions;
using System.Net;
using Rollbar;
using App.Common.Resources;
using App.Web.Classes;

namespace App.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController<UserDTO>
    {
        protected IUserService _userService;
        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GenerarToken")]
        public IActionResult GenerarToken([FromBody] LoginDTO login)
        {
            try
            {
                string data = _userService.GenerateJSONWebToken(login);

                if (data != null)
                    return Json(data.AsResponse((int)HttpStatusCode.OK, TypeMessage.Succes));
                else
                    return Json(ResponseExtension.AsResponse<string>(null,
                        (int)HttpStatusCode.NotAcceptable, TypeMessage.Error, Messages.M001));
            }
            catch (Exception ex)
            {
              
                return Json(ResponseExtension.AsResponse<string>(null,
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }

       
        [HttpGet]
        [Authorize]
     
        [Route("GetUser")]
        public IActionResult GetUser()
        {
            try
            {
                var data = _userService.GetUser();

                if (data != null)
                    return Json(data.AsResponse((int)HttpStatusCode.OK, TypeMessage.Succes));
                else
                    return Json(ResponseExtension.AsResponse<string>(null,
                        (int)HttpStatusCode.NotAcceptable, TypeMessage.Error, Messages.M003));
            }
            catch (Exception ex)
            {

                return Json(ResponseExtension.AsResponse<string>(null,
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }

        [HttpPost]
        [Authorize]
        [Route("CreateUser")]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var data = _userService.CreateUser(userDTO);

                if (data != null)
                    return Json(data.AsResponse((int)HttpStatusCode.OK, TypeMessage.Succes));
                else
                    return Json(ResponseExtension.AsResponse<string>(null,
                        (int)HttpStatusCode.NotAcceptable, TypeMessage.Error, Messages.M002));
            }
            catch (Exception ex)
            {

                return Json(ResponseExtension.AsResponse<string>(null,
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }

        [HttpPost]
        [Authorize]
        [Route("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var data = _userService.UpdateUser(userDTO);

                if (data != null)
                    return Json(data.AsResponse((int)HttpStatusCode.OK, TypeMessage.Succes));
                else
                    return Json(ResponseExtension.AsResponse<string>(null,
                        (int)HttpStatusCode.NotAcceptable, TypeMessage.Error, Messages.M003));
            }
            catch (Exception ex)
            {

                return Json(ResponseExtension.AsResponse<string>(null,
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }

        [HttpPost]
        [Authorize]
        [Route("DeleteUser")]
        public IActionResult DeleteUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var data = _userService.DeleteUser(userDTO);

                if (data != null)
                    return Json(data.AsResponse((int)HttpStatusCode.OK, TypeMessage.Succes));
                else
                    return Json(ResponseExtension.AsResponse<string>(null,
                        (int)HttpStatusCode.NotAcceptable, TypeMessage.Error, Messages.M003));
            }
            catch (Exception ex)
            {

                return Json(ResponseExtension.AsResponse<string>(null,
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }

       
        [HttpPost]
        [AutorizeToken]
        [Route("FilterUser")]
        public IActionResult FilterUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var data = _userService.filterUser(userDTO);

                if (data != null)
                    return Json(data.AsResponse((int)HttpStatusCode.OK, TypeMessage.Succes));
                else
                    return Json(ResponseExtension.AsResponse<string>(null,
                        (int)HttpStatusCode.NotAcceptable, TypeMessage.Error, Messages.M003));
            }
            catch (Exception ex)
            {

                return Json(ResponseExtension.AsResponse<string>(null,
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }
    }
}
