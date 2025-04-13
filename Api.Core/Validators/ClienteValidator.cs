using Api.Core.Dtos;
using FluentValidation;
using System.IO;
using System.Linq;

namespace Api.Core.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(cliente => cliente.TipoDocumento)
                .Cascade(CascadeMode)
                .NotEmpty().WithMessage("Tipo de Documento es requerido");

            RuleFor(cliente => cliente.TipoImpuesto)
             .Cascade(CascadeMode)
             .NotEmpty().WithMessage("Condicion Iva es requerido");

            RuleFor(cliente => cliente.NumeroDeDocumento)
                .Cascade(CascadeMode)
                .NotEmpty().WithMessage("Numero de Documento es requerido");
        }
    }
}
