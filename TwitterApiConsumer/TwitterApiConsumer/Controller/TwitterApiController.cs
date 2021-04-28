using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApiConsumer.View;
using TwitterApiConsumer.ViewModel;
using TwitterApiConsumer.Base.Service;
using TwitterApiConsumer.Base.Model;
using TwitterApiConsumer.Base.Store;

namespace TwitterApiConsumer.Controller
{
    /// <summary>
    /// This controller handles display of View
    /// </summary>
    public class TwitterApiController
    {
        private MainWindowViewModel _mainWindowViewModel;
        public TwitterApiController(MainWindowViewModel mainWindowViewModel)        
        {
            _mainWindowViewModel = mainWindowViewModel;
            Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            //Initialize service
            var service = new SampledStreamService(new MemoryStore<SampledStreamModel>());            

            //Initialize view and VM and Inject the view to UI
            var sampledStreamView = new SampledStreamView();
            var sampledStreamViewModel = new SampledStreamViewModel(service);
            sampledStreamView.DataContext = sampledStreamViewModel;
            _mainWindowViewModel.InjectViewInContainer(sampledStreamView);
            
        }
    }
}