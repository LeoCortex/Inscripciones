using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InscripcionesApi.src.Core
{
   public abstract class GenericCore<T> where T : class
    {
        protected GenericCore(){}
        public abstract IEnumerable<T> GetAll(bool asc, Dictionary<string, string>? filter, string? orderby, string? dependencias);
        public abstract T GetById(int id);
        public abstract T Add(T clienteDto);
        public abstract bool Delete(T clienteDto);
        public abstract bool Update(T clienteDto);
    }
}