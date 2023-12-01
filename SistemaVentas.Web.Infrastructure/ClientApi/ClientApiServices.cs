using Api.Web.WebApi.DTO.OperationResult;
using Api.Web.WebApi.DTO.Request;
using Api.Web.WebApi.DTO.Response;
using Api.Web.WebApi.Utilities.ApiServices;
using Api.Web.WebApi.Utilities.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Web.Infrastructure.ClientApi
{
    public  class ClientApiServices
    {
        private string Token;
        private string UrlEndPoint;
        private Logger _Logger;
        public ClientApiServices(Logger Logger)
        {
            this.Token = string.Empty;
            this.UrlEndPoint = string.Empty;
            this._Logger = Logger;
        }
        public bool HasToken()
        {
            return !string.IsNullOrEmpty(this.Token);
        }
        public bool HasEndPoint()
        {
            return !string.IsNullOrEmpty(this.UrlEndPoint);
        }

        public void SetEndPoint(string UrlEndPoint)
        {
            this.UrlEndPoint = UrlEndPoint;
        }
        //public OperationResult SetToken(ClaimsIdentity ClaimsIdentity)
        //{
        //    OperationResult _OperationResult = new OperationResult();
        //    try
        //    {
        //        var _Claim = ClaimsIdentity.FindFirst(x => x.Type == EnumClaims.AccessToken.ToString());
        //        if (_Claim == null)
        //        {
        //            _OperationResult.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
        //            _OperationResult.AddException(new Exception("No se encontro el Token de Autorización."));
        //            return _OperationResult;
        //        }
        //        this.Token = _Claim.Value;
        //        if (string.IsNullOrEmpty(this.Token))
        //        {
        //            _OperationResult.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
        //            _OperationResult.AddException(new Exception("No se encontro el Token de Autorización."));
        //            return _OperationResult;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _OperationResult.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
        //        _OperationResult.AddException(ex);
        //    }
        //    return _OperationResult;
        //}
        //public OperationResult GetToken(string UserName, string Password)
        //{
        //    OperationResult _OperationResult = new OperationResult();
        //    MessageFactory _MessageFactory = new MessageFactory(this._Logger);
        //    var _Response = _MessageFactory.GetToken(this.UrlEndPoint, "/Token", UserName, Password, HttpMethod.Post);
        //    if (_Response == null)
        //    {
        //        _OperationResult.SetStatusCode(OperationResult.StatusCodesEnum.SERVICE_UNAVAILABLE);
        //        _OperationResult.AddException(new Exception("No se obtuvo respuesta de la API Identity."));
        //        return _OperationResult;
        //    }
        //    this.Token = _Response.AccessToken;
        //    return _OperationResult;
        //}
        public ListProductsResponseDTO GetProducts()
        {
            var Response = new ListProductsResponseDTO();
            //if (!this.HasToken())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
            //    return Response;
            //}
            //else if (!this.HasEndPoint())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
            //    return Response;
            //}

            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            Response = _MessageFactory.SendRequest<ListProductsResponseDTO>(this.UrlEndPoint, "GetProducts", string.Empty, HttpMethod.Get);

            return Response;
        }
        //  public T SendRequest<T>(string EndPointUrl, string Action, string Payload, HttpMethod Method)
        //  {
        //      string _Response = string.Empty;
        //      DateTime dt1 = DateTime.Now;
        //      for (int i = 0; i < 4; i++)
        //      {
        //          ServicePointManager.Expect100Continue = false;
        //          ServicePointManager.DefaultConnectionLimit = 9999;
        //          System.Net.ServicePointManager.SecurityProtocol =
        //SecurityProtocolType.Tls |
        //SecurityProtocolType.Tls11 |
        //SecurityProtocolType.Tls12;

        //          using (HttpClient _HttpClient = new HttpClient())
        //          using (HttpRequestMessage _HttpRequestMessage = new HttpRequestMessage(Method, EndPointUrl + Action))
        //          {
        //              _HttpClient.DefaultRequestHeaders.Clear();
        //              _HttpClient.MaxResponseContentBufferSize = 2147483647;
        //              _HttpClient.Timeout = TimeSpan.FromMilliseconds(5400000);

        //              // Agrega la autorización como "Basic" con el valor del API KEY
        //              //_HttpClient.DefaultRequestHeaders.Add("API-KEY", apiKey);

        //              // Configura el contenido JSON en el cuerpo de la solicitud
        //              _HttpRequestMessage.Content = new StringContent(Payload, Encoding.UTF8, "application/json");
        //              using (HttpResponseMessage _HttpResponseMessage = _HttpClient.SendAsync(_HttpRequestMessage).Result)
        //              {
        //                  if (!_HttpResponseMessage.IsSuccessStatusCode)
        //                  {
        //                      switch (_HttpResponseMessage.StatusCode)
        //                      {
        //                          case System.Net.HttpStatusCode.BadRequest:
        //                              Console.WriteLine(((int)_HttpResponseMessage.StatusCode).ToString() + " " + _HttpResponseMessage.ReasonPhrase + " " + _HttpResponseMessage.Content.ReadAsStringAsync().Result);
        //                              Console.WriteLine(EndPointUrl);
        //                              Console.WriteLine(Payload);
        //                              throw new Exception(((int)_HttpResponseMessage.StatusCode).ToString() + " " + _HttpResponseMessage.ReasonPhrase + " " + _HttpResponseMessage.Content.ReadAsStringAsync().Result);
        //                          case System.Net.HttpStatusCode.Unauthorized:
        //                          case System.Net.HttpStatusCode.NotFound:
        //                              throw new Exception(((int)_HttpResponseMessage.StatusCode).ToString() + " " + _HttpResponseMessage.ReasonPhrase + " " + _HttpResponseMessage.Content.ReadAsStringAsync().Result);
        //                          default:
        //                              TimeSpan span = (DateTime.Now) - dt1;
        //                              Console.WriteLine("Execution time: " + span.TotalMilliseconds.ToString() + " milliseconds, retried " + i.ToString() + " times.");
        //                              Console.WriteLine(EndPointUrl);
        //                              Console.WriteLine(Payload);
        //                              if (i >= 3)
        //                              {
        //                                  Console.WriteLine(((int)_HttpResponseMessage.StatusCode).ToString() + " " + _HttpResponseMessage.ReasonPhrase + " " + _HttpResponseMessage.Content.ReadAsStringAsync().Result);
        //                                  Console.WriteLine(EndPointUrl);
        //                                  Console.WriteLine(Payload);
        //                                  throw new Exception(((int)_HttpResponseMessage.StatusCode).ToString() + " " + _HttpResponseMessage.ReasonPhrase + " " + _HttpResponseMessage.Content.ReadAsStringAsync().Result);
        //                              }
        //                              break;
        //                      }
        //                      continue;
        //                  }
        //                  _Response = _HttpResponseMessage.Content.ReadAsStringAsync().Result;
        //                  break;
        //              }
        //          }
        //      }
        //      return Deserialize<T>(_Response);
        //  }
        //  public static T Deserialize<T>(string SerializedString)
        //  {
        //      T o = JsonConvert.DeserializeObject<T>(SerializedString);
        //      return o;
        //  }
        public ListCategoriasResponseDTO GetCategorias()
        {
            var Response = new ListCategoriasResponseDTO();
            //if (!this.HasToken())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
            //    return Response;
            //}
            //else if (!this.HasEndPoint())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
            //    return Response;
            //}

            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            Response = _MessageFactory.SendRequest<ListCategoriasResponseDTO>(this.UrlEndPoint, "GetCategorias", string.Empty, HttpMethod.Get);

            return Response;
        }
        public OperationResult SaveProducto(SaveProductoRequestDTO _Request)
        {
            var Response = new OperationResult();
            //if (!this.HasToken())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
            //    return Response;
            //}
            //else if (!this.HasEndPoint())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
            //    return Response;
            //}
            if (_Request == null)
            {
                Response.SetStatusCode(OperationResult.StatusCodesEnum.BAD_REQUEST);
                Response.AddException(new Exception("Parámetro inválido."));
                return Response;
            }
            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            string _Payload = Api.Web.WebApi.Utilities.Serializer.JsonSerializer.Serialize(_Request);          
            Response = _MessageFactory.SendRequest<OperationResult>(this.UrlEndPoint, "SaveProducto", _Payload, HttpMethod.Post);

            return Response;
        }
        public OperationResult UpdateProducto(UpdateProductoRequestDTO _Request)
        {
            var Response = new OperationResult();
            //if (!this.HasToken())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
            //    return Response;
            //}
            //else if (!this.HasEndPoint())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
            //    return Response;
            //}
            if (_Request == null)
            {
                Response.SetStatusCode(OperationResult.StatusCodesEnum.BAD_REQUEST);
                Response.AddException(new Exception("Parámetro inválido."));
                return Response;
            }
            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            string _Payload = Api.Web.WebApi.Utilities.Serializer.JsonSerializer.Serialize(_Request);
            Response = _MessageFactory.SendRequest<OperationResult>(this.UrlEndPoint, "UpdateProducto", _Payload, HttpMethod.Post);

            return Response;
        }
        public OperationResult DeleteProduct(int _IdProducto)
        {
            var Response = new OperationResult();
            //if (!this.HasToken())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
            //    return Response;
            //}
            //else if (!this.HasEndPoint())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
            //    return Response;
            //}
            if (_IdProducto == 0)
            {
                Response.SetStatusCode(OperationResult.StatusCodesEnum.BAD_REQUEST);
                Response.AddException(new Exception("Parámetro inválido."));
                return Response;
            }
            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            string _Payload = Api.Web.WebApi.Utilities.Serializer.JsonSerializer.Serialize(_IdProducto);
            Response = _MessageFactory.SendRequest<OperationResult>(this.UrlEndPoint, "DeleteProduct", _Payload, HttpMethod.Post);

            return Response;
        }
        public OperationResult SaveCategoria(string _Descripcion)
        {
            var Response = new OperationResult();
            //if (!this.HasToken())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
            //    return Response;
            //}
            //else if (!this.HasEndPoint())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
            //    return Response;
            //}
            if (!string.IsNullOrEmpty(_Descripcion))
            {
                Response.SetStatusCode(OperationResult.StatusCodesEnum.BAD_REQUEST);
                Response.AddException(new Exception("Parámetro inválido."));
                return Response;
            }
            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            string _Payload = Api.Web.WebApi.Utilities.Serializer.JsonSerializer.Serialize(_Descripcion);
            Response = _MessageFactory.SendRequest<OperationResult>(this.UrlEndPoint, "SaveCategoria", _Payload, HttpMethod.Post);

            return Response;
        }
        public OperationResult UpdateCategoria(UpdateCategoriaRequestDTO _Request)
        {
            var Response = new OperationResult();
            //if (!this.HasToken())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
            //    return Response;
            //}
            //else if (!this.HasEndPoint())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
            //    return Response;
            //}
            if (_Request == null)
            {
                Response.SetStatusCode(OperationResult.StatusCodesEnum.BAD_REQUEST);
                Response.AddException(new Exception("Parámetro inválido."));
                return Response;
            }
            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            string _Payload = Api.Web.WebApi.Utilities.Serializer.JsonSerializer.Serialize(_Request);
            Response = _MessageFactory.SendRequest<OperationResult>(this.UrlEndPoint, "UpdateCategoria", _Payload, HttpMethod.Post);

            return Response;
        }
        public OperationResult DeleteCategoria(int _IdCategoria)
        {
            var Response = new OperationResult();
            //if (!this.HasToken())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
            //    return Response;
            //}
            //else if (!this.HasEndPoint())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
            //    return Response;
            //}
            if (_IdCategoria ==0)
            {
                Response.SetStatusCode(OperationResult.StatusCodesEnum.BAD_REQUEST);
                Response.AddException(new Exception("Parámetro inválido."));
                return Response;
            }
            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            string _Payload = Api.Web.WebApi.Utilities.Serializer.JsonSerializer.Serialize(_IdCategoria);
            Response = _MessageFactory.SendRequest<OperationResult>(this.UrlEndPoint, "DeleteProduct", _Payload, HttpMethod.Post);

            return Response;
        }
        public OperationResult GetVentaByNroDocumento(string _NroDocumento)
        {
            var Response = new OperationResult();
            //if (!this.HasToken())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
            //    return Response;
            //}
            //else if (!this.HasEndPoint())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
            //    return Response;
            //}S
            if ( !String.IsNullOrEmpty(_NroDocumento))
            {
                Response.SetStatusCode(OperationResult.StatusCodesEnum.BAD_REQUEST);
                Response.AddException(new Exception("Parámetro inválido."));
                return Response;
            }
            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            string _Payload = Api.Web.WebApi.Utilities.Serializer.JsonSerializer.Serialize( _NroDocumento);
            Response = _MessageFactory.SendRequest<OperationResult>(this.UrlEndPoint, "GetVentaByNroDocumento", _Payload, HttpMethod.Post);

            return Response;
        }
        public RegistroVentaResponseDTO RegisterVentas(SaveVentaRequestDTO _Request)
        {
            var Response = new RegistroVentaResponseDTO();
            //if (!this.HasToken())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
            //    return Response;
            //}
            //else if (!this.HasEndPoint())
            //{
            //    Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
            //    Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
            //    return Response;
            //}S
            if (_Request == null)
            {
                Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.BAD_REQUEST);
                Response.Result.AddException(new Exception("Parámetro inválido."));
                return Response;
            }
            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            string _Payload = Api.Web.WebApi.Utilities.Serializer.JsonSerializer.Serialize(_Request);
            Response = _MessageFactory.SendRequest<RegistroVentaResponseDTO>(this.UrlEndPoint, "RegisterVentas", _Payload, HttpMethod.Post);

            return Response;
        }
    }
}
