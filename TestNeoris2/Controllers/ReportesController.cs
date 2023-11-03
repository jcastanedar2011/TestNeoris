using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestNeoris2.Models;

namespace TestNeoris2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : Controller
    {
        private readonly TestNeorisContext _context;

        public ReportesController(TestNeorisContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Reporte> Get([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            string sql = "EXEC GetReporte @FechaInicial='" + fechaInicio + "', @FechaFinal='" + fechaFin + "'";
            IQueryable<Reporte> result = _context.Reporte.FromSqlRaw("EXEC GetReporte @FechaInicial='" + fechaInicio.Month + "-" + fechaInicio.Day + "-" + fechaInicio.Year + "', @FechaFinal='" + fechaFin.Month + "-" + fechaFin.Day + "-" +fechaFin.Year + "'");

            //var testNeorisContext = _context.Movimientos.Where(x => x.Fecha >= fechaInicio && x.Fecha <= fechaFin).Include(c => c.Cuentas);
            return result.ToArray();
        }
    }
}
