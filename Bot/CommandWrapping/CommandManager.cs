using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bot.CommandWrapping
{
    public class CommandManager : ICommandManager
    {
        private static readonly string[] ShowCommandPrefix = { "show", "commands" };
        private static readonly string[] ShowHelpPrefix = { "help" };
        private readonly List<ICommandWrapper> _wrappers = new List<ICommandWrapper>();
        private static readonly char[] BorderChars = { '\'', '"' };

        public CommandManager()
        {
            _wrappers.Add(new CommandWrapperBase("showCommands", ShowCommandPrefix, _ => ShowCommandList()));
            _wrappers.Add(new CommandWrapperBase("directHelp", ShowHelpPrefix, ShowHelp));
        }

        public ICommandManager AddCommand(ICommandWrapper commandWrapper)
        {
            if (commandWrapper == null)
                return this;
            _wrappers.Add(commandWrapper);
            return this;
        }

        public string[] ProcessInput(string input)
        {
            return ProcessInternal(input).Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string ProcessInternal(string input)
        {
            var tokens = SplitInput(input.Trim());
            for (var i = 1; i <= tokens.Length; i++)
            {
                var prefix = tokens.Take(i).Select(x => x.ToLower().Trim()).ToArray();
                var command = FindCommand(prefix);
                if (command != null)
                {
                    return command.Execute(tokens.Skip(i).Select(RemoveBorders).ToArray());
                }
            }
            return GetCommandNotFoundMessage();
        }

        private string ShowHelp(string[] args)
        {
            if (args.Length < 1)
            {
                return "Enter the command Id";
            }
            var command = _wrappers.FirstOrDefault(w => string.Equals(w.Id, args[0].Trim(), StringComparison.CurrentCultureIgnoreCase));
            var result = command == null
                ? GetCommandNotFoundMessage()
                : command.HelpMessage;
            return string.IsNullOrEmpty(result)
                ? "Can't find help for " + args[0]
                : result;
        }

        private string GetCommandNotFoundMessage()
        {
            return "Unknown command";
        }

        private ICommandWrapper FindCommand(string[] prefix)
        {
            return _wrappers.FirstOrDefault(w => w.IsMatch(prefix));
        }

        private string ShowCommandList()
        {
            return string.Join("\r\n", _wrappers.Select(w => w.Id));
        }

        private string[] SplitInput(string input)
        {
            var result = new List<string>();
            var tmp = input;
            while (!string.IsNullOrEmpty(tmp) && tmp.Contains(" "))
            {
                if (BorderChars.Contains(tmp[0]))
                {
                    var rightIndex = tmp.IndexOf(tmp[0], 1);
                    if (rightIndex > 0)
                    {
                        var token = tmp.Substring(0, rightIndex + 1);
                        tmp = tmp.Substring(rightIndex + 1).Trim();
                        result.Add(token);
                    }
                    else
                    {
                        result.Add(tmp);
                        tmp = string.Empty;
                    }
                }
                else
                {
                    var index = tmp.IndexOf(" ", StringComparison.Ordinal);
                    if (index > -1)
                    {
                        var token = tmp.Substring(0, index + 1);
                        result.Add(token);
                        tmp = tmp.Substring(index + 1).Trim();
                    }
                    else
                    {
                        result.Add(tmp);
                        tmp = string.Empty;
                    }
                }
            }
            if (!string.IsNullOrEmpty(tmp))
            {
                result.Add(tmp);
            }
            return result.ToArray();
        }

        private static string RemoveBorders(string param)
        {
            var left = param[0];
            var right = param[param.Length - 1];
            var length = param.Length;
            if (BorderChars.Contains(left))
            {
                return right == length
                    ? param.Substring(1, length - 2)
                    : param.Substring(1);
            }
            return param;
        }
    }
}