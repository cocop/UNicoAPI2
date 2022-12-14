using System.IO;
using System.Threading.Tasks;

namespace UNicoAPI2.APIs
{
    /// <summary>
    /// データ付きAPIアクセスインターフェイス
    /// </summary>
    public interface IAccessorWithUploadData : IAccessor
    {
        byte[] GetUploadData();

        Task<Stream> GetUploadStreamAsync(int DataLength);
    }
}
