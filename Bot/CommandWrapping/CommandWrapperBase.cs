using System;
using System.Linq;

namespace Bot.CommandWrapping
{
    public class CommandWrapperBase : ICommandWrapper
    {
        private readonly string[] _matchPrefix;
        private readonly Func<string[], string> _command;

        public CommandWrapperBase(string id, string[] matchPrefix, Func<string[], string> command)
        {
            Id = id;
            _matchPrefix = matchPrefix;
            _command = command;
        }

        public string HelpMessage { get; set; }

        public string Id { get; }

        public bool IsMatch(string[] prefix)
        {
            if (prefix.Length != _matchPrefix.Length)
                return false;
            return !prefix.Where((t, i) => t != _matchPrefix[i]).Any();
        }

        public string Execute(string[] parameters)
        {
            return _command(parameters);
        }
    }
}