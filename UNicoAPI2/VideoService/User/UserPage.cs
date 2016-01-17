using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNicoAPI2.VideoService.User
{
    /******************************************/
    /// <summary>
    /// ユーザーページへアクセスする
    /// </summary>
    /******************************************/
    public class UserPage
    {
        User target;
        Context context;

        internal UserPage(User Target, Context Context)
        {
            target = Target;
            context = Context;
        }
    }
}
