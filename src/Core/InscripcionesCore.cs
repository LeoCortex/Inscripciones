using AutoMapper;
using InscripcionesApi.src.Data.Models;
using InscripcionesApi.src.Repository;

namespace InscripcionesApi.src.Core
{

    public class InscripcionesCore<T>:GenericCore<T>  where T : class
    {

        private readonly IGenericRepository<Inscripcione> _genericRepository;
        private readonly IMapper _mapper;

        public InscripcionesCore(IGenericRepository<Inscripcione> genericRepository, IMapper mapper)
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

                var Inscripciones = _genericRepository.GetAll(asc, filter, orderby, dependencias).ToList();

                var a = typeof(T).Name;

                List<T> LInscripcion = _mapper.Map<List<T>>(Inscripciones);
                
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
                Inscripcione Inscripcion = _genericRepository.GetById(id);
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
                Inscripcione Inscripcion =_genericRepository.Add( _mapper.Map<Inscripcione>(InscripcionDto));

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
                return _genericRepository.Delete(_mapper.Map<Inscripcione>(InscripcionDto));
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
                return _genericRepository.Update(_mapper.Map<Inscripcione>(InscripcionDto));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
