using backend.Context;
using backend.Models;
using backend.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace backend.Tests
{
    public class CurriculoRepositoryTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        private IFormFile GetTestFile()
        {
            var content = "Teste de conteúdo do arquivo";
            var fileName = "teste.pdf";
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));

            return new FormFile(stream, 0, stream.Length, "Arquivo", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };
        }

        private IValidator<CurriculoInput> GetMockValidator()
        {
            var mock = new Mock<IValidator<CurriculoInput>>();
            mock.Setup(v => v.Validate(It.IsAny<CurriculoInput>()))
                .Returns(new ValidationResult()); // sempre válido
            return mock.Object;
        }

        [Fact]
        public async Task Adicionar_DeveAdicionarCurriculo()
        {
            // Arrange
            var context = GetDbContext();
            var repo = new CurriculoRepository(context, GetMockValidator());

            var input = new CurriculoInput
            {
                Nome = "Lucas Nithael",
                Email = "lucas@test.com",
                Telefone = "123456789",
                CargoDesejado = "Desenvolvedor",
                Escolaridade = "Superior",
                Observacao = "Teste",
                Arquivo = GetTestFile()
            };

            // Act
            await repo.Adicionar(input);
            var curriculos = await repo.Listar();

            // Assert
            Assert.Single(curriculos);
            Assert.Equal("Lucas Nithael", curriculos[0].Nome);
            Assert.Equal("teste.pdf", curriculos[0].NomeArquivo);
        }

        [Fact]
        public async Task Listar_DeveRetornarTodosCurriculos()
        {
            // Arrange
            var context = GetDbContext();
            var repo = new CurriculoRepository(context, GetMockValidator());

            await repo.Adicionar(new CurriculoInput
            {
                Nome = "Lucas",
                Email = "lucas@test.com",
                Telefone = "123",
                CargoDesejado = "Dev",
                Escolaridade = "Superior",
                Arquivo = GetTestFile()
            });

            await repo.Adicionar(new CurriculoInput
            {
                Nome = "Maria",
                Email = "maria@test.com",
                Telefone = "456",
                CargoDesejado = "Designer",
                Escolaridade = "Superior",
                Arquivo = GetTestFile()
            });

            // Act
            var curriculos = await repo.Listar();

            // Assert
            Assert.Equal(2, curriculos.Count);
        }
    }
}
