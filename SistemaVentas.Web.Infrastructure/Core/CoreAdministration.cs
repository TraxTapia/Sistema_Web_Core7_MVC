using Api.Web.WebApi.DTO.OperationResult;
using Api.Web.WebApi.DTO.Request;
using Api.Web.WebApi.DTO.Response;
using Api.Web.WebApi.Utilities.Logger;
using SistemaVentas.Web.Infrastructure.ClientApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Web.Infrastructure.Core
{
    public  class CoreAdministration
    {
        private Logger _Logger;
        public CoreAdministration(Logger Logger)
        {
            this._Logger = Logger;
        }
        //public ListProductsResponseDTO GetProducts(string EndPoint, ClaimsIdentity Claims)
        public ListProductsResponseDTO GetProducts(string EndPoint)
        {
            ListProductsResponseDTO Response = new ListProductsResponseDTO();
            try
            {
                ClientApiServices _Client = new ClientApiServices(this._Logger);
                _Client.SetEndPoint(EndPoint);
                //if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                //    return Response;
                Response = _Client.GetProducts();

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.Result.AddException(ex);
            }
            return Response;
        }
        public ListCategoriasResponseDTO GetCategorias(string EndPoint)
        {
            ListCategoriasResponseDTO Response = new ListCategoriasResponseDTO();
            try
            {
                ClientApiServices _Client = new ClientApiServices(this._Logger);
                _Client.SetEndPoint(EndPoint);
                //if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                //    return Response;
                Response = _Client.GetCategorias();

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.Result.AddException(ex);
            }
            return Response;
        }
        public OperationResult SaveProducto(string EndPoint, SaveProductoRequestDTO _Request)
        {
            OperationResult Response = new OperationResult();
            try
            {
                ClientApiServices _Client = new ClientApiServices(this._Logger);
                _Client.SetEndPoint(EndPoint);
                //if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                //    return Response;
                Response = _Client.SaveProducto(_Request);

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.AddException(ex);
            }
            return Response;
        }
        public OperationResult UpdateProducto(string EndPoint, UpdateProductoRequestDTO _Request)
        {
            OperationResult Response = new OperationResult();
            try
            {
                ClientApiServices _Client = new ClientApiServices(this._Logger);
                _Client.SetEndPoint(EndPoint);
                //if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                //    return Response;
                Response = _Client.UpdateProducto(_Request);

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.AddException(ex);
            }
            return Response;
        }
        public OperationResult DeleteProduct(string EndPoint, int _IdProducto)
        {
            OperationResult Response = new OperationResult();
            try
            {
                ClientApiServices _Client = new ClientApiServices(this._Logger);
                _Client.SetEndPoint(EndPoint);
                //if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                //    return Response;
                Response = _Client.DeleteProduct(_IdProducto);

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.AddException(ex);
            }
            return Response;
        }
        public OperationResult SaveCategoria(string EndPoint, string _Descripcion)
        {
            OperationResult Response = new OperationResult();
            try
            {
                ClientApiServices _Client = new ClientApiServices(this._Logger);
                _Client.SetEndPoint(EndPoint);
                //if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                //    return Response;
                Response = _Client.SaveCategoria(_Descripcion);

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.AddException(ex);
            }
            return Response;
        }
        public OperationResult UpdateCategoria(string EndPoint, UpdateCategoriaRequestDTO _Request)
        {
            OperationResult Response = new OperationResult();
            try
            {
                ClientApiServices _Client = new ClientApiServices(this._Logger);
                _Client.SetEndPoint(EndPoint);
                //if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                //    return Response;
                Response = _Client.UpdateCategoria(_Request);

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.AddException(ex);
            }
            return Response;
        }
        public OperationResult DeleteCategoria(string EndPoint, int _IdCategoria)
        {
            OperationResult Response = new OperationResult();
            try
            {
                ClientApiServices _Client = new ClientApiServices(this._Logger);
                _Client.SetEndPoint(EndPoint);
                //if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                //    return Response;
                Response = _Client.DeleteCategoria(_IdCategoria);

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.AddException(ex);
            }
            return Response;
        }
        public OperationResult GetVentaByNroDocumento(string EndPoint, string _NroDocumento)
        {
            OperationResult Response = new OperationResult();
            try
            {
                ClientApiServices _Client = new ClientApiServices(this._Logger);
                _Client.SetEndPoint(EndPoint);
                //if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                //    return Response;
                Response = _Client.GetVentaByNroDocumento(_NroDocumento);

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.AddException(ex);
            }
            return Response;
        }
        public RegistroVentaResponseDTO RegisterVentas(string EndPoint, SaveVentaRequestDTO _Request)
        {
            RegistroVentaResponseDTO Response = new RegistroVentaResponseDTO();
            try
            {
                ClientApiServices _Client = new ClientApiServices(this._Logger);
                _Client.SetEndPoint(EndPoint);
                //if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                //    return Response;
                Response = _Client.RegisterVentas(_Request);

            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.Result.AddException(ex);
            }
            return Response;
        }
        
    }
}
