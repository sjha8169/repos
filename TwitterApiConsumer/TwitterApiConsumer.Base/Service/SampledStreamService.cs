using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApiConsumer.Base.Store;
using TwitterApiConsumer.Base.Client;
using Newtonsoft.Json;
using TwitterApiConsumer.Base.JsonObjects;
using TwitterApiConsumer.Base.Model;
using System.Text.RegularExpressions;
using TwitterApiConsumer.Base.Constants;
using TwitterApiConsumer.Base.Logger;
using System.Configuration;

namespace TwitterApiConsumer.Base.Service
{
    public class SampledStreamService : AbstractService<SampledStreamModel>
    {
        #region Private Field

        private readonly string _samplingPeriodInSeconds = ConfigurationManager.AppSettings["SamplingPeriodInSeconds"];

        #endregion

        #region Construction

        public SampledStreamService(IStore<SampledStreamModel> store) : base(store)
        {
            DataStorage = store;
        }

        #endregion

        #region Private Property
        private IStore<SampledStreamModel> DataStorage { get; set; }

        #endregion

        #region Public Property

        public int SamplingPeriodInSeconds
        {
            get
            {
                int result;
                if (!string.IsNullOrEmpty(_samplingPeriodInSeconds) && int.TryParse(_samplingPeriodInSeconds, out result))
                {
                    return result;
                }
                else
                {
                    return 1;
                }
            }
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Starts streaming on background thread and returns Datetime when streaming started
        /// </summary>
        public DateTime StartSampledStream()
        {
            Task.Run(() => new SampledStreamClient().StartStreaming(ConvertFromJsonAndStore));
            return DateTime.Now;
        }

        /// <summary>
        /// starts a new thread per stream received to process and save
        /// </summary>
        /// <param name="json"></param>
        private void ConvertFromJsonAndStore(string json)
        {
            Task task = new TaskFactory().StartNew((jobj) =>
            {
                var sampledStreamModel = JsonToModelConverter(jobj.ToString());
                StoreModel(sampledStreamModel);
            }, json);

            //task.ContinueWith((ret) =>
            //{
            //    if(ret.IsFaulted)
            //    {
            //        new Logger.LogWritter().Write(ret.Exception.ToString());
            //    }
            //});
        }

        /// <summary>
        /// converts json string to .net object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public SampledStreamModel JsonToModelConverter(string json)
        {
            SampledStreamModel result = null;
            try
            {
                SampledStreamJsonObject jObject = JsonConvert.DeserializeObject<SampledStreamJsonObject>(json);
                if (jObject?.data != null)
                {
                    SampledStreamModel model = new SampledStreamModel();
                    string emoji = FindEmoji(jObject.data.text);
                    model.Emoji = !string.IsNullOrEmpty(emoji) ? new List<string>() { emoji } : new List<string>();
                    model.HasEmoji = !string.IsNullOrEmpty(emoji);

                    if (jObject.data.entities?.hashtags != null && jObject.data.entities.hashtags.Count > 0)
                    {
                        model.HashTags = jObject.data.entities.hashtags.Select(c => c.tag).ToList();
                    }
                    if (jObject.data.entities?.urls != null && jObject.data.entities.urls.Count > 0)
                    {
                        model.HasUrl = true;
                        if (jObject?.includes?.media != null && jObject.includes.media.Count > 0 && jObject.includes.media.Any(c => c.type.ToLower() == "photo"))
                        {
                            model.HasPhotoUrl = true;
                        }
                        model.UrlDomain = new List<string>();
                        foreach (var url in jObject.data.entities.urls)
                        {
                            Uri uri = new Uri(url.expanded_url);
                            model.UrlDomain.Add(uri.Host);
                        }
                    }
                    result = model;
                }
            }
            catch(Exception ex)
            {
                new LogWritter().Write(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// Saves 
        /// </summary>
        /// <param name="sampledStreamModel"></param>
        /// <returns></returns>
        public bool StoreModel(SampledStreamModel sampledStreamModel)
        {
            bool result = false;
            try
            {
                DataStorage.Store(sampledStreamModel);
                result = true;
            }
            catch (Exception ex)
            {
                new Logger.LogWritter().Write(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// Retrived data for sampled period
        /// </summary>
        /// <returns></returns>
        public async Task<Tuple<long, List<SampledStreamModel>>> RetriveModel()
        {
            Tuple<long, List<SampledStreamModel>> result = null;
            try
            {
                result = await Task.Run(() => DataStorage.Retrive(DateTime.Now));
            }
            catch(Exception ex)
            {
                new Logger.LogWritter().Write(ex.ToString());
            }
            return result;
        }

        #endregion

        #region Private Method

        private string FindEmoji(string text)
        {           
            Regex rgx = new Regex(Emoji.EmojiPattern);
            //var y = rgx.IsMatch(text);
            //to find emojis in text
            string emoji = rgx.Match(text).Value;
            return emoji;
        }

        #endregion 
    }
}
