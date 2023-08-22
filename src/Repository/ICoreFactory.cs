using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InscripcionesApi.src.Core;

namespace InscripcionesApi.src.Repository
{
    public interface ICoreFactory<T> where T : class 
    {
        GenericCore<T> CreateInstance();
    }
}