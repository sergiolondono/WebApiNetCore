using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIPrueba.Contexts;
using WebAPIPrueba.Entities;
using WebAPIPrueba.Models;

namespace WebAPIPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AutorController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> Get()
        {
            var autores = await _context.Autores.ToListAsync();
            // .Include(x => x.Libros).ToList();
            var autoresDTO = _mapper.Map<List<AutorDTO>>(autores);
            return autoresDTO;
        }

        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            var autor = await _context.Autores
                //.Include(x => x.Libros)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            var autorDTO = _mapper.Map<AutorDTO>(autor);
            return autorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacion)
        {
            var autor = _mapper.Map<Autor>(autorCreacion);
            _context.Add(autor);
            await _context.SaveChangesAsync();
            var autorDTO = _mapper.Map<AutorDTO>(autor);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AutorCreacionDTO autorActualizacion)
        {
            var autor = _mapper.Map<Autor>(autorActualizacion);
            autor.Id = id;
            _context.Entry(autor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<AutorCreacionDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var autorDB = await _context.Autores.FirstOrDefaultAsync(a => a.Id == id);

            if (autorDB == null)
            {
                return NotFound();
            }

            var autorDTO = _mapper.Map<AutorCreacionDTO>(autorDB);

            patchDocument.ApplyTo(autorDTO, ModelState);
            _mapper.Map(autorDTO, autorDB);

            var isValid = TryValidateModel(autorDB);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            var autorId = await _context.Autores
                .Select(a => a.Id)
                .FirstOrDefaultAsync(a => a == id);

            if (autorId == default(int))
            {
                return NotFound();
            }

            _context.Remove(new Autor { Id = autorId });
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
