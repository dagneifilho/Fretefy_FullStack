using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Fretefy.Test.Domain.ViewModels.Request
{
    public class RegiaoPostViewModel : IValidatableObject
    {
        public string Nome {get;set;}
        public IEnumerable<Guid> CidadesIds {get;set;}
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrWhiteSpace(Nome))
                yield return new ValidationResult("O campo Nome é obrigatório!",new List<string>{"Nome"});
            if(Nome.Length > 1024) 
                yield return new ValidationResult("O campo Nome deve conter no máximo 1024 caracteres.",new List<string>{"Nome"});
            if(CidadesIds == null || CidadesIds.Count() == 0)
                yield return new ValidationResult("É necessário informar ao menos uma cidade.",new List<string>{"CidadesId"});
        }
    }
}
