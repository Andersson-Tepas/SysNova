using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SysNova.DAL.Context;
using SysNova.Repository.Interfaces;

namespace SysNova.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SysNovaDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(SysNovaDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate)
        {
            return await _dbSet
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);

                await _context.SaveChangesAsync();

                return entity;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine();
                Console.WriteLine("================================================");
                Console.WriteLine("ERROR DE ENTITY FRAMEWORK AL GUARDAR");
                Console.WriteLine("================================================");
                Console.WriteLine($"Entidad: {typeof(T).Name}");
                Console.WriteLine($"Mensaje EF: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine(
                        $"InnerException: {ex.InnerException.Message}");
                }

                if (ex.InnerException?.InnerException != null)
                {
                    Console.WriteLine(
                        $"InnerException 2: {ex.InnerException.InnerException.Message}");
                }

                Console.WriteLine();
                Console.WriteLine("DETALLE COMPLETO:");
                Console.WriteLine(ex);
                Console.WriteLine("================================================");
                Console.WriteLine();

                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("================================================");
                Console.WriteLine("ERROR GENERAL AL GUARDAR");
                Console.WriteLine("================================================");
                Console.WriteLine($"Entidad: {typeof(T).Name}");
                Console.WriteLine($"Mensaje: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine(
                        $"InnerException: {ex.InnerException.Message}");
                }

                Console.WriteLine();
                Console.WriteLine("DETALLE COMPLETO:");
                Console.WriteLine(ex);
                Console.WriteLine("================================================");
                Console.WriteLine();

                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine();
                Console.WriteLine("================================================");
                Console.WriteLine("ERROR DE ENTITY FRAMEWORK AL ACTUALIZAR");
                Console.WriteLine("================================================");
                Console.WriteLine($"Entidad: {typeof(T).Name}");
                Console.WriteLine($"Mensaje EF: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine(
                        $"InnerException: {ex.InnerException.Message}");
                }

                if (ex.InnerException?.InnerException != null)
                {
                    Console.WriteLine(
                        $"InnerException 2: {ex.InnerException.InnerException.Message}");
                }

                Console.WriteLine();
                Console.WriteLine("DETALLE COMPLETO:");
                Console.WriteLine(ex);
                Console.WriteLine("================================================");
                Console.WriteLine();

                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("================================================");
                Console.WriteLine("ERROR GENERAL AL ACTUALIZAR");
                Console.WriteLine("================================================");
                Console.WriteLine($"Entidad: {typeof(T).Name}");
                Console.WriteLine($"Mensaje: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine(
                        $"InnerException: {ex.InnerException.Message}");
                }

                Console.WriteLine();
                Console.WriteLine("DETALLE COMPLETO:");
                Console.WriteLine(ex);
                Console.WriteLine("================================================");
                Console.WriteLine();

                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine();
                Console.WriteLine("================================================");
                Console.WriteLine("ERROR DE ENTITY FRAMEWORK AL ELIMINAR");
                Console.WriteLine("================================================");
                Console.WriteLine($"Entidad: {typeof(T).Name}");
                Console.WriteLine($"Mensaje EF: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine(
                        $"InnerException: {ex.InnerException.Message}");
                }

                Console.WriteLine();
                Console.WriteLine("DETALLE COMPLETO:");
                Console.WriteLine(ex);
                Console.WriteLine("================================================");
                Console.WriteLine();

                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("================================================");
                Console.WriteLine("ERROR GENERAL AL ELIMINAR");
                Console.WriteLine("================================================");
                Console.WriteLine($"Entidad: {typeof(T).Name}");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine(ex);
                Console.WriteLine("================================================");
                Console.WriteLine();

                throw;
            }
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}