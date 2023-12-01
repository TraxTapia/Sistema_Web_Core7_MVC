using Api.Web.WebApi.DTO.OperationResult;
using Api.Web.WebApi.DTO.Request;
using Api.Web.WebApi.DTO.Response;
using Api.Web.WebApi.Utilities.Logger;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Web.Infrastructure.Core;

namespace SistemaVentas.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Logger _Logger;
        string _Url = string.Empty;
        public CategoriasController(IConfiguration configuration)
        {
            _configuration = configuration;
            this._Logger = new Logger(_configuration["EnvApp:LogPath"]);
            _Url = _configuration["Settings:Urlpi"];
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetProducts()
        {
            var _Result = new object();
            ListCategoriasResponseDTO _Response = new ListCategoriasResponseDTO();
            CoreAdministration _Core = new CoreAdministration(this._Logger);
            //var _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                if (!(_Response = _Core.GetCategorias(_Url)).Result.IsOK())
                    throw new Exception(string.Join(", ", _Response.Result.Errors.Select(x => x.Message)));
            }
            catch (Exception ex)
            {
                //this._Logger.LogText("Error : Usuario : " + _CurrentUserName);
                this._Logger.LogError(ex);
            }
            return PartialView("_PanelResult", _Response);
        }
        [HttpPost]
        public JsonResult SaveCategoria(string _Descripcion)
        {
            var Result = new object();
            //string _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                OperationResult _Response = new OperationResult();
                CoreAdministration _Core = new CoreAdministration(this._Logger);
                if (!(_Response = _Core.SaveCategoria(_Url, _Descripcion)).IsOK())
                    throw new Exception(string.Join(", ", _Response.Errors.Select(x => x.Message)));

                Result = new { Error = string.Empty, IsOK = true, Code = OperationResult.StatusCodesEnum.OK };
            }
            catch (Exception ex)
            {
                //this._Logger.LogText("Erorr: Usuario : " + _CurrentUserName);
                this._Logger.LogError(ex);
                Result = new { Error = ex.Message, IsOK = false, Code = OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR };
            }
            return Json(Result);
        }
        [HttpPost]
        public JsonResult UpdateCategoria(UpdateCategoriaRequestDTO _Request)
        {
            var Result = new object();
            //string _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                OperationResult _Response = new OperationResult();
                CoreAdministration _Core = new CoreAdministration(this._Logger);
                if (!(_Response = _Core.UpdateCategoria(_Url, _Request)).IsOK())
                    throw new Exception(string.Join(", ", _Response.Errors.Select(x => x.Message)));
                Result = new { Error = string.Empty, IsOK = true, Code = OperationResult.StatusCodesEnum.OK };
            }
            catch (Exception ex)
            {
                //this._Logger.LogText("Erorr: Usuario : " + _CurrentUserName);
                this._Logger.LogError(ex);
                Result = new { Error = ex.Message, IsOK = false, Code = OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR };
            }
            return Json(Result);
        }
        [HttpPost]
        public JsonResult DeleteCategoria(int _IdCategoria)
        {
            var Result = new object();
            //string _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                OperationResult _Response = new OperationResult();
                CoreAdministration _Core = new CoreAdministration(this._Logger);
                if (!(_Response = _Core.DeleteCategoria(_Url, _IdCategoria)).IsOK())
                    throw new Exception(string.Join(", ", _Response.Errors.Select(x => x.Message)));
                Result = new { Error = string.Empty, IsOK = true, Code = OperationResult.StatusCodesEnum.OK };
            }
            catch (Exception ex)
            {
                //this._Logger.LogText("Erorr: Usuario : " + _CurrentUserName);
                this._Logger.LogError(ex);
                Result = new { Error = ex.Message, IsOK = false, Code = OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR };
            }
            return Json(Result);
        }
    }
}
