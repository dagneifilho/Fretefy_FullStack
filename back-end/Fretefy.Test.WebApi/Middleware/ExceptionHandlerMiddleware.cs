using System;
using System.Net;
using Fretefy.Test.Domain.Exceptions;
using Fretefy.Test.Domain.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Fretefy.Test.WebApi.Middleware
{
    public class ExceptionHandlerMiddleware : BaseMiddleware
    {
        public ExceptionHandlerMiddleware(RequestDelegate next) : base(next){}

        public override (HttpStatusCode code, dynamic message) GetResponse(Exception exception)
        {   
            ErrorViewModel vm = new ErrorViewModel();
            switch (exception)
            {
                case CidadeInexistenteException cie:
                    var cidadesInexistentes = string.Join(",", cie.IdsCidades);
                    vm.Erros.Add(new FieldError{
                        Campo = cie.Campo,
                        Erro = "As seguites cidades nao existem: " + cidadesInexistentes
                    });
                    return (HttpStatusCode.BadRequest, vm);
                case CidadesJaCadastradasException cjce:
                    var cidadesExistentes = string.Join(",", cjce.IdsCidades);
                    vm.Erros.Add(new FieldError{
                        Campo = cjce.Campo,
                        Erro = "As seguites cidades nao existem: " + cidadesExistentes
                    });
                    return (HttpStatusCode.BadRequest, vm);
                case RegiaoExistenteException ree:
                    vm.Erros.Add(new FieldError{
                        Campo = ree.Campo,
                        Erro = $"A regiao informada ja existe."
                    });
                    return (HttpStatusCode.BadRequest, vm);
                case RegiaoInexistenteException rie:
                    vm.Erros.Add(new FieldError{
                        Campo = rie.Campo,
                        Erro = $"A regiao com o id {rie.Id} nao existe."
                    });
                    return (HttpStatusCode.BadRequest, vm);
                default:
                    vm.Mensagem = exception.Message;
                    return (HttpStatusCode.InternalServerError, vm);
                
            }
        }
    }

}

