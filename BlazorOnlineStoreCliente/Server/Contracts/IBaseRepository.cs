using BlazorOnlineStoreCliente.Server.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace BlazorOnlineStoreCliente.Server.Contracts
{
    public interface IBaseRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IPagedList<T>> GetPagedList(Pagination pagination);
        Task<T> GetById(int id);
        Task<T> Add(T newEntity);
        Task<T> Update(T updatedEntity);
        Task<T> Delete(int id);
        Task<IEnumerable<T>> Search(string searchKey);

    }
}
