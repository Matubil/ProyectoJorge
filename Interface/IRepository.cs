using DapperApi2022.Models;
using DapperApi2022.Models.Dto;
using DapperApi2022.Repositorio;

namespace DapperApi2022.Interface
{
    public interface IRepository
    {
        public Task<IEnumerable<DepositoModel>> GetCompanies();
        public Task<IEnumerable<PruebaModel>> pruebitaMia();
        public Task<IEnumerable<prueba>> TodosDocumentos();
        public Task<List<Afiliado>> GetAllAfilia();
        public Task<List<int>> GetMatriculasDeudor();
        public Task<List<int>> GetAllPeriodos(int matricula);
        public Task<List<DeudorDto>> GetlAllConceptosPeriodo(int matricula, int periodo);
        public Task<List<int>> GetMatriculas();
        public Task<int> CountConceptosPagosPorPeriodo(int matricula, int periodo);





    }
}
