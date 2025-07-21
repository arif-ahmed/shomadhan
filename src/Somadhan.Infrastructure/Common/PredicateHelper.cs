using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Somadhan.Domain.Core.Identity;

namespace Somadhan.Infrastructure.Common;

public static class PredicateHelper
{
    public static object? ExtractShopIdValue(Expression<Func<User, bool>> predicate)
    {
        // Step 1: Is it a binary expression?
        if (predicate.Body is BinaryExpression binaryExpr &&
            binaryExpr.NodeType == ExpressionType.Equal)
        {
            // Step 2: Is one side user.ShopId?
            MemberExpression? memberExpr = null;
            Expression? valueExpr = null;

            if (IsShopIdMember(binaryExpr.Left))
            {
                memberExpr = (MemberExpression)binaryExpr.Left;
                valueExpr = binaryExpr.Right;
            }
            else if (IsShopIdMember(binaryExpr.Right))
            {
                memberExpr = (MemberExpression)binaryExpr.Right;
                valueExpr = binaryExpr.Left;
            }

            if (memberExpr != null && valueExpr != null)
            {
                // Step 3: Compile value side if needed
                if (valueExpr is ConstantExpression constExpr)
                {
                    return constExpr.Value;
                }
                else
                {
                    // Covers closure/captured variables
                    var valueLambda = Expression.Lambda(valueExpr);
                    return valueLambda.Compile().DynamicInvoke();
                }
            }
        }

        return null;

        // Helper to check if member is user.ShopId
        static bool IsShopIdMember(Expression expr)
        {
            return expr is MemberExpression me &&
                   me.Member.Name == "ShopId";
        }
    }

}
