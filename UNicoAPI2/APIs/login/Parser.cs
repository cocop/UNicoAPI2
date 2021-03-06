﻿using System.Text;
using System.Text.RegularExpressions;
using UNicoAPI2.VideoService.User;

namespace UNicoAPI2.APIs.login
{
    public class Parser : IParser<User>
    {
        static readonly Regex regex = new Regex("<p class=\"item profile-id\"><span class=\"label\">ID</span>(?<value>[0-9].*?)</p>");

        public User Parse(byte[] Value)
        {
            var http = Encoding.UTF8.GetString(Value);
            var result = regex.Match(http);

            if (!result.Success)
                return null;

            return new User(result.Groups["value"].Value);
        }
    }
}
