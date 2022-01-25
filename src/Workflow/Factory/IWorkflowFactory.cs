namespace Workflow.Factory;

public interface IWorkflowFactory
{
    T Create<T>() where T : notnull;
}

public interface IWorkflowFactory<out T> where T : notnull
{
    T Create();
}

public interface IWorkflowFactory<in TParameter, out T> where T : notnull
{
    T Create(TParameter parameter);
}

public interface IWorkflowFactory<in TParameter1, in TParameter2, out T> where T : notnull
{
    T Create(TParameter1 parameter1, TParameter2 parameter2);
}

public interface IWorkflowFactory<in TParameter1, in TParameter2, in TParameter3, out T> where T : notnull
{
    T Create(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3);
}

public interface IWorkflowFactory<in TParameter1, in TParameter2, in TParameter3, in TParameter4, out T> where T : notnull
{
    T Create(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4);
}

public interface IWorkflowFactory<in TParameter1, in TParameter2, in TParameter3, in TParameter4, in TParameter5, out T> where T : notnull
{
    T Create(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4, TParameter5 parameter5);
}