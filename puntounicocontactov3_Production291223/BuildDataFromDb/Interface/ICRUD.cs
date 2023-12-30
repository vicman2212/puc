using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDataFromDb.Interface
{
    public interface ICRUD<T>
    {
        public Task<int> ExistAsync(T id);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> InsertAsync(T data);
        public Task<bool> UpdateAsync(T id);
        public Task<T> SelectAsync(int id);
    }
}
