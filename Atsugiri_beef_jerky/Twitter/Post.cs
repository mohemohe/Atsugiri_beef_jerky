using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhinemaidens;

namespace Atsugiri_beef_jerky.Twitter
{
    class Post
    {
        private Lorelei lorelei;

        public Post()
        {
            lorelei = new Lorelei(PostTokens.ConsumerKey,
                                  PostTokens.ConsumerSecret,
                                  PostTokens.AccessToken,
                                  PostTokens.AccessTokenSecret);
        }

        public void PostTweet(string body)
        {
            try
            {
                lorelei.PostTweet(body);
            }
            catch { }
        }

        public void PostReply(string screenName, string body, string inReplyToId)
        {
            var tweet = "@" + screenName + " " + body;

            try
            {
                lorelei.PostTweet(tweet, inReplyToId);
            }
            catch { }
        }
    }
}
