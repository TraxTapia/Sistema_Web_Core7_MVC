using Api.Web.WebApi.DTO.OperationResult;
using Api.Web.WebApi.DTO.Request;
using Api.Web.WebApi.DTO.Response;
using Api.Web.WebApi.Utilities.Logger;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Web.Infrastructure.Core;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace SistemaVentas.Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Logger _Logger;
        string _Url = string.Empty;

        public ProductosController(IConfiguration configuration)
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
            ListProductsResponseDTO _Response = new ListProductsResponseDTO();
            CoreAdministration _Core = new CoreAdministration(this._Logger);
            //var _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                if (!(_Response = _Core.GetProducts(_Url)).Result.IsOK())
                    throw new Exception(string.Join(", ", _Response.Result.Errors.Select(x => x.Message)));
            }
            catch (Exception ex)
            {
                //this._Logger.LogText("Error : Usuario : " + _CurrentUserName);
                this._Logger.LogError(ex);
            }
            return PartialView("_PanelResult", _Response);
        } 

        public JsonResult GetCategorias()
        {
            var Result = new object();
            //string _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                ListCategoriasResponseDTO _OperationResult = new ListCategoriasResponseDTO();
                CoreAdministration _Core = new CoreAdministration(this._Logger);
                _OperationResult = _Core.GetCategorias(_Url);
                if (_OperationResult.Result.Code == OperationResult.StatusCodesEnum.CONFLICT.ToString("D"))
                    return Json(new { Error = string.Empty, IsOK = false, Code = OperationResult.StatusCodesEnum.CONFLICT });
                if (!_OperationResult.Result.IsOK() && _OperationResult.Result.Code != OperationResult.StatusCodesEnum.ACCEPTED.ToString("D"))
                    throw new Exception(string.Join(", ", _OperationResult.Result.Errors.Select(x => x.Message)));
                Result = new { Error = string.Empty, IsOK = true, Code = OperationResult.StatusCodesEnum.OK, _OperationResult.Items };
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
        public JsonResult SaveProducto(SaveProductoRequestDTO _Request)
        {
            var Result = new object();
            //string _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                OperationResult _Response = new OperationResult();
                CoreAdministration _Core = new CoreAdministration(this._Logger);
                if (!(_Response = _Core.SaveProducto(_Url, _Request)).IsOK())
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
        public JsonResult UpdateProducto(UpdateProductoRequestDTO _Request)
        {
            var Result = new object();
            //string _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                OperationResult _Response = new OperationResult();
                CoreAdministration _Core = new CoreAdministration(this._Logger);
                if (!(_Response = _Core.UpdateProducto(_Url, _Request)).IsOK())
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
        public JsonResult DeleteProduct(int _IdProducto)
        {
            var Result = new object();
            //string _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                OperationResult _Response = new OperationResult();
                CoreAdministration _Core = new CoreAdministration(this._Logger);
                if (!(_Response = _Core.DeleteProduct(_Url, _IdProducto)).IsOK())
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
