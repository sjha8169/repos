using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApiConsumer.Model
{
    public class SampleStreamComputeModel : NotifyPropertyChanged.NotifyPropertyChanged
    {

        public long NoOfTweetsReceived
        { 
            get
            {
                return _noOfTweetsReceived;
            }
            set
            {
                _noOfTweetsReceived = value;
                OnPropertyChanged();
            }
        }
        private long _noOfTweetsReceived;

        public double AverageTweetPerHour 
        {
            get
            {
                return _averageTweetPerHour;
            }
            set
            {
                _averageTweetPerHour = value;
                OnPropertyChanged();
            }
        }
        private double _averageTweetPerHour;

        public double AverageTweetPerMin
        {
            get
            {
                return _averageTweetPerMin;
            }
            set
            {
                _averageTweetPerMin = value;
                OnPropertyChanged();
            }
        }
        private double _averageTweetPerMin;

        public double AverageTweetPerSec
        {
            get
            {
                return _averageTweetPerSec;
            }
            set
            {
                _averageTweetPerSec = value;
                OnPropertyChanged();
            }
        }
        private double _averageTweetPerSec;

        public ObservableCollection<string> TopEmojis
        {
            get
            {
                return _topEmojis;
            }
            set
            {
                _topEmojis = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _topEmojis;

        public string PercentOfTweetsWithEmojis
        {
            get
            {
                return _percentOfTweetsWithEmojis;
            }
            set
            {
                _percentOfTweetsWithEmojis = value;
                OnPropertyChanged();
            }
        }
        private string _percentOfTweetsWithEmojis;

        public ObservableCollection<string> TopHashTags
        {
            get
            {
                return _topHashTags;
            }
            set
            {
                _topHashTags = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _topHashTags;

        public string PercentofTweetsWithUrl 
        {
            get
            {
                return _percentofTweetsWithUrl;
            }
            set
            {
                _percentofTweetsWithUrl = value;
                OnPropertyChanged();
            }
        }
        private string _percentofTweetsWithUrl;

        public string PercentofTweetsWithPhotoUrl 
        {
            get
            {
                return _percentofTweetsWithPhotoUrl;
            }
            set
            {
                _percentofTweetsWithPhotoUrl = value;
                OnPropertyChanged();
            }
        }
        private string _percentofTweetsWithPhotoUrl;

        public ObservableCollection<string> TopDomainsofUrls 
        {
            get
            {
                return _topDomainsofUrls;
            }
            set 
            {
                _topDomainsofUrls = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _topDomainsofUrls;
    }
}
