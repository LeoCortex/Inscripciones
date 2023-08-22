using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using InscripcionesApi.src.Data;
using InscripcionesApi.src.Repository;
using InscripcionesApi.src.Utilities;
using Microsoft.EntityFrameworkCore;

namespace InscripcionesApi.src.Dal
{

    /// <summary>
    /// Clase generica que gestiona las acciones crud y las peticiones con la base de datos 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly InscripcionesContext  _dbContext;
        private DbSet<T> _dbSet;

        public GenericRepository(InscripcionesContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public GenericRepository()
        {
            _dbContext = new InscripcionesContext();
            _dbSet = _dbContext.Set<T>();
        }

        /// <summary>
        /// Obtener registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T GetById(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// obtiene todos los registros de una tabla aplicando ordenamiento y  filtros 
        /// </summary>
        /// <param name="asc">1 asc, 0 desc</param>
        /// <param name="filter">diccionario con nombre de la propiedad por la que se filtra y  valor</param>
        /// <param name="orderby">nombre de la propiedad por la que se ordena</param>
        /// <param name="dependencias">lista separada por coma de los nombres de las dependencias de <T> que se desean consultar</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual IEnumerable<T> GetAll(bool asc, Dictionary<string, string>? filter, string orderby, string? dependencias)
        {
            try
            {
                IQueryable<T>? result = null;

                PropertyInfo? propertyInfo = typeof(T).GetProperty(orderby);

                //se traen las dependencias necesarias para el set de datos
				if (string.IsNullOrEmpty(dependencias)) dependencias = Utilities<T>.GetDependencies(_dbSet);

                //Se crea la exprecion linq para realizar el filtro en DAL si filter trae algun parametro
                if (filter != null && filter.Count > 0)
                {
                    Expression<Func<T, bool>> finalFilter = Utilities<T>.BuildFilter(filter);
                    result = _dbSet.Where(finalFilter);
                }
                else{
                    
                    IQueryable<T> res = _dbSet;
                    foreach(var dep in dependencias.Split("."))
                        res = res.Include(dep);
                    result = res;
                }
                

				//Ordenamiento de la lista
				var resultOrdered = asc ? result.OrderBy(x => orderby) : result.OrderByDescending(x => orderby);

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// agrega un nuevo registro 
        /// </summary>
        /// <param name="entity">Objeto tipo <T></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T Add(T entity)
        {
            try
            {
                //creacion de nuevo registro
                _dbSet.Add(entity);
                _dbContext.SaveChanges();

                var ret = _dbSet.Entry(entity).Entity;
                //Devuelve registro creado
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
		/// <summary>
        /// Actualiza registro
        /// </summary>
        /// <param name="entity">objeto tipo <T></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
		public bool Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
                }

                var idProperty = typeof(T).GetProperty("Id") ?? throw new InvalidOperationException("Entity does not have an 'Id' property.");
                var idValue = (int)idProperty.GetValue(entity);

                if (idValue <= 0)
                {
                    throw new ArgumentException("Invalid Id value.");
                }

                var existingEntity = _dbSet.Find(idValue);

                if (existingEntity != null)
                {
                    _dbSet.Entry(existingEntity).CurrentValues.SetValues(entity);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ArgumentException("Entity not found in the database.");
                }
            }
            catch (Exception ex)
            {
                throw; // Rethrow the original exception for better debugging.
            }
        }
        
        /// <summary>
        /// elimina registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
                }

                var idProperty = typeof(T).GetProperty("Id");
                if (idProperty == null)
                {
                    throw new InvalidOperationException("Entity does not have an 'Id' property.");
                }

                var idValue = (int)idProperty.GetValue(entity);
                var dbEntity = _dbSet.Find(idValue);

                if (dbEntity != null)
                {
                    _dbSet.Remove(dbEntity);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    throw new InvalidOperationException("Entity not found in the database.");
                }
            }
            catch (Exception ex)
            {
                throw; // Rethrow the original exception for better debugging.
            }

        }
    }
}





