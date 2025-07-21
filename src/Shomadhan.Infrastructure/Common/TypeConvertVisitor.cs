using System.Linq.Expressions;
using System.Reflection;

namespace Shomadhan.Infrastructure.Common;

public class TypeConvertVisitor<TSource, TTarget> : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    public TypeConvertVisitor(ParameterExpression parameter)
    {
        _parameter = parameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        // Replace the original parameter with the target parameter
        return _parameter;
    }

    protected override Expression VisitMember(MemberExpression node)
    {
        // Only remap properties on the parameter, not on captured variables!
        if (node.Expression != null && node.Expression.NodeType == ExpressionType.Parameter &&
            node.Expression.Type == typeof(TSource))
        {
            // Remap property from TSource to TTarget
            if (node.Member.MemberType == MemberTypes.Property)
            {
                var property = typeof(TTarget).GetProperty(
                    node.Member.Name,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                if (property == null)
                {
                    var availableProps = typeof(TTarget).GetProperties().Select(p => p.Name).ToArray();
                    throw new InvalidOperationException(
                        $"Property '{node.Member.Name}' not found on type '{typeof(TTarget)}'. " +
                        $"Available properties: {string.Join(", ", availableProps)}");
                }

                var expression = Visit(node.Expression);
                return Expression.Property(expression, property);
            }
            else if (node.Member.MemberType == MemberTypes.Field)
            {
                var field = typeof(TTarget).GetField(
                    node.Member.Name,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                if (field == null)
                {
                    var availableFields = typeof(TTarget).GetFields().Select(f => f.Name).ToArray();
                    throw new InvalidOperationException(
                        $"Field '{node.Member.Name}' not found on type '{typeof(TTarget)}'. " +
                        $"Available fields: {string.Join(", ", availableFields)}");
                }

                var expression = Visit(node.Expression);
                return Expression.Field(expression, field);
            }
            throw new NotSupportedException(
                $"Only property and field access are supported. Tried to access: {node.Member.Name} ({node.Member.MemberType})");
        }

        // For all other member accesses (e.g. closure variables like request.ShopId), just return the original node
        return base.VisitMember(node);
    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        throw new NotSupportedException(
            $"Method calls in expressions are not supported by this visitor. Method: {node.Method.Name}");
    }
}

