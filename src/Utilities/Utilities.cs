using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace InscripcionesApi.src.Utilities
{
	public class Utilities<T> where T : class
	{
		public static Expression<Func<T, bool>> BuildAndExpression<T>(List<Expression<Func<T, bool>>> expressions)
		{
			if (expressions.Count == 0)
			{
				return x => true;
			}

			var firstExpression = expressions[0].Body;
			var parameter = expressions[0].Parameters[0];

			for (int i = 1; i < expressions.Count; i++)
			{
				var secondExpression = expressions[i].Body;
				var swappedSecondExpression = ExpressionParameterReplacer.ReplaceParameter(secondExpression, expressions[i].Parameters[0], parameter);
				
				firstExpression = Expression.AndAlso(firstExpression, swappedSecondExpression);
			}

			var lambda = Expression.Lambda<Func<T, bool>>(firstExpression, parameter);
			return lambda;
		}

		/// <summary>
		/// Propiedad que recibe un diccionario con el que se arma una expresion lambda para 
		/// la propiedad T
		/// </summary>
		/// <param name="diccionario"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static Expression<Func<T, bool>> BuildFilter(Dictionary<string, string> diccionario)
		{
			var tipo = typeof(T);
			var parametro = Expression.Parameter(tipo, "x");
			Expression? expresionFinal = null;
			try
			{
				foreach (var par in diccionario)
				{
					PropertyInfo? propiedad = typeof(T).GetProperty(par.Key);
					var propiedadType = propiedad.PropertyType;
					//se verifica si la propiedad es de tipo nulable para asignar el tipo de dato correctamente 
					if (propiedadType.IsGenericType && propiedadType.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						propiedadType = Nullable.GetUnderlyingType(propiedadType);
					}

					//Se oblica a cambiar el tipo de dato para compatibilidad con campos nulables
					var convertida = Expression.Convert(Expression.Property(parametro, propiedad), propiedadType);
					var constante = Expression.Constant(Convert.ChangeType(par.Value, propiedadType));

					//se crea la igualdad para el parametro correspondiente al campo del diccionario
					var igualdad = Expression.Equal(convertida, constante);

					//se "concatenan" las expresiones para vada propiedad del diccionario
					if (expresionFinal == null)
					{
						expresionFinal = igualdad;
					}
					else
					{
						expresionFinal = Expression.AndAlso(expresionFinal, igualdad);
					}
				}
				return Expression.Lambda<Func<T, bool>>(expresionFinal, parametro);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static string GetDependencies(DbSet<T> dbSet)
		{
			var entityType = dbSet.EntityType;
			var navigationProperties = entityType.GetNavigations();
			
			StringBuilder dependencies = new();

			foreach (var navigationProperty in navigationProperties)
			{
				dependencies.Append(navigationProperty.Name).Append('.');
			}

			if (dependencies.Length > 0)
			{
				dependencies.Length--; // Elimina la última coma
			}

			return dependencies.ToString();
		}

		public static T GetProperty<T>(T entity, string property)
		{
			try
			{
				var propiedadId = entity.GetType().GetProperty(property);
				return (T)propiedadId.GetValue(entity);
			}
			catch (Exception ex)
			{

				throw new Exception("Ocurrió un error al consultar la propiedad {0} en la entidad.", ex); ;
			}
		}

		public static void CopiarPropiedades<T>(ref T origen, Object destino)
		{

			try
			{
				PropertyInfo[] propiedadesOrigen = origen.GetType().GetProperties();
				PropertyInfo[] propiedadesDestino = destino.GetType().GetProperties();

				foreach (PropertyInfo propiedadDestino in propiedadesDestino)
				{
					foreach (PropertyInfo propiedadOrigen in propiedadesOrigen)
					{
						if (propiedadOrigen.Name == propiedadDestino.Name && propiedadOrigen.PropertyType == propiedadDestino.PropertyType)
						{
							propiedadDestino.SetValue(destino, propiedadOrigen.GetValue(origen));
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{

				throw new Exception("Ocurrio un error al copiar las pripiedades del viewModel al Modelo", ex); ;
			}
		}
	}

}
