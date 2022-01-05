using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace UNicoAPI2.Connect
{
    public class Session<ResultType>
    {
        public static explicit operator Task<ResultType>(Session<ResultType> This)
        {
            return This.RunAsync(CancellationToken.None);
        }

        /// <summary>
        /// Task化
        /// </summary>
        public Task<ResultType> ToTask()
        {
            return (Task<ResultType>)this;
        }

        /// <summary>
        /// セッションが完了したか
        /// </summary>
        public bool IsCompleted { get; private set; } = false;

        /// <summary>
        /// セッションの処理結果
        /// </summary>
        public ResultType Result { get; private set; }

        readonly Flow<byte[]> flow;
        readonly Func<IInnerDownFlow<byte[]>, ResultType> sessionFlow;


        /// <summary>
        /// セッションを作成します
        /// </summary>
        /// <param name="SessionFlow">セッションの処理</param>
        /// <param name="Complete">セッション終了時の最終結果を出力する関数</param>
        public Session(Func<IInnerDownFlow<byte[]>, ResultType> SessionFlow)
        {
            flow = new Flow<byte[]>();
            sessionFlow = SessionFlow;
        }

        /// <summary>
        /// ストリームを処理する
        /// </summary>
        /// <param name="Token">キャンセルトークン</param>
        public ResultType Run(CancellationToken Token)
        {
            flow.SetSessionFunc((accessor) =>
            {
                if (accessor.Type == APIs.AccessorType.Upload)
                    RunUpload(Token, accessor);

                var dres = GetDownloadResponse(Token, accessor);
                flow.SetResponse(dres);

                if (!flow.IsBreak)
                {
                    flow.SetResult(RunDownload(Token, dres));
                }
            });

            var result = sessionFlow(flow);
            IsCompleted = true;
            return result;
        }

        private void RunUpload(CancellationToken Token, APIs.IAccessor accesser)
        {
            var udata = accesser.GetUploadData();
            var ustreamTask = accesser.GetUploadStreamAsync(udata.Length);
            ustreamTask.Wait(Token);

            var ustream = ustreamTask.Result;
            ustream.WriteAsync(udata, 0, udata.Length).Wait(Token);
        }

        private WebResponse GetDownloadResponse(CancellationToken Token, APIs.IAccessor accesser)
        {
            var dstreamTask = accesser.GetDownloadStreamAsync();
            dstreamTask.Wait(Token);
            return dstreamTask.Result;
        }

        private byte[] RunDownload(CancellationToken Token, WebResponse response)
        {

            var dstream = response.GetResponseStream();

            using (var stream = new MemoryStream())
            {
                var task = dstream.CopyToAsync(stream);
                task.Wait(Token);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// ストリームを処理する
        /// </summary>
        /// <param name="Token">キャンセルトークン</param>
        public Task<ResultType> RunAsync(CancellationToken Token)
        {
            return Task.Run(() =>
            {
                return Run(Token);
            });
        }

    }
}
