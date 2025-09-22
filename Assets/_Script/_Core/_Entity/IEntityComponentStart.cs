/// <summary>
/// note that this call won't be called in actual start call.
/// when this is called you can assume that entityComponent is already initialized
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEntityComponentStart<T> : IEntityComponent where T : Entity<T>
{
    public void EntityComponentStart(T entity);
}
