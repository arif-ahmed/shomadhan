using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Somadhan.Persistence.EF.Common;

public static class ExpressionConversionHelper
{
    public static Expression<Func<TTarget, bool>> ConvertPredicate<TSource, TTarget>(Expression<Func<TSource, bool>> predicate)
    {
        var parameter = Expression.Parameter(typeof(TTarget), predicate.Parameters[0].Name);
        var visitor = new TypeConvertVisitor<TSource, TTarget>(parameter);
        var body = visitor.Visit(predicate.Body);
        return Expression.Lambda<Func<TTarget, bool>>(body, parameter);
    }
}