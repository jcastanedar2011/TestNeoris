using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestNeoris.Models;

namespace TestNeoris.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        private readonly TestNeorisContext _context;
        public ClientesController(TestNeorisContext context)
        {
            _context = context;
        }


        // GET: api/<ClientesController>
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            var testNeorisContext = _context.Clientes.Include(c => c.Persona);
            return testNeorisContext.ToArray();
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public IEnumerable<Cliente> Get(int id)
        {
            var testNeorisContext = _context.Clientes.Where(x => x.Clienteid == id).Include(c => c.Persona);
            return testNeorisContext.ToArray();
        }

        // POST api/<ClientesController>
        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            // Verifica si ya existe un cliente con el mismo ClienteId
            if (_context.Clientes.Any(c => c.PersonaId == cliente.PersonaId))
            {
                return Conflict("La persona seleccionada ya tiene otro cliente asignado.");
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return CreatedAtAction("Get", new { id = cliente.Clienteid }, cliente);
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Clienteid)
            {
                return BadRequest();
            }

            // Verifica si existe un cliente con el mismo ClienteId
            if (_context.Clientes.Any(c => c.Clienteid != cliente.Clienteid && c.PersonaId == cliente.PersonaId))
            {
                return Conflict("La persona seleccionada ya tiene otro cliente asignado.");
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(c => c.Clienteid == id);
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return NoContent();
        }
    }
}