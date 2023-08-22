using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InscripcionesApi.src.Data;
using InscripcionesApi.src.Data.Models;
using InscripcionesApi.src.DTO;
using Microsoft.EntityFrameworkCore;

namespace InscripcionesApi.src.DAL
{
    public class EstudiantesRepository
    {
        private readonly InscripcionesContext  _dbContext;
        private readonly DbSet<Estudiante> _dbSet;

        public EstudiantesRepository()
        {
            _dbContext = new InscripcionesContext();
            _dbSet = _dbContext.Set<Estudiante>();
        }

         public List<EstudianteCompDTO> GetCompañeros(int idEstudiante)
        {
            try
            {
                List<EstudianteCompDTO> estudiantesCompartidos = (from estudiante in _dbContext.Estudiantes
                                            join inscripcion in _dbContext.Inscripciones on estudiante.Id equals inscripcion.IdEstudiantes
                                            join clase in _dbContext.Clases on inscripcion.IdClases equals clase.Id
                                            join profesor in _dbContext.Profesores on clase.IdProfesores equals profesor.Id
                                            join asignatura in _dbContext.Asignaturas on clase.IdAsignaturas equals asignatura.Id
                                            where estudiante.Id == idEstudiante
                                            select new EstudianteCompDTO
                                            {
                                                Id = estudiante.Id,
                                                Identificacion = estudiante.Identificacion,
                                                Nombres = estudiante.Nombres,
                                                Apellidos = estudiante.Apellidos,
                                                ProfesorNombre = $"{profesor.Nombres} {profesor.Apellidos}",
                                                AsignaturaNombre = asignatura.Nombre
                                            }).ToList<EstudianteCompDTO>();


                return estudiantesCompartidos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}