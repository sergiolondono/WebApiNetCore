using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIPrueba.Contexts;
using WebAPIPrueba.Entities;

namespace WebAPIPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LibrosController(ApplicationDbContext context)
        { 
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return _context.Libros
                .Include(l => l.Autor)
                .ToList();
        }

        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = _context.Libros.FirstOrDefault(a => a.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            _context.Add(libro);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id }, libro);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            _context.Entry(libro).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = _context.Libros.FirstOrDefault(a => a.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            _context.SaveChanges();
            return libro;
        }

    }
}
