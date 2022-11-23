using ApiLibros2.DTOs;
using ApiLibros2.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLibros2.Controllers

{
    [ApiController]
    [Route("libros/{librosId:int}/editorial")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EditorialController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public EditorialController(ApplicationDbContext dbContext, IMapper mapper, UserManager<IdentityUser> useManager)
        {

            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = useManager;

        }

        [HttpGet]
        public async Task<ActionResult<List<EditorialDTO>>> Get(int libroId)
        {
            var existeLibro = await dbContext.Libros.AnyAsync(libroDB => libroDB.Id == libroId);

            if (!existeLibro)
            {
                return NotFound();
            }

            var editorial = await dbContext.Editorial.Where(editorialDB => editorialDB.LibroId == libroId).ToListAsync();

            return mapper.Map<List<EditorialDTO>>(editorial);
        }

        [HttpGet("{id:int}", Name = "obtenerEditorial")]
        public async Task<ActionResult<EditorialDTO>> GetById(int id)
        {
            var editorial = await dbContext.Editorial.FirstOrDefaultAsync(editorialDB => editorialDB.Id == id);

            if (editorial == null)
            {
                return NotFound();

            }

            return mapper.Map<EditorialDTO>(editorial);
        }

        [HttpPost]
        [AllowAnonymous] //Me permite que cualquier usuario tenga acceso sin necesidad de generar el token
        public async Task<ActionResult> Post(int libroId, EditorialDTOCreacion editorialDTOCreacion)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existeLibro = await dbContext.Libros.AnyAsync(libroDB => libroDB.Id == libroId);

            if (!existeLibro)
            {
                return NotFound();
            }

            var editorial = mapper.Map<Editorial>(editorialDTOCreacion);
            editorial.LibroId = libroId;
            editorial.UsuarioId = usuarioId;
            dbContext.Add(editorial);

            await dbContext.SaveChangesAsync();

            var editorialDTO = mapper.Map<EditorialDTO>(editorial);

            return CreatedAtRoute("obtenerEditorial", new { id = editorial.Id, libroId = libroId }, editorialDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int libroId, int id, EditorialDTOCreacion editorialDTOCreacion)
        {
            var existeLibro = await dbContext.Libros.AnyAsync(libroDB => libroDB.Id == libroId);

            if (!existeLibro)
            {
                return NotFound();
            }

            var existeEditorial = await dbContext.Libros.AnyAsync(editorialDB => editorialDB.Id == id);

            if (!existeEditorial)
            {
                return NotFound();
            }

            var editorial = mapper.Map<Editorial>(editorialDTOCreacion);
            editorial.Id = id;
            editorial.LibroId = libroId;

            dbContext.Update(editorial);

            await dbContext.SaveChangesAsync();
            return NoContent();

        }
    }
}
