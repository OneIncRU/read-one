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
    }
}