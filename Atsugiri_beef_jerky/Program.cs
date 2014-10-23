using System;
using System.Collections.Generic;
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

        static void Main(string[] args)
        {
            timeLine = new TimeLine();
            timeLine.StartUserStream();

            while (true)
            {
                var tip = timeLine.GetTweet();
                if (tip == null) continue;
                if (tip.IsRetweet == true) continue;

                WriteLog(tip.screenName, tip.name, tip.body);
                if (CheckTweetBody(tip.body))
                {
                    if(post == null) post = new Post();
                    post.PostReply(tip.screenName, "厚切りビーフジャーキー", tip.id);
                }
            }
        }

        static void WriteLog(string screenName, string name, string body)
        {
            Console.WriteLine("@" + screenName + " " + name);
            Console.WriteLine(body);
            Console.WriteLine();
        }

        static bool CheckTweetBody(string tweetBody)
        {
            if (tweetBody.Contains("debug")) return true;
            if (tweetBody.StartsWith("@")) return false;

            foreach (var s1 in targetString1)
            {
                foreach (var s2 in targetString2)
                {
                    if (tweetBody.Contains(s1) && tweetBody.Contains(s2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
