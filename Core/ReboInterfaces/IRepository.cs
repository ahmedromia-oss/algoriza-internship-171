using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T , GetDTO>
    {
        public Task<ICollection<GetDTO>> GetAll(PaginationModel paginationModel);
        public Task<ICollection<GetDTO>> GetAll(PaginationModel paginationModel , Expression<Func<T, bool>> where);



        public Task<GetDTO> GetByWhere(Expression<Func<T , bool>> where);

        public Task<T> Add(Object addDto);
        public Task<T> update(Object updateDto , string id);
        public Task<bool> deleteById(string id);
        public Task<int> count();



    }
}
