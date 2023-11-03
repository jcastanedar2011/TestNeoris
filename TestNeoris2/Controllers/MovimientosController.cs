using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestNeoris2.Models;

namespace TestNeoris2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : Controller
    {
        private readonly TestNeorisContext _context;
        public MovimientosController(TestNeorisContext context)
        {
            _context = context;
        }
        // GET: api/<MovimientosController>
        [HttpGet]
        public IEnumerable<Movimiento> Get()
        {
            var testNeorisContext = _context.Movimientos;
            return testNeorisContext.ToArray();
        }


        // GET: api/<MovimientosController>
        [HttpGet("{id}")]
        public IEnumerable<Movimiento> Get(int id)
        {
            var testNeorisContext = _context.Movimientos.Where(x => x.MovimientoId == id);
            return testNeorisContext.ToArray();
        }

        // POST api/<MovimientosController>
        [HttpPost]
        public IActionResult Post([FromBody] Movimiento movimiento)
        {
            movimiento.Fecha = DateTime.Now;
            if (movimiento.Valor < 0)
            {
                return Conflict("no se pueden hacer movimientos con valores negativos.");
            }
            else
            {
                movimiento.Saldo = Actualiza_Saldo(movimiento);
                _context.Movimientos.Add(movimiento);
                _context.SaveChanges();
            }

            return CreatedAtAction("Get", new { id = movimiento.MovimientoId }, movimiento);
        }

        public decimal? Actualiza_Saldo(Movimiento movimiento) {
            var movimientos = _context.Movimientos.Where(x => x.NumeroCuenta == movimiento.NumeroCuenta);
            decimal ultimo_saldo = movimientos.OrderBy(x => x.MovimientoId).LastOrDefault()?.Saldo ?? 0;
            if (ultimo_saldo == 0)
            { 
                ultimo_saldo = _context.Cuenta.Where(x => x.NumeroCuenta == movimiento.NumeroCuenta).FirstOrDefault().SaldoInicial.Value;
            }

            if (ultimo_saldo < movimiento.Valor && movimiento.TipoMovimiento == "D")
            {
                return -1;
            }
            else
            {
                if (movimiento.TipoMovimiento == "D")
                {
                    movimiento.Saldo = ultimo_saldo - movimiento.Valor;
                }
                else if (movimiento.TipoMovimiento == "C")
                {
                    movimiento.Saldo = ultimo_saldo + movimiento.Valor;
                }
            }
            return movimiento.Saldo;
        }


        // PUT api/<MovimientosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movimiento movimiento)
        {
            if (id != movimiento.MovimientoId)
            {
                return BadRequest();
            }

            _context.Entry(movimiento).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(c => c.MovimientoId == id);
        }

        // DELETE api/<MovimientosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movimiento = _context.Movimientos.Find(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimiento);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
