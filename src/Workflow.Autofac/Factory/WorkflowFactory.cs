using Autofac;
using DependencyInjection.Factory;

namespace Workflow.Autofac.Factory;

internal class WorkflowFactory : IFactory
{
    private readonly ILifetimeScope _lifetimeScope;

    public WorkflowFactory(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }

    public T Create<T>() where T : notnull
    {
        return _lifetimeScope.Resolve<T>();
    }

    public object Create(Type type)
    {
        return _lifetimeScope.Resolve(type);
    }

    public T Create<T>(Type type) where T : notnull
    {
        return _lifetimeScope.Resolve<T>();
    }
}
    
internal class WorkflowFactory<T> : IFactory<T> where T : notnull
{
    private readonly Func<T> _factory;

    public WorkflowFactory(Func<T> factory)
    {
        _factory = factory;
    }

    public T Create()
    {
        return _factory();
    }
}
    
internal class WorkflowFactory<TParameter, T> : IFactory<TParameter, T> where T : notnull
{
    private readonly Func<TParameter, T> _factory;

    public WorkflowFactory(Func<TParameter, T> factory)
    {
        _factory = factory;
    }

    public T Create(TParameter parameter)
    {
        return _factory(parameter);
    }
}
    
internal class WorkflowFactory<TParameter1, TParameter2, T> : IFactory<TParameter1, TParameter2, T> where T : notnull
{
    private readonly Func<TParameter1, TParameter2, T> _factory;

    public WorkflowFactory(Func<TParameter1, TParameter2, T> factory)
    {
        _factory = factory;
    }

    public T Create(TParameter1 parameter1, TParameter2 parameter2)
    {
        return _factory(parameter1, parameter2);
    }
}
    
internal class WorkflowFactory<TParameter1, TParameter2, TParameter3, T> : IFactory<TParameter1, TParameter2, TParameter3, T> where T : notnull
{
    private readonly Func<TParameter1, TParameter2, TParameter3, T> _factory;

    public WorkflowFactory(Func<TParameter1, TParameter2, TParameter3, T> factory)
    {
        _factory = factory;
    }

    public T Create(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3)
    {
        return _factory(parameter1, parameter2, parameter3);
    }
}
    
internal class WorkflowFactory<TParameter1, TParameter2, TParameter3, TParameter4, T> : IFactory<TParameter1, TParameter2, TParameter3, TParameter4, T> where T : notnull
{
    private readonly Func<TParameter1, TParameter2, TParameter3, TParameter4, T> _factory;

    public WorkflowFactory(Func<TParameter1, TParameter2, TParameter3, TParameter4, T> factory)
    {
        _factory = factory;
    }

    public T Create(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4)
    {
        return _factory(parameter1, parameter2, parameter3, parameter4);
    }
}
    
internal class WorkflowFactory<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, T> : IFactory<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, T> where T : notnull
{
    private readonly Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, T> _factory;

    public WorkflowFactory(Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, T> factory)
    {
        _factory = factory;
    }

    public T Create(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4, TParameter5 parameter5)
    {
        return _factory(parameter1, parameter2, parameter3, parameter4, parameter5);
    }
}