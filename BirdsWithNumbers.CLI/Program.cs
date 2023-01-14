// Twitter API Stream Consumption

using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using BirdsWithNumbers.Status.Processing;
using BirdsWithNumbers.Status.Error;

namespace BirdsWithNumbers.CLI;

public class Program
{
    private const string ENVIRONMENT_TWITTER_BEARER_TOKEN = "TWITTER_BEARER_TOKEN";
    private const int INTERVAL = 2500;
    private const int TOP = 10;

    public static void Main(string[] args)
    {
        // Your Twitter API v2 credentials
        string? bearerToken = Environment.GetEnvironmentVariable(ENVIRONMENT_TWITTER_BEARER_TOKEN);


        if (bearerToken == null)
        {
            Console.WriteLine($"Could not find a twitter bearer token. Set your environment variable, {ENVIRONMENT_TWITTER_BEARER_TOKEN}, to your twitter bearer token.");
            return;
        }

        var history = new ErrorHandlerHistory();
        var totalTweets = new BirdCalculatorTotal();
        var hashTags = new BirdCalculatorHashTags();
        var topHashTags = new BirdCalculatorTop<string>(TOP, hashTags);
        var processor = new BirdMessageProcessorComposite(new IBirdMessageProcessor[] { totalTweets, topHashTags });
        var stream = new BirdStreamTwitter(bearerToken,processor,history);
        stream.Start();

        while(stream.Processing)
        {
            Console.Clear();
            Console.WriteLine($"Total number of tweets received: {totalTweets.Current}");
            Console.WriteLine($"Total number of seen hashtags: {hashTags.Current.Count()}");
            Console.WriteLine();
            Console.WriteLine($"Top {TOP} Hashtags:");
            Console.WriteLine("====================");

            foreach (var hashtag in topHashTags.Current)
            {
                Console.WriteLine($"#{hashtag.Data}: {hashtag.Count}");
            }

            Console.WriteLine();

            foreach (var error in history.Errors)
            {
                Console.Error.WriteLine(error.Message);
            }

            Thread.Sleep(INTERVAL);
        }
    }
}

