using System.Linq.Expressions;
using System.Reflection;

namespace Workflow.Property;

internal static class WorkflowProperty<TContext, TProperty>
{
    public static async Task SetAsync(
        TContext context,
        Func<TContext, Task<TProperty>> actionReturn,
        Expression<Func<TContext, TProperty>> propertyPicker
    )
    {
        var prop = (PropertyInfo)((MemberExpression)propertyPicker.Body).Member;
        prop.SetValue(
            context,
            await actionReturn(context).ConfigureAwait(true)
        );
    }

    public static Task SetAsync(
        TContext context,
        TProperty? value,
        Expression<Func<TContext, TProperty>> propertyPicker
    )
    {
        var prop = (PropertyInfo)((MemberExpression)propertyPicker.Body).Member;
        prop.SetValue(
            context,
            value
        );
        return Task.CompletedTask;
    }

    public static Task<IEnumerable<TProperty>> GetAsync(
        TContext context,
        Expression<Func<TContext, IEnumerable<TProperty>>> propertyPicker
    )
    {
        var prop = (PropertyInfo)((MemberExpression)propertyPicker.Body).Member;
        var value = prop.GetValue(context);
        if (value is null) throw new ArgumentException($"{nameof(prop.Name)} is null or can not be read");
        return Task.FromResult((IEnumerable<TProperty>)value);
    }
}