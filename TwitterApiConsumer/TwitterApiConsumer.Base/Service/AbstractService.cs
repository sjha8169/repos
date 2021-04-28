using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApiConsumer.Base.Store;

namespace TwitterApiConsumer.Base.Service
{
    /// <summary>
    /// Forces all deriving classes to implement IStore<typeparamref name="T"/> as parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractService<T> where T: class
    {
        public AbstractService(IStore<T> store)
        { 
        }
    }
}
