using backend.Models;

namespace backend.Repositories
{
    public interface ICurriculoRepository
    {
        Task Adicionar(CurriculoInput input);
        Task<List<Curriculo>> Listar();
    }
}
