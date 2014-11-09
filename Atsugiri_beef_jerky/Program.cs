using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atsugiri_beef_jerky.Twitter;

namespace Atsugiri_beef_jerky
{
    class Program
    {
        private static string[] targetString1 = { "はら", "おなか", "ぽなか", "腹" };
        private static string[] targetString2 = { "空いた", "すいた", "へっ", "減っ", "すきすき", "しゅきしゅき" };

        private static TimeLine timeLine;
        private static Post post;
        private static DateTime dt = new DateTime(0);

        private enum Status
        {
            Tweet,
            AddIgnore,
            RemoveIgnore,
            Follow,
            Remove,
            Continue
        }

        static void Main(string[] args)
        {
            Initialize();

            timeLine.StartUserStream();

            while (true)
            {
                var tip = timeLine.GetTweet();
                if (tip == null)
                {
                    if (dt == new DateTime(0))
                    {
                        dt = DateTime.Now.AddSeconds(60);
                    }

                    if (dt.CompareTo(DateTime.Now) == -1)
                    {
                        timeLine.ReconnectUserStream();   
                    }
                    continue;
                }

                dt = new DateTime(0);
                if (tip.IsRetweet == true) continue;

                WriteLog(tip.screenName, tip.name, tip.body);

                var status = CheckTweetBody(tip.body);
                if (status != Status.Continue)
                {
                    switch (status)
                    {
                        case Status.Tweet:
                            Tweet(tip.screenName, "厚切りビーフジャーキー", tip.id);
                            break;

                        case Status.AddIgnore:
                            AddIgnore();
                    }
                }
                    

            }
        }

        static void Initialize()
        {
            timeLine = new TimeLine();

        }

        static void WriteLog(string screenName, string name, string body)
        {
            Console.WriteLine(DateTime.Now.ToString("f")); //とりあえず
            Console.WriteLine(name + " / @" + screenName);
            Console.WriteLine(body);
            Console.WriteLine();
            Console.WriteLine();
        }

        static void Tweet(string screenName, string body, string inReplyToId = null)
        {
            if (post == null)
            {
                post = new Post();
            }
            post.PostReply(screenName, body, inReplyToId);
        }

        static bool IsIgnored(string screenName)
        {
            
        }

        static void AddIgnore()
        {
            
        }

        static Status CheckTweetBody(string tweetBody)
        {
            if (tweetBody.StartsWith("@") && !tweetBody.StartsWith("@atgr_beef_jerky")) 
                return Status.Continue;
            if (tweetBody.Contains("add ignore")) 
                return Status.AddIgnore;
            if (tweetBody.Contains("remove ignore")) 
                return Status.RemoveIgnore;
            if (tweetBody.Contains("follow me")) 
                return Status.Follow;
            if (tweetBody.Contains("remove me")) 
                return Status.Remove;

            foreach (var s1 in targetString1)
            {
                foreach (var s2 in targetString2)
                {
                    if (tweetBody.Contains(s1) && tweetBody.Contains(s2))
                    {
                        return Status.Tweet;
                    }
                }
            }
            return Status.Continue;
        }
    }
}
