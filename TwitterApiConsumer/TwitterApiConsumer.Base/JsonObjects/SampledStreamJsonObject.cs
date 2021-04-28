using System;
using System.Collections.Generic;

namespace TwitterApiConsumer.Base.JsonObjects
{
    public class SampledStreamJsonObject
    {
        public Data data { get; set; }
        public Includes includes { get; set; }
    } 

    public class Url
    {
        public int start { get; set; }
        public int end { get; set; }
        public string url { get; set; }
        public string expanded_url { get; set; }
        public string display_url { get; set; }
        public int status { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }   

    public class Entities
    {
        public List<Url> urls { get; set; }       
        public List<HashTags> hashtags { get; set; }
    }    

    public class Data
    {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public string text { get; set; }       
        public Entities entities { get; set; }
        public Attachments attachments { get; set; }

    }

    public class HashTags
    {
        public string tag { get; set; }
    }

    public class Attachments
    {
        public List<string> media_keys { get; set; }
    }

    public class Media
    {
        public string media_key { get; set; }
        public string type { get; set; }
    }

    public class Includes
    {
        public List<Media> media { get; set; }
    }
}
