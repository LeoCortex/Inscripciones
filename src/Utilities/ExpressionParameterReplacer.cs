using System.Linq.Expressions;

namespace InscripcionesApi.src.Utilities
{
	internal class ExpressionParameterReplacer : ExpressionVisitor
	{
		private readonly ParameterExpression _parameter;
		private readonly ParameterExpression _replacement;

		private ExpressionParameterReplacer(ParameterExpression parameter, ParameterExpression replacement)
		{
			_parameter = parameter;
			_replacement = replacement;
		}

		internal static Expression ReplaceParameter(Expression expression, ParameterExpression parameter, ParameterExpression replacement)
		{
			return new ExpressionParameterReplacer(parameter, replacement).Visit(expression);
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			return node == _parameter ? _replacement : base.VisitParameter(node);
		}
	}
}
