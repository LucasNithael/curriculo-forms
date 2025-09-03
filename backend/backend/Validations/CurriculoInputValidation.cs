using backend.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace backend.Validations
{
    public class CurriculoInputValidation : AbstractValidator<CurriculoInput>
    {
        public CurriculoInputValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome pode ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email é inválido.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
                .WithMessage("O telefone informado não é válido.");

            RuleFor(x => x.CargoDesejado)
                .NotEmpty().WithMessage("O cargo desejado é obrigatório.");

            RuleFor(x => x.Escolaridade)
                .NotEmpty().WithMessage("A escolaridade é obrigatória.");

            RuleFor(x => x.Arquivo)
                .NotNull().WithMessage("O arquivo é obrigatório.")
                .Must(BeAValidFileType).WithMessage("O arquivo deve ser PDF, DOC ou DOCX.")
                .Must(f => f.Length <= 1 * 1024 * 1024).WithMessage("O arquivo não pode ter mais de 1MB.");
        }

        private bool BeAValidFileType(IFormFile file)
        {
            if (file == null) return false;

            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(extension);
        }
    }
}
