using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDataFromDb.Interface
{
    public interface IExistData<T>
    {
        public Task<T> IsExist(T data);
    }
}
