namespace ITIAttendanceSystem.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges();
    }
}
