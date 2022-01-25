using System;
using Autofac;
using Workflow.Factory;

namespace Workflow.Tests;

internal class WorkflowFactory : IWorkflowFactory
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
}
    
internal class WorkflowFactory<T> : IWorkflowFactory<T> where T : notnull
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
    
internal class WorkflowFactory<TParameter, T> : IWorkflowFactory<TParameter, T> where T : notnull
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
    
internal class WorkflowFactory<TParameter1, TParameter2, T> : IWorkflowFactory<TParameter1, TParameter2, T> where T : notnull
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