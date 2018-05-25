using BusinessRules.POC.Models;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.SimpleRuleValidators
{
        public interface ISimpleRuleLearningDeliveryValidator
        {
            bool Evaluate(MessageLearnerLearningDelivery learningDelivery);
        }

        public class SimpleRuleLearningDeliveryValidator : ISimpleRuleLearningDeliveryValidator
        {
       
        public SimpleRuleLearningDeliveryValidator()
        {

        }

        public bool Evaluate(MessageLearnerLearningDelivery ld)
        {
            List<SimpleRule> rules = new List<SimpleRule>
                                {
                                    new SimpleRule("DateOfBirth", "GreaterThanOrEqual","LearnStartDate" ) //LearnStartDate_05
                                };


            Func<MessageLearnerLearningDelivery, bool> compiledRule = CompileRule<MessageLearnerLearningDelivery>(rules[0]);
            var result = compiledRule(ld);
  
            return result;
        }

        static Expression BuildExpr<T>(SimpleRule r, ParameterExpression param)
        {
            var left = MemberExpression.Property(param, r.Left);
            var right = MemberExpression.Property(param, r.Right);

            ExpressionType tBinary;
            if (ExpressionType.TryParse(r.Operator, out tBinary))
            {
                var rightConstant = Expression.Constant(right);
                return Expression.MakeBinary(tBinary, left, rightConstant);
            }
            //else
            //{
            //    var method = tProp.GetMethod(r.Operator);
            //    var tParam = method.GetParameters()[0].ParameterType;
            //   var right = Expression.Constant(r.Right);
            //    // use a method call, e.g. 'Contains'
            //    return Expression.Call(left, method, right);
            //}
            return null;
        }

        public static Func<T, bool> CompileRule<T>(SimpleRule r)
        {
            var paramUser = Expression.Parameter(typeof(T));
            Expression expr = BuildExpr<T>(r, paramUser);

            return Expression.Lambda<Func<T, bool>>(expr, paramUser).Compile();
        }

       

    }

    public static class ExtenionMethods
    {
        public static T To<T>(this IConvertible obj)
        {
            Type t = typeof(T);
            Type u = Nullable.GetUnderlyingType(t);

            if (u != null)
            {
                return (obj == null) ? default(T) : (T)Convert.ChangeType(obj, u);
            }
            else
            {
                return (T)Convert.ChangeType(obj, t);
            }
        }
    }

}
