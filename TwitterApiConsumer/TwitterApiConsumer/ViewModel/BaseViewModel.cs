using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApiConsumer.Base.Service;

namespace TwitterApiConsumer.ViewModel
{
    public abstract class BaseViewModel<T> : NotifyPropertyChanged.NotifyPropertyChanged where T : class
    {
        public BaseViewModel(AbstractService<T> parm) 
        { 
        }
    }
}
