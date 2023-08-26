namespace GesRelationClient.Services
{
    public interface IRepositorie<T>  where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetPaged(int pageNumber = 1, int pageSize = 10);
        Task<T> GetById(int id);
        Task<int> GetTotal();
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
    }
}
