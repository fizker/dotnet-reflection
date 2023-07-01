using System.Linq.Expressions;
using System.Reflection;

namespace Reflection;

public class Mutator
{
	public void Mutate<TObject, TKey>(TObject obj, Expression<Func<TObject, TKey>> selector, string value) {
		var memberExpression = (MemberExpression)selector.Body;
		var property = (PropertyInfo)memberExpression.Member;
		property.SetValue(obj, value);
	}
}
