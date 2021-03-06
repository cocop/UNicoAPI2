﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace UNicoAPI2.Connect
{
    public class Session<ResultType>
    {
        public static explicit operator Task<ResultType>(Session<ResultType> This)
        {
            return This.RunAsync(This.UntreatedCount, CancellationToken.None);
        }

        /// <summary>
        /// Task化
        /// </summary>
        public Task<ResultType> ToTask()
        {
            return (Task<ResultType>)this;
        }

        /// <summary>
        /// 未処理のアクセッサ数
        /// </summary>
        public int UntreatedCount { get { return accessers.Length - nowIndex; } }

        /// <summary>
        /// セッションの処理結果
        /// </summary>
        public ResultType Result { get; private set; }


        int nowIndex;
        byte[] data;
        Func<byte[], APIs.IAccessor>[] accessers;
        Func<byte[], ResultType> result;
        object key = new object();


        /// <summary>
        /// 処理するアクセッサを設定する
        /// </summary>
        /// <param name="Accessers">設定するアクセッサのリスト</param>
        public void SetAccessers(Func<byte[], APIs.IAccessor>[] Accessers, Func<byte[], ResultType> Result)
        {
            lock (key)
            {
                nowIndex = 0;
                data = null;
                accessers = Accessers;
                result = Result;
            }
        }


        /// <summary>
        /// ストリームを処理する
        /// </summary>
        /// <param name="RunCount">処理するアクセッサの数</param>
        /// <param name="Token">キャンセルトークン</param>
        public ResultType Run(int RunCount, CancellationToken Token)
        {
            for (int i = 0; i < RunCount; i++)
            {
                var accesser = accessers[nowIndex](data);

                if (accesser.Type == APIs.AccessorType.Upload)
                    RunUpload(Token, accesser);

                var streamResult = RunDownload(Token, accesser);

                if (streamResult != null)
                    return Result = (ResultType)streamResult;

                ++nowIndex;
            }

            if (UntreatedCount == 0)
                return Result = result(data);

            return default(ResultType);
        }

        private void RunUpload(CancellationToken Token, APIs.IAccessor accesser)
        {
            var udata = accesser.GetUploadData();
            var ustreamTask = accesser.GetUploadStreamAsync(udata.Length);
            ustreamTask.Wait(Token);

            var ustream = ustreamTask.Result;
            ustream.WriteAsync(udata, 0, udata.Length).Wait(Token);
        }

        private object RunDownload(CancellationToken Token, APIs.IAccessor accesser)
        {
            var dstreamTask = accesser.GetDownloadStreamAsync();
            dstreamTask.Wait(Token);

            var dresponse = dstreamTask.Result;
            var dstream = dresponse.GetResponseStream();

            if (dresponse is ResultType && UntreatedCount == 1)
                return dresponse;

            using (var stream = new MemoryStream())
            {
                var task = dstream.CopyToAsync(stream);
                task.Wait(Token);
                data = stream.ToArray();
            }

            return null;
        }

        /// <summary>
        /// ストリームを処理する
        /// </summary>
        /// <param name="RunCount">処理するアクセッサの数</param>
        /// <param name="Token">キャンセルトークン</param>
        public Task<ResultType> RunAsync(int RunCount, CancellationToken Token)
        {
            return Task.Run(() =>
            {
                lock (key)
                    return Run(RunCount, Token);
            });
        }

        /// <summary>
        /// すべてのストリームを処理する
        /// </summary>
        /// <param name="Token">キャンセルトークン</param>
        public ResultType Run(CancellationToken Token)
        {
            return Run(UntreatedCount, Token);
        }

        /// <summary>
        /// すべてのストリームを処理する
        /// </summary>
        /// <param name="Token">キャンセルトークン</param>
        public Task<ResultType> RunAsync(CancellationToken Token)
        {
            return Task.Run(() =>
            {
                lock (key)
                    return Run(UntreatedCount, Token);
            });
        }

    }
}
