using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitterApiConsumer.Base.Logger;

namespace TwitterApiConsumer.Base.Store
{
    public class MemoryStore<T> : IStore<T> where T: class
    {
        #region private Field
        
        private ConcurrentQueue<KeyValuePair<DateTime, T>> _queue = new ConcurrentQueue<KeyValuePair<DateTime, T>>();
        private long _totalNoOfTweet;

        #endregion
        #region IStore implementation

        //Dequeue from Concurrentqueue
        public Tuple<long,List<T>> Retrive(DateTime dateTimeNow)
        {
            List<T> tList = new List<T>();
            //DateTime samplingPeriodLessTime =  dateTimeNow.AddSeconds((!string.IsNullOrEmpty(_samplingPeriodInSeconds) ? (-Convert.ToInt32(_samplingPeriodInSeconds)) : -1));
            if (!_queue.IsEmpty)
            {
                for (int i = 0; i <= _queue.Count; i++)
                {
                    KeyValuePair<DateTime, T> peekResult;
                    //try peek to see if time matches
                    if (_queue.TryPeek(out peekResult))
                    {
                        if (DateTime.Compare(peekResult.Key, dateTimeNow) <= 0)
                        {
                            KeyValuePair<DateTime, T> dequeueResult;
                            if(_queue.TryDequeue(out dequeueResult))
                            {
                                tList.Add(dequeueResult.Value);
                                _totalNoOfTweet++;//Count total no of tweets based on no. of tweets dequeued.
                            }
                        }
                    }
                }
            }
            //new LogWritter().Write($"queue count {_queue.Count.ToString()}, total tweet {_totalNoOfTweet.ToString()}, list count {tList.Count}");
            return new Tuple<long, List<T>>(_totalNoOfTweet, tList);
        }

        /// <summary>
        /// Enqueue in concurrentqueue
        /// </summary>
        /// <param name="objectToBeStored"></param>
        public bool Store(T objectToBeStored)
        {
            _queue.Enqueue(new KeyValuePair<DateTime, T>(DateTime.Now, objectToBeStored));
            //new LogWritter().Write(_queue.Count.ToString());
            return true;
        }
        #endregion
    }
}
