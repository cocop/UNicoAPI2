using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNicoAPI2.VideoService.Converter.UserPage
{
    public class DownloadPublicMylistList
    {
        public static Response<Mylist.Mylist[]> From(Context context, Dictionary<string, string>[] Response)
        {
            var result = Converter.Response.From<Mylist.Mylist[]>("ok", null);

            result.Result = new Mylist.Mylist[Response.Length];
            for (int i = 0; i < result.Result.Length; i++)
            {
                result.Result[i] = context.IDContainer.GetMylist(Response[i]["id"]);
                result.Result[i].Title = Response[i]["name"];
            }

            return result;
        }
    }
}
