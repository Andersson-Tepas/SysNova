using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysNova.BL.Interfaces;
using SysNova.EN.Entities;
using SysNova.Repository.Interfaces;
using System.Linq.Expressions;

namespace SysNova.BL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;

        public BlogService(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Blog?> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Blog>> FindAsync(
            Expression<Func<Blog, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<Blog> AddAsync(Blog blog)
        {
            return await _repository.AddAsync(blog);
        }

        public async Task UpdateAsync(Blog blog)
        {
            await _repository.UpdateAsync(blog);
        }

        public async Task DeleteAsync(Blog blog)
        {
            await _repository.DeleteAsync(blog);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<Blog, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
