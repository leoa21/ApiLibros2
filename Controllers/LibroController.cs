using ApiLibros2.DTOs;
using ApiLibros2.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLibros2.Controllers
{
    [ApiController]
    [Route("/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public LibrosController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }


        [HttpGet("listado")]
        public async Task<ActionResult<List<GetLibroDTO>>> Get()
        {
            var libros = await dbContext.Libros.ToListAsync();
            return mapper.Map<List<GetLibroDTO>>(libros);
        }

        [HttpGet("{id:int}", Name = "obtenerlibro")]
        public async Task<ActionResult<LibroDTOConAutor>> Get(int id)
        {
            var libro = await dbContext.Libros
                .Include(libroDB => libroDB.LibroAutor)
                .ThenInclude(libroAutorDB => libroAutorDB.Autor)
                .Include(editorialDB => editorialDB.Editorial)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            libro.LibroAutor = libro.LibroAutor.OrderBy(x => x.Orden).ToList();

            return mapper.Map<LibroDTOConAutor>(libro);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetLibroDTO>>> Get(string nombre)
        {
            var libros = await dbContext.Libros.Where(libroDB => libroDB.NombreLibro.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetLibroDTO>>(libros);
        }


        [HttpPost]
        public async Task<ActionResult> Post(LibroDTO libroDto)
        {
            var existeLibroMismoNombre = await dbContext.Libros.AnyAsync(x => x.NombreLibro == libroDto.NombreLibro);

            if (existeLibroMismoNombre)
            {
                return BadRequest($"Ya existe un libro con el nombre {libroDto.NombreLibro}");
            }

            var libro = mapper.Map<Libro>(libroDto);

            dbContext.Add(libro);
            await dbContext.SaveChangesAsync();

            var libroDTO= mapper.Map<GetLibroDTO>(libro);

            return CreatedAtRoute("obtenerlibro", new { id = libro.Id }, libroDTO);


        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(LibroDTO libroCreacionDTO, int id)
        {
            var exist = await dbContext.Libros.AnyAsync(x => x.Id == id);


            if (!exist)
            {
                return NotFound();
            }

            var libro = mapper.Map<Libro>(libroCreacionDTO);
            libro.Id = id;

            dbContext.Update(libro);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Libros.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Libro()
            {
                Id = id
            });

            await dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}