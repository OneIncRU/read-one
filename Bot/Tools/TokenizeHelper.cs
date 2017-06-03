using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot.Tools
{
    public static class TokenizeHelper
    {
        private static readonly char[] BorderChars = { '\'', '"' };

        public static string[] ToTokens(this string input)
        {
            return SplitInput(input).Select(x=>RemoveBorders(x).Trim()).ToArray();
        }

        private static IEnumerable<string> SplitInput(string input)
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
            return result;
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