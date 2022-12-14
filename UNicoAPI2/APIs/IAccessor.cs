using System.Net;
using System.Threading.Tasks;

namespace UNicoAPI2.APIs
{
    /// <summary>
    /// APIアクセスインターフェイス
    /// </summary>
    public interface IAccessor
    {
        Task<WebResponse> GetDownloadStreamAsync();
    }
}