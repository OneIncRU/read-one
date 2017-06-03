using System;
using ReadOne;

namespace Bot.Tools
{
    public static class CommandHelper
    {
        public static ICommand CreateAdd(string name)
        {
            return new Add
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        public static ICommand CreateReview(Guid id, int rate, string review, string readerName, string[] tags)
        {
            return new Review
            {
                Id = id,
                Comment = review,
                Rating = rate,
                Reader = readerName,
                Tags = tags
            };
        }
    }
}