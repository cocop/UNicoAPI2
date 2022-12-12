using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
