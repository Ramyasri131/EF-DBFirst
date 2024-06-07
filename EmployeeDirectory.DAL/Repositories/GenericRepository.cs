using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.DAL.Repositories
{
    public class GenericRepository<T>(RamyaEmployeeDirectoryDbContext dbContext) :IGenericRepository<T> where T : class
    {
        private readonly RamyaEmployeeDirectoryDbContext _dbContext= dbContext;

        public async Task<List<T>> GetAll()  
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(object? id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);   
            await _dbContext.SaveChangesAsync();
        }
    }
}