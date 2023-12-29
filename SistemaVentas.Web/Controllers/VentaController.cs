using Api.Web.WebApi.DTO.OperationResult;
using Api.Web.WebApi.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Web.Infrastructure.Core;
using Api.Web.WebApi.Utilities.Logger;
using Api.Web.WebApi.DTO.Response;
using static System.Formats.Asn1.AsnWriter;
using Api.Web.WebApi.DTO;

namespace SistemaVentas.Web.Controllers
{
    public class VentaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Logger _Logger;
        string _Url = string.Empty;
        public VentaController(IConfiguration configuration)
        {
            _configuration = configuration;
            this._Logger = new Logger(_configuration["EnvApp:LogPath"]);
            _Url = _configuration["Settings:Urlpi"];
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Autocomplete(string Search)
        {
            var Result = new object();
            //string _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            try
            {
                ListAutocompleteResponseDTO _Response = new ListAutocompleteResponseDTO();
                CoreAdministration _Core = new CoreAdministration(this._Logger);
                if (!(_Response = _Core.Autocomplete(_Url, Search)).Result.IsOK())
                    throw new Exception(string.Join(", ", _Response.Result.Errors.Select(x => x.Message)));
                Result = new { Error = string.Empty, IsOK = true,_Response.Autocomplete};
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
        public JsonResult ObtenerProducto(int idproducto)
        {
            var _Result = new object();
            var _Product = new ProductoDTO();
            ListProductsResponseDTO _Response = new ListProductsResponseDTO();
            CoreAdministration _Core = new CoreAdministration(this._Logger);
            try
            {
                if (!(_Response = _Core.GetProducts(_Url)).Result.IsOK())
                    throw new Exception(string.Join(", ", _Response.Result.Errors.Select(x => x.Message)));
                _Product = _Response.Items.Where(x => x.IdProducto == idproducto).FirstOrDefault();
                _Result = new { Error = string.Empty, IsOK = true, _Product };
            }
            catch (Exception ex)
            {
                //this._Logger.LogText("Erorr: Usuario : " + _CurrentUserName);
                this._Logger.LogError(ex);
                _Result = new { Error = ex.Message, IsOK = false, Code = OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR };
            }
            return Json(_Result);
        }
        [HttpPost]
        public JsonResult RegisterVentas(SaveVentaRequestDTO _Request)
        {
            var _Result = new object();
            RegistroVentaResponseDTO _Response = new RegistroVentaResponseDTO();
            CoreAdministration _Core = new CoreAdministration(this._Logger);
            try
            {
                if (!(_Response = _Core.RegisterVentas(_Url, _Request)).Result.IsOK())
                    throw new Exception(string.Join(", ", _Response.Result.Errors.Select(x => x.Message)));
                _Result = new { Error = string.Empty, IsOK = true, _Response.NroDocumento };
            }
            catch (Exception ex)
            {
                //this._Logger.LogText("Erorr: Usuario : " + _CurrentUserName);
                this._Logger.LogError(ex);
                _Result = new { Error = ex.Message, IsOK = false, Code = OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR };
            }
            return Json(_Result);
        }
    }
}
