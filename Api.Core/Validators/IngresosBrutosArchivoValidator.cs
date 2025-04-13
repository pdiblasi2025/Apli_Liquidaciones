using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Validators
{
    public class IngresosBrutosArchivoValidator : AbstractValidator<Dtos.IngresosBrutosArchivoPost>
    {
        private int _maxFileLength = 2097152; // 2 MB
        private string[] _filePermittedExtensions = { ".pdf" };
        public IngresosBrutosArchivoValidator()
        {
            RuleFor(x => x.Archivo)
                .Must(z => z.Length > 0 && z.Length < _maxFileLength).WithMessage("No se puede subir un archivo de más de 2 MB")
                .Must(z => _filePermittedExtensions.Contains(Path.GetExtension(z.FileName).ToLower())).WithMessage("Solo se puede subir archivos PDF");
        }
    }
}
