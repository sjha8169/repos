using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TwitterApiConsumer.Controller;

namespace TwitterApiConsumer.ViewModel
{
    public class MainWindowViewModel : NotifyPropertyChanged.NotifyPropertyChanged
    {
        #region private Field
        private UserControl _contentControlContent;
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            new TwitterApiController(this);
        }
        #endregion

        #region Public Property

        public UserControl ContentControlContent
        {
            get
            {
                return _contentControlContent;
            }
            set
            {
                _contentControlContent = value;
                OnPropertyChanged();
            }
        }

        #endregion        

        #region Public Method

        public void InjectViewInContainer(UserControl userControl)
        {
            ContentControlContent = userControl;
            //System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate ()
            //{
            //    ContentControlContent = userControl;
            //});

        }

        #endregion

        #region Private Method



        #endregion
    }
}
