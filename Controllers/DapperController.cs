using DapperApi2022.Repositorio;
using Microsoft.AspNetCore.Mvc;
using DapperApi2022.Interface;

namespace DapperApi2022.Controllers
{
    [Route("api/dapper")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        private readonly IRepository _companyRepo;

        public DapperController(IRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyRepo.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("yoyo")]
        public async Task<IActionResult> TodosDocumentos()
        {
            try
            {
                var companies = await _companyRepo.TodosDocumentos();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("paciente")]
        public async Task<IActionResult> pruebitaMia()
        {
            try
            {
                var companies = await _companyRepo.pruebitaMia();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        #region GetAllAfilia
        [HttpGet("GetAfilia")]
        public async Task<IActionResult> GetAllAfilia()
        {
            try
            {
                var afiliados = await _companyRepo.GetAllAfilia();
                return Ok(afiliados);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        #endregion


        #region GetAllDeudores
        [HttpGet("GetDeudores")]
        public async Task<IActionResult> GetMatriculaDeudor()
        {
            try
            {
                var deudores = await _companyRepo.GetMatriculasDeudor();
                return Ok(deudores);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region calcularDeudores
        [HttpGet("GetCantDeudoresJorge")]
        public async Task<IActionResult> getDeudores()
        {
            try
            {
                var listaMatriculas = await _companyRepo.GetMatriculasDeudor();
                int cantPeriodosImp;
                int indicePeriodo;
                int periodoActual;
                int cantConceptos;
                int conceptosImpagos;
                int cantDeudores=0;
                var listaMatriculasDeDeudores = new List<int>();
                foreach (var matriculaActual in listaMatriculas)
                {
                    cantPeriodosImp = 0;
                    indicePeriodo = 0;
                    var listaPeriodo = await _companyRepo.GetAllPeriodos(matriculaActual);
                    listaPeriodo = listaPeriodo.Select(x => Convert.ToInt32(x)).ToList();
                    periodoActual = listaPeriodo[indicePeriodo];
                    while ((cantPeriodosImp < 13) && (periodoActual < 202212))
                    {
                        var listaConceptos = await _companyRepo.GetlAllConceptosPeriodo(matriculaActual, periodoActual);

                        cantConceptos = 0;
                        conceptosImpagos = 0;
                        foreach (var conceptoActual in listaConceptos)
                        {
                            cantConceptos++;
                            if (conceptoActual.dorden == 99999999)
                            {
                                conceptosImpagos++;
                            }
                        }

                        if (cantConceptos == conceptosImpagos)
                        {
                            cantPeriodosImp++;
                        }
                        indicePeriodo++;
                        periodoActual = listaPeriodo[indicePeriodo];
                    }

                    if ((0 < cantPeriodosImp)&&(cantPeriodosImp <= 12))
                    {
                        listaMatriculasDeDeudores.Add(matriculaActual);
                        cantDeudores++;
                    }
                }
                var resultado = new ResultadoDeudores
                {
                    MatriculasDeDeudores = listaMatriculasDeDeudores,
                    CantidadDeDeudores = cantDeudores
                };
                return Ok(resultado);

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region calcularDeudores2.0
        [HttpGet("GetCantDeudoresJorgeVersion2")]
        public async Task<IActionResult> getAfiliadosDeudores()
        {
            try
            {
                var listaMatriculas = await _companyRepo.GetMatriculas(); ;
                int cantPeriodosImp;
                int indicePeriodo;
                int periodoActual;
                int cantConceptos;
                int conceptosImpagos;
                int cantDeudores = 0;
                var listaMatriculasDeDeudores = new List<int>();
                foreach (var matriculaActual in listaMatriculas)
                {
                    cantPeriodosImp = 0;
                    indicePeriodo = 0;
                    var listaPeriodo = await _companyRepo.GetAllPeriodos(matriculaActual);
                    if (listaPeriodo.Count() > 0)
                    {
                        listaPeriodo = listaPeriodo.Select(x => Convert.ToInt32(x)).ToList();
                        periodoActual = listaPeriodo[indicePeriodo];
                        int cantConceptosPagos = 0;
                        while ((cantPeriodosImp < 13) && (periodoActual < 202212) && (indicePeriodo < listaPeriodo.Count() - 1))
                        {
                            cantConceptosPagos = await _companyRepo.CountConceptosPagosPorPeriodo(matriculaActual, periodoActual);

                            if (cantConceptosPagos == 0)
                            {
                                cantPeriodosImp++;
                            }
                            indicePeriodo++;
                            periodoActual = listaPeriodo[indicePeriodo];
                        }
                        if ((0 < cantPeriodosImp) && (cantPeriodosImp <= 12))
                        {
                            cantDeudores++;
                            listaMatriculasDeDeudores.Add(matriculaActual);
                        }
                    }
                }
                var resultado = new ResultadoDeudores
                {
                    CantidadDeDeudores = cantDeudores,
                    MatriculasDeDeudores = listaMatriculasDeDeudores
                };
                return Ok(resultado);

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}

