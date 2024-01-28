using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDataFromDb.Interface
{
    public interface IUpdateData<T>
    {
        public Task<char> UpdateDataAsync(T id);
    }
}
