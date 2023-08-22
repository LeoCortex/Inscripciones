using System.Linq.Expressions;

namespace InscripcionesApi.src.Repository
{
	//interface para de metodos para objetos que interactuan con DB
	public interface IGenericRepository<T>
	{
		T GetById(int id);
		IEnumerable<T> GetAll(bool asc, Dictionary<string, string>? filter, string? orderby, string? dependencias);
		T Add(T entity);
		bool Update(T entity);
		bool Delete(T entity);
	}
}
