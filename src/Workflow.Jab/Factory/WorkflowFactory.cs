using Workflow.Factory;

namespace Workflow.Jab.Factory;

public class WorkflowFactory : IWorkflowFactory
{
    private readonly IServiceProvider _serviceProvider;

    public WorkflowFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T Create<T>() where T : notnull
    {
        if (_serviceProvider.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"Type {typeof(T).FullName} can not be resolved. Try register explizitly.");
        }
        
        return service;
    }
}
    
public class WorkflowFactory<T> : IWorkflowFactory<T> where T : notnull
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
    
public class WorkflowFactory<TParameter, T> : IWorkflowFactory<TParameter, T> where T : notnull
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
    
public class WorkflowFactory<TParameter1, TParameter2, T> : IWorkflowFactory<TParameter1, TParameter2, T> where T : notnull
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
    
public class WorkflowFactory<TParameter1, TParameter2, TParameter3, T> : IWorkflowFactory<TParameter1, TParameter2, TParameter3, T> where T : notnull
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
    
public class WorkflowFactory<TParameter1, TParameter2, TParameter3, TParameter4, T> : IWorkflowFactory<TParameter1, TParameter2, TParameter3, TParameter4, T> where T : notnull
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
    
public class WorkflowFactory<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, T> : IWorkflowFactory<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, T> where T : notnull
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