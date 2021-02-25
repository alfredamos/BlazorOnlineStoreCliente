using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Client.Contracts
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T newService);
        Task<T> Update(T updatedService);
        Task Delete(int id);
        Task<IEnumerable<T>> Search(string searchKey);
    }
}
