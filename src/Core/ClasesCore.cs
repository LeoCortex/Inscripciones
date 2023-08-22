using AutoMapper;
using InscripcionesApi.src.Core;
using InscripcionesApi.src.Data.Models;
using InscripcionesApi.src.Repository;

namespace ClasesApi.src.Core
{

    public class ClasesCore<T>:GenericCore<T>  where T : class
    {

        private readonly IGenericRepository<Clase> _genericRepository;
        private readonly IMapper _mapper;

        public ClasesCore(IGenericRepository<Clase> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        
        public override IEnumerable<T> GetAll(bool asc, Dictionary<string, string>? filter, string? orderby , string? dependencias)
        {
            try
            {
                if (string.IsNullOrEmpty(orderby))
                {
                    orderby = "Id";
                }

                var clases = _genericRepository.GetAll(asc, filter, orderby, dependencias).ToList();

                var a = typeof(T).Name;

                List<T> LInscripcion = _mapper.Map<List<T>>(clases);
                
                return LInscripcion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public override T GetById(int id)
        {
            try
            {
                Clase Inscripcion = _genericRepository.GetById(id);
                T InscripcionDto = _mapper.Map<T>(Inscripcion);
                return InscripcionDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override T Add(T InscripcionDto)
        {
            try
            {
                Clase Inscripcion =_genericRepository.Add( _mapper.Map<Clase>(InscripcionDto));

                return _mapper.Map<T>(Inscripcion) ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override bool Delete(T InscripcionDto)
        {
            try
            {
                return _genericRepository.Delete(_mapper.Map<Clase>(InscripcionDto));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override bool Update(T InscripcionDto)
        {
            try
            {
                return _genericRepository.Update(_mapper.Map<Clase>(InscripcionDto));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
