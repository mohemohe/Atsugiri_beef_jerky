using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Rhinemaidens;

namespace Atsugiri_beef_jerky.Twitter
{
    class TimeLine
    {
        private Lorelei lorelei;

        public TimeLine()
        {
            lorelei = new Lorelei(ReceiveTokens.ConsumerKey,
                                  ReceiveTokens.ConsumerSecret,
                                  ReceiveTokens.AccessToken,
                                  ReceiveTokens.AccessTokenSecret);
        }

        public void StartUserStream()
        {
            lorelei.ConnectUserStream(true);
        }

        public void ReconnectUserStream()
        {
            lorelei.DisconnectUserStream();
            Thread.Sleep(500);
            lorelei.ConnectUserStream(true);
        }

        public TweetInfoPack GetTweet()
        {
            return lorelei.TryDequeueTweetInfoQueue();
        }
    }
}
