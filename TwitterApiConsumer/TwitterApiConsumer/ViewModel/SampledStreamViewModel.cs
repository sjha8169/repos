using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TwitterApiConsumer.Base.Logger;
using TwitterApiConsumer.Base.Service;
using TwitterApiConsumer.Base.Model;
using TwitterApiConsumer.Model;
using System.Collections.ObjectModel;
using System.Globalization;

namespace TwitterApiConsumer.ViewModel
{
    public class SampledStreamViewModel : BaseViewModel<SampledStreamModel>
    {
        #region Private Property        
        private SampledStreamService SampledStreamServiceInst { get; set; }
        private DateTime DateTimeStreamingStarted { get; set; }

        private static object _lock = new object();
        #endregion

        #region Constructor
        public SampledStreamViewModel(SampledStreamService service) : base(service)
        {
            SampledStreamServiceInst = service;            
            DateTimeStreamingStarted = SampledStreamServiceInst.StartSampledStream();
            StartTimerToFetchAndDisplayData();

        }
        #endregion

        #region Public Property    

        public SampleStreamComputeModel SampleStreamData
        {
            get
            {
                return _sampleStreamData;
            }
            set
            {
                _sampleStreamData = value;
                OnPropertyChanged();
            }
        }
        private SampleStreamComputeModel _sampleStreamData;       

        #endregion 



        #region Private Method
        /// <summary>
        /// Runs timer for given sampling period and displays data
        /// </summary>
        private void StartTimerToFetchAndDisplayData()
        {
            var timer = new System.Timers.Timer(TimeSpan.FromSeconds(SampledStreamServiceInst.SamplingPeriodInSeconds).TotalMilliseconds) { AutoReset = true, Enabled = true };
            timer.Elapsed += async (source, e) =>
            {
                var data = await SampledStreamServiceInst.RetriveModel();
                if (data != null && data.Item1 > 0 && data.Item2 != null && data.Item2.Count > 0)
                {
                    AnalyzeAndPopulateDataToUI(data);
                }
            };
        }
         
        /// <summary>
        /// Analyse tweet data and compute 
        /// </summary>
        /// <param name="data"></param>
        private void AnalyzeAndPopulateDataToUI(Tuple<long, List<SampledStreamModel>> data)
        {
            try
            {
                lock (_lock)
                {
                    SampleStreamData = new SampleStreamComputeModel();
                    SampleStreamData.NoOfTweetsReceived = data.Item1;

                    var timeSpan = DateTime.Now - DateTimeStreamingStarted;
                    var differenceHour = timeSpan.TotalHours;
                    var differenceMin = timeSpan.TotalMinutes;
                    var differenceSec = timeSpan.TotalSeconds;
                    if (differenceHour >= 1)
                    {
                        SampleStreamData.AverageTweetPerHour = (data.Item1 / differenceHour);
                    }
                    if (differenceMin >= 1)
                    {
                        SampleStreamData.AverageTweetPerMin = (data.Item1 / differenceMin);
                    }
                    if (differenceSec >= 1)
                    {
                        SampleStreamData.AverageTweetPerSec = (data.Item1 / differenceSec);
                    }

                    var emojiCount = data.Item2?.Where(h => h.Emoji != null)?.SelectMany(c => c.Emoji)?.Where(g => !string.IsNullOrEmpty(g))?.GroupBy(d => d)?.Select(e => new { Emoji = e.Key, Count = e.Count() })?.OrderByDescending(f => f.Count)?.ToList();
                    SampleStreamData.TopEmojis = emojiCount?.Select(c => c.Emoji)?.Take(3)?.ToList() != null ? new ObservableCollection<string>(emojiCount?.Select(c => c.Emoji)?.Take(3)?.ToList()) : new ObservableCollection<string>();

                    int totalNoOfEmojis = data.Item2.Where(c => c.HasEmoji).Count();
                    if (totalNoOfEmojis > 0)
                    {                        
                        SampleStreamData.PercentOfTweetsWithEmojis = ((double)totalNoOfEmojis / data.Item2.Count).ToString("P");
                    }

                    var hashTagCount = data.Item2.Where(h => h.HashTags != null)?.SelectMany(c => c.HashTags)?.Where(g => !string.IsNullOrEmpty(g))?.GroupBy(d => d)?.Select(e => new { HashTag = e.Key, Count = e.Count() })?.OrderByDescending(f => f.Count)?.ToList();
                    SampleStreamData.TopHashTags = hashTagCount?.Select(c => c.HashTag)?.Take(3)?.ToList() != null ? new ObservableCollection<string>(hashTagCount?.Select(c => c.HashTag)?.Take(3)?.ToList()) : new ObservableCollection<string>();

                    int totalNoOfUrl = data.Item2.Where(c => c.HasUrl).Count();
                    if (totalNoOfUrl > 0)
                    {
                        SampleStreamData.PercentofTweetsWithUrl = ((double)totalNoOfUrl / data.Item2.Count).ToString("P");
                    }

                    int totalNoOfPhotoUrl = data.Item2.Where(c => c.HasPhotoUrl).Count();
                    if (totalNoOfPhotoUrl > 0)
                    {
                        SampleStreamData.PercentofTweetsWithPhotoUrl = ((double)totalNoOfPhotoUrl / data.Item2.Count).ToString("P");
                    }

                    var urlDomainCount = data.Item2.Where(h => h.UrlDomain != null)?.SelectMany(c => c.UrlDomain)?.Where(g => !string.IsNullOrEmpty(g))?.GroupBy(d => d)?.Select(e => new { Domain = e.Key, Count = e.Count() })?.OrderByDescending(f => f.Count)?.ToList();
                    SampleStreamData.TopDomainsofUrls = urlDomainCount?.Select(c => c.Domain)?.Take(3)?.ToList() != null ? new ObservableCollection<string>(urlDomainCount?.Select(c => c.Domain)?.Take(3)?.ToList()) : new ObservableCollection<string>();
                }
            }
            catch(Exception ex)
            {
                new LogWritter().Write(ex.ToString());
            }
        }

        #endregion


    }
}
