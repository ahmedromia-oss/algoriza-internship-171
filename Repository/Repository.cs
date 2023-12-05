using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;

namespace Repository
{
    public class Repository<T , GetDTO>: IRepository<T , GetDTO> where T :class  where GetDTO : class
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public Repository(AppDbContext appDbContext , IMapper mapper)
        {
           
this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task<T> Add(Object addDto)
        {
            await this.appDbContext.Set<T>().AddAsync(addDto as T);
            return addDto as T;
            
        }

        public async Task<bool> deleteById(string id)
        {
            T entity =await this.appDbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }
           
                this.appDbContext.Set<T>().Remove(entity);
            
            
                return true;
           
            
            
        }

        public async Task<ICollection<GetDTO>> GetAll(PaginationModel paginationModel)
        {
            return await this.appDbContext.Set<T>().Skip(((paginationModel.Page == 0 ? 1: paginationModel.Page) -1) * paginationModel.PageSize).ProjectTo<GetDTO>(mapper.ConfigurationProvider).Take(paginationModel.PageSize == 0 ? 10: paginationModel.PageSize).ToListAsync();
        }

       

        

        public async Task<GetDTO> GetByWhere(Expression<Func<T, bool>> where)
        {
            return await this.appDbContext.Set<T>().Where(where).ProjectTo<GetDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
           
        }

        public T update(Object updateDto)
        {
            this.appDbContext.Set<T>().Update(updateDto as T);
            return updateDto as T;
        }
    }
}
