using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fretefy.Test.Domain.ViewModels.Response
{
    public class ErrorViewModel
    {
        public ErrorViewModel() 
        {
            
        }
        public ErrorViewModel(ModelStateDictionary modelState)
        {
            
            foreach (var key in modelState.Keys)
            {
                var errors = modelState[key].Errors;
                Erros = new List<FieldError>();
                foreach (var error in errors)
                {
                    Erros.Add(new FieldError
                    {
                        Campo = key,
                        Erro = error.ErrorMessage
                    });
                }
            }

        }
        public string Mensagem { get; set; } = "Ocorreram erros de validacao.";
        public List<FieldError> Erros { get; set; } = new List<FieldError>();
    }

    public class FieldError
    {
        public string Campo { get; set; }
        public string Erro { get; set; }
    }

}
    
