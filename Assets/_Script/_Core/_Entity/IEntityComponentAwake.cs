/// <summary>
/// note that this call won't be called in actual awake call. initialize component in Unity's Awake instead of this callback
/// NOTE : Dont reference other components in this scope they might not be initialzied if they need to use interface awake call to initialize
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEntityComponentAwake<T> : IEntityComponent where T : Entity<T>
{
    public void EntityComponentAwake(T entity);
}
