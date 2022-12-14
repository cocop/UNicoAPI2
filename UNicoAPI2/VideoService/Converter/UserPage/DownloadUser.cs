using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNicoAPI2.VideoService.Converter.UserPage
{
    public class DownloadUser
    {
        public static Response<User.User> From(Context Context, Dictionary<string, string> Response)
        {
            var result = Converter.Response.From<User.User>("ok", null);
            result.Result = Context.IDContainer.GetUser(Response["id"]);
            result.Result.Icon = new Picture(Response["icon"], Context.CookieContainer);
            result.Result.Name = Response["name"];
            result.Result.Description = Response["description"];

            return result;
        }
    }
}
