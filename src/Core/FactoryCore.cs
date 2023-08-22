using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;
using InscripcionesApi.src.Repository;
using InscripcionesApi.src.Data.Models;
using InscripcionesApi.src.Dal;
using InscripcionesApi.src.Data;
using ClasesApi.src.Core;

namespace InscripcionesApi.src.Core
{
    public class FactoryCore<T> : ICoreFactory<T> where T : class
    {
        private readonly InscripcionesContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public FactoryCore(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _context = _serviceProvider.GetRequiredService<InscripcionesContext>();
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }

        public GenericCore<T> CreateInstance()
        {
            switch (typeof(T).Name)
            {
                case "EstudianteDTO":
                    return new EstudiantesCore<T>( new GenericRepository<Estudiante>(_context), _mapper) as GenericCore<T>;
                case "InscripcioneDTO":
                    return new InscripcionesCore<T>( new GenericRepository<Inscripcione>(_context), _mapper) as GenericCore<T>;
                case "ClaseDTO":
                    return new ClasesCore<T>( new GenericRepository<Clase>(_context), _mapper) as GenericCore<T>;
                default:
                    throw new ArgumentException($"No se puede crear una instancia de {typeof(T)}");
            }
        }
    }
}