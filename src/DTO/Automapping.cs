using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InscripcionesApi.src.DTO;
using InscripcionesApi.src.Data.Models;

namespace GestionLogistica.DTOs
{
    public class Automapping : Profile
    {
        public Automapping(){
            CreateMap<Estudiante,EstudianteDTO>();
            CreateMap<EstudianteDTO,Estudiante>();
            CreateMap<Inscripcione,InscripcioneDTO>();
            CreateMap<InscripcioneDTO,Inscripcione>();
            CreateMap<Profesore,ProfesoreDTO>();
            CreateMap<ProfesoreDTO,Profesore>();
            CreateMap<Clase,ClaseDTO>();
            CreateMap<ClaseDTO,Clase>();
            CreateMap<Asignatura,AsignaturaDTO>();
            CreateMap<AsignaturaDTO,Asignatura>();
        }
        
    }
}