using System;
using BirdsWithNumbers.Status.Messaging;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BirdsWithNumbers.Status.Error;

namespace BirdsWithNumbers.Status.Processing
{
	/// <summary>
	/// Represents a bird stream for twitter.
	/// </summary>
	public sealed class BirdStreamTwitter : IBirdStream
	{
        private readonly string API = "https://api.twitter.com/2/tweets/sample/stream?tweet.fields=entities";

        private readonly string bearerToken;
		private readonly IBirdMessageProcessor processor;
        private readonly IErrorHandler errorHandler;

        /// <summary>
        /// Gets whether the stream is processing.
        /// </summary>
        public bool Processing { get; private set; } = false;

        /// <summary>
        /// Initializes a new instance of this object.
        /// </summary>
        /// <param name="bearerToken">The bearer token for communicating with the twitter api.</param>
        /// <param name="processor">The message processor for accepting the tweets.</param>
        /// <param name="errorHandler">The error handler.</param>
		public BirdStreamTwitter(string bearerToken, IBirdMessageProcessor processor, IErrorHandler errorHandler)
		{
			this.bearerToken = bearerToken;
			this.processor = processor;
            this.errorHandler = errorHandler;
		}

        /// <summary>
        /// Starts processing the tweet stream.
        /// </summary>
        public async void Start()
        {
			var client = new HttpClient();
			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");
			client.DefaultRequestHeaders.Add("User-Agent", "BirdsWithNumbers");

            Processing = true;
			var stream = await client.GetStreamAsync(API);

            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (String.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    try
                    {
                        var tweet = new Tweet(line);
                        processor.Add(tweet.ToBirdMessage());                     
                    }
                    catch (JsonReaderException e)
                    {
                        errorHandler.Handle(e);
                    }
                }
            }

            Processing = false;

        }

        /// <summary>
        /// Helper class that is used to build BirdMessage objects.
        /// </summary>
        private sealed class Tweet
        {
            private const string FIELD_DATA = "data";
            private const string FIELD_ID = "id";
            private const string FIELD_TEXT = "text";
            private const string FIELD_ENTITIES = "entities";
            private const string FIELD_HASHTAGS = "hashtags";
            private const string FIELD_TAG = "tag";

            private JObject json;

            public Tweet(string json)
            {
                this.json = JObject.Parse(json);
            }

            public BirdMessage ToBirdMessage()
            {
                BirdMessage message = new BirdMessage();
                message.Id = (string?)json[FIELD_DATA]?[FIELD_ID] ?? "";
                message.Message = (string?)json[FIELD_DATA]?[FIELD_TEXT] ?? "";

                JArray? hashTags = json[FIELD_DATA]?[FIELD_ENTITIES]?[FIELD_HASHTAGS] as JArray;

                if (hashTags != null)
                {
                    List<string> tags = new List<string>();

                    foreach (JObject tag in hashTags)
                    {
                        var name = (string?)tag.GetValue(FIELD_TAG);

                        if (name != null)
                        {
                            tags.Add(name);
                        }
                    }

                    message.HashTags = tags.ToArray();
                }

                return message;
            }
        }
    }
}

