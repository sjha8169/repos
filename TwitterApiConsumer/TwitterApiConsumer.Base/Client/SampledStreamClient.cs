using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.IO;
using TwitterApiConsumer.Base.Logger;

namespace TwitterApiConsumer.Base.Client
{
    public class SampledStreamClient : BaseClient
    {
        #region Private Fields

        private readonly string _sampleStreamUrl = ConfigurationManager.AppSettings["SampleStreamUrl"];
        private readonly string _noOfRetries = ConfigurationManager.AppSettings["NoOfRetries"];        
        private const string _queryString = "?tweet.fields=created_at,entities&expansions=attachments.media_keys&media.fields=type";
        private const int _retryPeriodInSeconds = 10;
        private HttpClient _client;


        #endregion

        #region Public Fields



        #endregion

        #region Constructor
        public SampledStreamClient()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _bearerToken);
            _client.BaseAddress = new Uri(_sampleStreamUrl);
        }
        #endregion

        #region Public Methods

        public void StartStreaming(Action<string> jsonToModelConverter)
        {
            //int nooftweetsreceived = 1;
            HttpResponseMessage response = null;
            int maxConnectionAttempt = Convert.ToInt32(_noOfRetries);
            int noOfConnectionAttempts = 0;

            while (noOfConnectionAttempts <= maxConnectionAttempt)
            {
                noOfConnectionAttempts++;
                try
                {
                    using (response = _client.GetAsync(_queryString, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult())

                        if (response.IsSuccessStatusCode)
                        {
                            using (StreamReader stream = new StreamReader(response.Content.ReadAsStreamAsync().GetAwaiter().GetResult()))
                            {
                                // looping over results from sampled stream api2
                                do
                                {
                                    string json = stream.ReadLineAsync().GetAwaiter().GetResult();

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        //new LogWritter().Write($"Packet {nooftweetsreceived}");
                                        //nooftweetsreceived++;
                                        jsonToModelConverter.Invoke(json);
                                    }

                                } while (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() && !stream.EndOfStream);
                            }
                            new LogWritter().Write("Streaming Finished");
                        }
                        else
                        {
                            new LogWritter().Write($"Request to Twitter Api did not go thru, attempting reconnection after {_retryPeriodInSeconds}s");
                            TryReconnection(noOfConnectionAttempts, maxConnectionAttempt);
                        }
                }
                catch (HttpRequestException hex)
                {
                    new LogWritter().Write($"Exception occured trying to connect to Twitter Api..{Environment.NewLine}{hex?.Message}{Environment.NewLine} attempting reconnection after {_retryPeriodInSeconds}s");                    

                }
                catch (Exception ex)
                {
                    new LogWritter().Write($"{ex.ToString()}, {Environment.NewLine} attempting reconnection after {_retryPeriodInSeconds}s");                    
                }
                finally
                {
                    TryReconnection(noOfConnectionAttempts, maxConnectionAttempt);
                }
            }
        }

        #endregion

        #region Private Method

        private void TryReconnection(int noOfConnectionAttempts, int maxConnectionAttempt)
        {
            if (noOfConnectionAttempts < maxConnectionAttempt)
            {
                System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(_retryPeriodInSeconds));
            }
        }
        #endregion

    }
}
