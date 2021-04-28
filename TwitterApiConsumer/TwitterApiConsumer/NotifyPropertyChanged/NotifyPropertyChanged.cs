using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TwitterApiConsumer.NotifyPropertyChanged
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        //INotifyPropertyChanged implementation
        #region public Event

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private Method
        public void OnPropertyChanged([CallerMemberName] string member = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(member));
        }

        #endregion

    }
}
