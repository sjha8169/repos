using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApiConsumer.Base.Model;
using TwitterApiConsumer.Base.Service;
using TwitterApiConsumer.Base.Store;

namespace TwitterApiConsumerTest
{
    [TestClass]
    public class SampledStreamServiceTest
    {
        [TestMethod]
        public void JsonToModelConverterTest()
        {

            string json = "{\"data\":{\"text\":\"RT @ohmypinks: jisoo best girl vote #HowYouLikeThat for #BestMusicΓÇª\",\"entities\":{\"mentions\":[{\"start\":3,\"end\":13,\"username\":\"ohmypinks\"}],\"hashtags\":[{\"start\":109,\"end\":124,\"tag\":\"HowYouLikeThat\"}]},\"id\":\"1386181858203930624\",\"created_at\":\"2021-04-25T04:54:43.000Z\"}}";
            var service = new SampledStreamService(new MemoryStore<SampledStreamModel>());
            var result = service.JsonToModelConverter(json);
            Assert.AreEqual(result.HashTags.Count, 1, "Json is converted to .Net object");
        }

        [TestMethod]
        public void StoreAndRetriveData()
        {
            string domain = "twitter.com";
            var service = new SampledStreamService(new MemoryStore<SampledStreamModel>());

            //store
            var sampledStreamModel = new SampledStreamModel() { HasEmoji = false, HasPhotoUrl = false, UrlDomain = new List<string> { domain } };
            var storeResult = service.StoreModel(sampledStreamModel);
            Assert.AreEqual(storeResult, true);

            //Retrive
            var retriveResult = service.RetriveModel().GetAwaiter().GetResult();
            Assert.IsNotNull(retriveResult);
            Assert.AreEqual(retriveResult.Item1, 1);
            Assert.AreEqual(retriveResult.Item2.Count, 1);
        }
    }
}
