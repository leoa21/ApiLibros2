using ApiLibros2.DTOs;
using ApiLibros2.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLibros2.Controllers
{
    [ApiController]
    [Route("autor")]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public AutorController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
   
        }

        [HttpGet("/listadoAutor")]

        public async Task<ActionResult<List<Autor>>> GetAll()
        {
            return await dbContext.Autor.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "obtenerAutor")]
         
        public async Task<ActionResult<AutorDTOConLibro>> GetById(int id)
        {
            var autor = await dbContext.Autor
                .Include(autorDB => autorDB.LibroAutor)
                .ThenInclude(libroAutorDB => libroAutorDB.Libro)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(autor == null)
            {
                return NotFound();
            }

            return mapper.Map<AutorDTOConLibro>(autor);
        }

        //[HttpGet("{nombre}/{param?})] ---- Es para un parametro opcional


        [HttpPost]
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDto)
        {
            if(autorCreacionDto.LibrosIds == null)
            {
                return BadRequest("No se puede crear un autor sin un libro");
            }

            var librosIds = await dbContext.Libros
                .Where(libroDB => autorCreacionDto.LibrosIds.Contains(libroDB.Id)).Select(x => x.Id).ToListAsync();

            if(autorCreacionDto.LibrosIds.Count != librosIds.Count)
            {
                return BadRequest("No existe uno de los libros enviados");
            }


            var autor = mapper.Map<Autor>(autorCreacionDto);

            OrdenarPorLibro(autor);

            dbContext.Add(autor);
            await dbContext.SaveChangesAsync();

            var autorDTO = mapper.Map<AutorDTO>(autor);

            return CreatedAtRoute("obtenerAutor", new { id = autor.Id }, autorDTO);
            
        } 

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AutorCreacionDTO autorCreacionDTO)
        {
            var autorDB = await dbContext.Autor
                .Include(x => x.LibroAutor)
                .FirstOrDefaultAsync(x => x.Id == id);


            if(autorDB == null)
            {
                return NotFound();
            }

            autorDB = mapper.Map(autorCreacionDTO, autorDB);

            OrdenarPorLibro(autorDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Autor.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado");
            }

            dbContext.Remove(new Autor { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        private void OrdenarPorLibro(Autor autor)
        {
            if(autor.LibroAutor != null)
            {
                for(int i = 0; i < autor.LibroAutor.Count; i++)
                {
                    autor.LibroAutor[i].Orden = i;
                }
            }
        }


        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<AutorPatchDTO> patchDocument)
        {
            if(patchDocument == null) { return BadRequest(); }

            var autorDB = await dbContext.Autor.FirstOrDefaultAsync(x => x.Id == id);

            if(autorDB == null) { return BadRequest(); }

            var autorDTO = mapper.Map<AutorPatchDTO>(autorDB);

            patchDocument.ApplyTo(autorDTO);

            var isValid = TryValidateModel(autorDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(autorDTO, autorDB);

            await dbContext.SaveChangesAsync();
            return NoContent();


        }

    }
}
