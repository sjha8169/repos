using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApiConsumer.Base.Store
{
    public interface IStore<T> where T: class
    {
        bool Store(T objectToBeStored);
        Tuple<long, List<T>> Retrive(DateTime dateTime);
    }
}
