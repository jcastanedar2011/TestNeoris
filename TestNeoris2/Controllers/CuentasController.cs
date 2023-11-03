using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text.Json;
using TestNeoris2.Models;

namespace TestNeoris2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : Controller
    {
        private readonly TestNeorisContext _context;

        public CuentasController(TestNeorisContext context)
        {
            _context = context;
        }
        // GET: api/<CuentasController>
        [HttpGet]
        public IEnumerable<Cuentas> Get()
        {
            var testNeorisContext = _context.Cuenta.Include(c => c.Cliente); ;
            return testNeorisContext.ToArray();
        }


        // GET api/<CuentasController>/5
        [HttpGet("{id}")]
        public IEnumerable<Cuentas> Get(int id)
        {
            var testNeorisContext = _context.Cuenta.Where(x => x.NumeroCuenta == id).Include(c => c.Cliente); ;
            return testNeorisContext.ToArray();
        }


        // POST api/<CuentasController>
        [HttpPost]
        public IActionResult Post([FromBody] Cuentas cuenta)
        {
            // Verifica si ya existe una cuenta con el mismo numero
            if (_context.Cuenta.Any(c => c.NumeroCuenta == cuenta.NumeroCuenta))
            {
                return Conflict("El numero de cuenta ya existe");
            }

            _context.Cuenta.Add(cuenta);
            _context.SaveChanges();

            return CreatedAtAction("Get", new { id = cuenta.NumeroCuenta }, cuenta);
        }

        // PUT api/<CuentasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cuentas cuenta)
        {
            if (id != cuenta.NumeroCuenta)
            {
                return BadRequest();
            }

            _context.Entry(cuenta).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                var url = ClienteExists(cuenta.Clienteid);
                if (!url.IsCompleted)
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

        public async Task<IActionResult> ClienteExists(int? id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7022/");
            //HttpResponseMessage response = await client.PostAsJsonAsync("api/Clientes/Clientes", id);
            var response = await client.GetAsync("api/Clientes/" + id);
            if (response.IsSuccessStatusCode)
            {
                // Procesar la respuesta
                var content = await response.Content.ReadAsStringAsync();
                if (!string.Equals(content, "[]"))
                {
                    cliente: var data = JsonSerializer.Deserialize<Cliente>(content);
                }
                else {
                    return NotFound();
                }
                    
                return Ok(content);
            }
            return BadRequest();
        }

        // DELETE api/<CuentasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cuenta = _context.Cuenta.Find(id);

            if (cuenta == null)
            {
                return NotFound();
            }

            _context.Cuenta.Remove(cuenta);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
