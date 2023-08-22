using AutoMapper;
using InscripcionesApi.src.DAL;
using InscripcionesApi.src.Data.Models;
using InscripcionesApi.src.DTO;
using InscripcionesApi.src.Repository;

namespace InscripcionesApi.src.Core
{

    public class EstudiantesCore<T>:GenericCore<T>  where T : class
    {

        private readonly IGenericRepository<Estudiante> _genericRepository;
        private readonly IMapper _mapper;

        public EstudiantesCore(IGenericRepository<Estudiante> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public override T Add(T clienteDto)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(T clienteDto)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> GetAll(bool asc, Dictionary<string, string>? filter, string? orderby, string? dependencias)
        {
            throw new NotImplementedException();
        }

        public override T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EstudianteCompDTO> GetCompañeros(int idEstudiante)
        {
            try
            {
                EstudiantesRepository repo = new();
                var estudiantes = repo.GetCompañeros(idEstudiante);
                
                return estudiantes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override bool Update(T clienteDto)
        {
            throw new NotImplementedException();
        }
    }
}
