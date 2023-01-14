using System;
using BirdsWithNumbers.Status.Messaging;
using BirdsWithNumbers.Status.Processing;

namespace BirdsWithNumbers.Status.Tests.Processing;

public class BirdCalculatorHashTagsTests
{
	const string HASHTAG_PHOTOGRAPHY = "photography";
	const string HASHTAG_FUNNY = "funny";
	const string HASHTAG_PETS = "pets";

	private BirdMessage messageAboutPhotographyJourney;
	private BirdMessage messageAboutFunnyStuffMyCatDoes;
	private BirdMessage messageAboutPicturesOfMyDog;
	private BirdMessage messageAboutNewPet;

	private static BirdCalculatorHashTags CreateTestTarget()
	{
		return new BirdCalculatorHashTags();
	}

	public BirdCalculatorHashTagsTests()
	{
		messageAboutPhotographyJourney = new BirdMessage
		{
			Id = Guid.NewGuid().ToString(),
			Message = "I went to Arizona and took some pics! #photography",
			HashTags = new string[] { HASHTAG_PHOTOGRAPHY }
		};

		messageAboutFunnyStuffMyCatDoes = new BirdMessage
		{
			Id = Guid.NewGuid().ToString(),
			Message = "Cat misses jump, lol. #funny #pets",
			HashTags = new string[] { HASHTAG_PETS, HASHTAG_FUNNY }
		};

		messageAboutPicturesOfMyDog = new BirdMessage
		{
			Id = Guid.NewGuid().ToString(),
			Message = "Took some photos of my new puppy! #pets #photography",
			HashTags = new string[] { HASHTAG_PETS, HASHTAG_PHOTOGRAPHY }
		};

		messageAboutNewPet = new BirdMessage
		{
			Id = Guid.NewGuid().ToString(),
			Message = "Got a new bird. #pets",
			HashTags = new string[] { HASHTAG_PETS }
		};
	}

	[Fact]
	public void ShouldAggregateAllHashTags()
	{
		// Arrange.
		var target = CreateTestTarget();
		var expected = $"{HASHTAG_PETS}(3);{HASHTAG_PHOTOGRAPHY}(2);{HASHTAG_FUNNY}(1)";

		// Act.
		target.Add(messageAboutPhotographyJourney);
		target.Add(messageAboutFunnyStuffMyCatDoes);
		target.Add(messageAboutPicturesOfMyDog);
		target.Add(messageAboutNewPet);
		var strings = target.Current.OrderByDescending((x) => x.Count).Select((c) => String.Format("{0}({1})", c.Data, c.Count));
		var actual = String.Join(";", strings);

		// Assert.
		Assert.Equal(expected, actual);
	}
}


