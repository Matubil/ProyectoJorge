using Dapper;
using DapperApi2022.Context;
using DapperApi2022.Models;
using DapperApi2022.Interface;
using DapperApi2022.Models.Dto;


namespace DapperApi2022.Repositorio
{
    public class Repository : IRepository
    {
        private readonly DapperContext _context;

        public Repository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepositoModel>> GetCompanies()
        {

            var query = @$"SELECT * 
                            FROM w_transferencias 
                                WHERE transf_titular 
                                    LIKE 'Zubcar S.A.' ORDER BY inter_id DESC";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<DepositoModel>(query);
                return companies.ToList();
            }
        }

        public async Task<IEnumerable<prueba>> TodosDocumentos()
        {
            var query = @$"SELECT DISTINCT af.Matricula, af.Tipo, a.lqperiodo 
                                FROM  APORTE_LIQUIDACION a 
                                    INNER JOIN AFILIA af 
                                        ON a.lqmatricula = af.Matricula 
                                            WHERE lqperiodo  
                                                BETWEEN  '202212' AND '202212' AND Baja = 'N' AND Tipo 
                                                    IN('H', 'I', 'R', 'D')";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<prueba>(query);
                return companies.ToList();
            }
        }


        public async Task<IEnumerable<PruebaModel>> pruebitaMia()
        {

            var query = "SELECT * FROM prueba";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<PruebaModel>(query);
                return companies.ToList();
            }
        }

        #region GetAllAfilia
        public async Task<List<Afiliado>> GetAllAfilia()
        {
            using (var conexion = _context.CreateConnection())
            {
                var sql =
                    $@"SELECT Matricula, Nombres, Apellido FROM AFILIA WHERE Matricula = 16591";

                var resultado = await conexion.QueryAsync<Afiliado>(sql);

                return resultado.ToList();
            }
        }
        #endregion

        #region GetMatriculasDeudor
        public async Task<List<int>> GetMatriculasDeudor()
        {
            using (var conexion = _context.CreateConnection())
            {
                var sql =
                    $@"SELECT DISTINCT ap.dmatricula
                            FROM APORTE_DEUDAS ap 
                            INNER JOIN AFILIA a ON ap.dmatricula = a.Matricula
                            INNER JOIN (SELECT dmatricula, MIN(CAST(dperiodo AS INT)) as dperiodo_min,
                             MAX(CAST(dperiodo AS INT)) as dperiodo_max FROM APORTE_DEUDAS
                            GROUP BY dmatricula) agrupado ON ap.dmatricula = agrupado.dmatricula 
                            WHERE a.Baja = 'N' AND a.Tipo = 'A' 
                            AND ap.dorden = 99999999";

                var resultado = await conexion.QueryAsync<int>(sql);

                return resultado.ToList();
            }
        }
        #endregion

        #region GetAllPeriodos
        public async Task<List<int>> GetAllPeriodos(int matricula)
        {
            using (var conexion = _context.CreateConnection())
            {
                var sql =
                    $@"SELECT DISTINCT(dperiodo) 
                            FROM APORTE_DEUDAS 
                                WHERE dmatricula= {matricula}";

                var resultado = await conexion.QueryAsync<int>(sql);

                return resultado.ToList();
            }
        }
        #endregion

        #region GetlAllConceptosPeriodo
        public async Task<List<DeudorDto>> GetlAllConceptosPeriodo(int matricula, int periodo)
        {
            using (var conexion = _context.CreateConnection())
            {
                var sql =
                    $@"SELECT dconcepto, dorden
                            FROM APORTE_DEUDAS 
                                WHERE dmatricula = {matricula} AND dperiodo = '{periodo}'";

                var resultado = await conexion.QueryAsync<DeudorDto>(sql);

                return resultado.ToList();
            }
        }
        #endregion


    }
}
