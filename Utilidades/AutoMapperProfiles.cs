using ApiLibros2.DTOs;
using ApiLibros2.Entidades;
using AutoMapper;

namespace ApiLibros2.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<LibroDTO, Libro>();
            CreateMap<Libro, GetLibroDTO>();
            CreateMap<Libro, LibroDTOConAutor>()
                .ForMember(libroDTO => libroDTO.Autor, opciones => opciones.MapFrom(MapLibroDTOAutor));
            CreateMap<AutorCreacionDTO, Autor>()
                .ForMember(autor => autor.LibroAutor, opciones => opciones.MapFrom(MapLibroAutor));
            CreateMap<Autor, AutorDTO>();
            CreateMap<Autor, AutorDTOConLibro>()
                .ForMember(autorDTO => autorDTO.Libros, opciones => opciones.MapFrom(MapAutorDTOLibro));
            CreateMap<AutorPatchDTO, Autor>().ReverseMap();
            CreateMap<EditorialDTOCreacion, Editorial>();
            CreateMap<Editorial, EditorialDTO>();

        }

      private List<AutorDTO> MapLibroDTOAutor(Libro libro, GetLibroDTO getLibroDTO)
        {
           var result = new List<AutorDTO>();

            if(libro.LibroAutor == null) { return result; }

            foreach(var libroAutor in libro.LibroAutor)
            {
                result.Add(new AutorDTO()
                {
                    Id = libroAutor.AutorId,
                    NombreAutor = libroAutor.Autor.NombreAutor,
                    ApellidoAutor = libroAutor.Autor.ApellidoAutor
                    
                });
            }

            return result;

        }

        private List<GetLibroDTO> MapAutorDTOLibro(Autor autor, AutorDTO autorDTO)
        {
            var result = new List<GetLibroDTO>();

            if(autor.LibroAutor == null)
            {
                return result;
            }

            foreach(var libroautor in autor.LibroAutor)
            {
                result.Add(new GetLibroDTO()
                {
                    Id = libroautor.AutorId,
                    NombreLibro = libroautor.Libro.NombreLibro,
                    Fecha = libroautor.Libro.Fecha
                });
            }

            return result;
        }


        private List<LibroAutor> MapLibroAutor(AutorCreacionDTO autorCreacionDTO, Autor autor)
        {
            var resultado = new List<LibroAutor>();

            if(autorCreacionDTO.LibrosIds == null) { return resultado; }
            
            foreach(var libroId in autorCreacionDTO.LibrosIds)
            {
                resultado.Add(new LibroAutor()
                {
                    LibroId = libroId
                });
            }

            return resultado;
        }

    }
}
