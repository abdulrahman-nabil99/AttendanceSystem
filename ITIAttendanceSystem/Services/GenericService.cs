
using ITIAttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ITIAttendanceSystem.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        protected readonly AttendanceDbContext _db;
        protected readonly DbSet<TEntity> _dbSet;
        public GenericService(AttendanceDbContext _context)
        {
            _db = _context;
            _dbSet = _context.Set<TEntity>();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            var entity = _dbSet.Find(id);
            return entity;
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            SaveChanges();
        }


        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
