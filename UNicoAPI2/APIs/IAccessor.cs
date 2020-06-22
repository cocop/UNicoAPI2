using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace UNicoAPI2.APIs
{
    /// <summary>
    /// APIアクセスインターフェイス
    /// </summary>
    public interface IAccessor
    {
        /// <summary>
        /// アクセッサの種別
        /// </summary>
        AccessorType Type { get; }

        /// <summary>
        /// アップロードするデータを取得する
        /// </summary>
        byte[] GetUploadData();

        /// <summary>
        /// アップロードストリーム、アクセッサタイプがダウンロードである場合は処理しない
        /// </summary>
        Task<Stream> GetUploadStreamAsync(int DataLength);

        /// <summary>
        /// ダウンロードストリーム、アクセッサタイプがアップロードである場合はレスポンス
        /// </summary>
        Task<WebResponse> GetDownloadStreamAsync();
    }
}