using backend.Context;
using backend.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class CurriculoRepository(AppDbContext context, IValidator<CurriculoInput> validator) : ICurriculoRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IValidator<CurriculoInput> _validator = validator;
        public async Task Adicionar(CurriculoInput input)
        {
            var bytes = ToByteArray(input.Arquivo);
            string nomeArquivo = input.Arquivo.FileName;

            ValidarInput(input);

            var curriculo = new Curriculo
            { 
                Nome = input.Nome,
                Email = input.Email,
                Telefone = input.Telefone,
                CargoDesejado = input.CargoDesejado,
                Escolaridade = input.Escolaridade,
                Observacao = input.Observacao,
                DataCriado = DateTime.Now,
                Arquivo = bytes,
                NomeArquivo = nomeArquivo,

            };

            await _context.Curriculos.AddAsync(curriculo);
            await _context.SaveChangesAsync();
        }

        public static byte[] ToByteArray(IFormFile file)
        {
            using var ms = new MemoryStream();
            using var stream = file.OpenReadStream();
            stream.Position = 0;
            stream.CopyTo(ms);
            return ms.ToArray();
        }


        public async Task<List<Curriculo>> Listar()
        {
            var curriculos = await _context.Curriculos.ToListAsync();
            return curriculos;
        }

        private void ValidarInput(CurriculoInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
