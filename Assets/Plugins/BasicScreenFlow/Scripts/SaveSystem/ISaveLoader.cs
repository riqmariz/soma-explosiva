public interface ISaveLoader
{
    void Save<T>(T data, string path) where T : class;
    T Load<T>(string path) where T : class;
}
