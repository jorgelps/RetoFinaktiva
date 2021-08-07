
using App.Common.Extensions;
using App.Common.Resources;
using App.Domain.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace App.Web.Base
{
    public abstract class BaseController<TDTO> : Controller where TDTO : class
    {
        private IBaseService<TDTO> _service;

        public BaseController(IBaseService<TDTO> service)
        {
            _service = service;
        }

        public BaseController() { }

        [HttpGet]
        [Route("getAll")]
        [Authorize]
        public virtual IActionResult GetAll()
        {
            try
            {
                var data = _service.GetAll();                
                return Json(data.AsResponse((int)HttpStatusCode.OK, TypeMessage.Succes));
            }
            catch (Exception ex)
            {
                
                return Json(ResponseExtension.AsResponse<string>(null, 
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }

        [HttpGet]
        [Route("details/{id:int}")]
        [Authorize]
        public virtual IActionResult Details(int id)
        {
            try
            {
                var data = _service.FindById(id);
                if (data == null)
                {
                    return Json(ResponseExtension.AsResponse<string>(null, 
                        (int)HttpStatusCode.OK, TypeMessage.Information, Messages.M003));
                }
                else
                {
                    return Json(data.AsResponse((int)HttpStatusCode.OK, TypeMessage.Succes));
                }
            }
            catch (Exception ex)
            {
                   return Json(ResponseExtension.AsResponse<string>(null, 
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public virtual IActionResult Create([FromBody]TDTO modelDTO)
        {
            try
            {
                var data = _service.Create(modelDTO);
                return Json(data.AsResponse((int)HttpStatusCode.OK, TypeMessage.Succes));
            }
            catch (ApplicationException ex)
            {
                    return Json(ResponseExtension.AsResponse<string>(null, 
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
            catch (Exception ex)
            {
                  return Json(ResponseExtension.AsResponse<string>(null, 
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }

        [HttpPost]
        [Route("update")]
        [Authorize]
        public virtual IActionResult Update([FromBody]TDTO modelDTO)
        {
            try
            {
                var data = _service.Update(modelDTO);
                return Json(data.AsResponse((int)HttpStatusCode.OK, TypeMessage.Succes));
            }
            catch (ApplicationException ex)
            {
                 return Json(ResponseExtension.AsResponse<string>(null, 
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
            catch (Exception ex)
            {
                   return Json(ResponseExtension.AsResponse<string>(null, 
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }

        [HttpPost]
        [Route("delete")]
        [Authorize]
        public virtual IActionResult Delete([FromBody]int id)
        {
            try
            {
                var data = _service.FindById(id);

                if (data == null)
                {
                    return Json(ResponseExtension.AsResponse<string>(null, 
                        (int)HttpStatusCode.OK, TypeMessage.Information, Messages.M003));
                }

                _service.Delete(id);
                return Json(ResponseExtension.AsResponse<string>(null, 
                    (int)HttpStatusCode.OK, TypeMessage.Succes));
            }
            catch (ApplicationException ex)
            {
              
                return Json(ResponseExtension.AsResponse<string>(null, 
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
            catch (Exception ex)
            {
               
                return Json(ResponseExtension.AsResponse<string>(null, 
                    (int)HttpStatusCode.InternalServerError, TypeMessage.Error, Messages.M002));
            }
        }
       
    }
}
