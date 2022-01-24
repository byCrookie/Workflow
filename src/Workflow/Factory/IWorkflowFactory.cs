namespace Workflow.Factory;

public interface IWorkflowFactory
{
    T Create<T>();
}
    
public interface IWorkflowFactory<out T>
{
    T Create();
}
    
public interface IWorkflowFactory<in TParameter, out T>
{
    T Create(TParameter parameter);
}
    
public interface IWorkflowFactory<in TParameter1, in TParameter2, out T>
{
    T Create(TParameter1 parameter1, TParameter2 parameter2);
}
    
public interface IWorkflowFactory<in TParameter1, in TParameter2, in TParameter3, out T>
{
    T Create(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3);
}
    
public interface IWorkflowFactory<in TParameter1, in TParameter2, in TParameter3, in TParameter4, out T>
{
    T Create(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4);
}
    
public interface IWorkflowFactory<in TParameter1, in TParameter2, in TParameter3, in TParameter4, in TParameter5, out T>
{
    T Create(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4, TParameter5 parameter5);
}