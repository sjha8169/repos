using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApiConsumer.Base.Model
{
    public class SampledStreamModel
    {
        public DateTime TweetTime { get; set; }
        public bool HasEmoji { get; set; }
        public List<string> Emoji { get; set; }
        public List<string> HashTags { get; set; }
        public bool HasUrl { get; set; }
        public bool HasPhotoUrl { get; set; }
        public List<string> UrlDomain { get; set; }
    }
}
