using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestNeoris.Models;

namespace TestNeoris.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : Controller
    {
        private readonly TestNeorisContext _context;
        public PersonasController(TestNeorisContext context)
        {
            _context = context;
        }
        // GET: api/<PersonasController>
        [HttpGet]
        public IEnumerable<Persona> Get()
        {
            var testNeorisContext = _context.Personas;
            return testNeorisContext.ToArray();
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public IEnumerable<Persona> Get(int id)
        {
            var testNeorisContext = _context.Personas.Where(x => x.Id == id);
            return testNeorisContext.ToArray();
        }

        // POST api/<PersonasController>
        [HttpPost]
        public IActionResult Post([FromBody] Persona persona)
        {
            // Verifica si ya existe un cliente con el mismo ClienteId
            if (_context.Personas.Any(c => c.Identificacion == persona.Identificacion))
            {
                return Conflict("Ya existe una persona registrada con el mismo numero de identifiacion");
            }

            _context.Personas.Add(persona);
            _context.SaveChanges();

            return CreatedAtAction("Get", new { id = persona.Id }, persona);
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Persona persona)
        {
            if (id != persona.Id)
            {
                return BadRequest();
            }

            // Verifica si existe una persona con el misno numero de identificacion
            if (_context.Personas.Any(c => c.Id != persona.Id && c.Identificacion == persona.Identificacion))
            {
                return Conflict("La identificacion ya existe para otra persona.");
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
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

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(c => c.Id == id);
        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var persona = _context.Personas.Find(id);

            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            _context.SaveChanges();

            return NoContent();
        }
    }
}