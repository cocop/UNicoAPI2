using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNicoAPI2.Connect;

namespace UNicoAPI2.VideoService.Video
{
    public class DmcVideoSource : IDisposable
    {
        public Uri ContentsUri { get; private set; }
        public HeartBeatsStatus Status { get; private set; }

        APIs.heartbeats.Response.Rootobject heartbeatsInfo;
        CancellationTokenSource tokenSource = new CancellationTokenSource();


        public DmcVideoSource(CookieContainer cookieContainer, APIs.heartbeats.Response.Rootobject rootobject)
        {
            Status = HeartBeatsStatus.Active;

            ContentsUri = new Uri(rootobject.data.session.content_uri);

            Task.Run(() =>
            {
                do
                {
                    try
                    {
                        var beatTask = BeatAsync(cookieContainer, rootobject);
                        beatTask.Wait(tokenSource.Token);
                        heartbeatsInfo = beatTask.Result;
                        Task.Delay(rootobject.data.session.keep_method.heartbeat.lifetime).Wait(tokenSource.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                } while (heartbeatsInfo != null && !tokenSource.IsCancellationRequested);
            });
        }

        private async Task<APIs.heartbeats.Response.Rootobject> BeatAsync(CookieContainer cookieContainer, APIs.heartbeats.Response.Rootobject rootobject)
        {
            var session = new Session<APIs.heartbeats.Response.Rootobject>();
            session.SetAccessers(
                new Func<byte[], APIs.IAccessor>[]
                {
                    (data) =>
                    {
                        var accessor = new APIs.heartbeats.Accessor();
                        accessor.Setting(cookieContainer, rootobject.data.session);
                        return accessor;
                    }
                },
                (data) =>
                {
                    return new APIs.heartbeats.Parser().Parse(data);
                });

            return await session.RunAsync(tokenSource.Token);
        }

        public void Dispose()
        {
            Status = HeartBeatsStatus.Dispose;
            tokenSource.Cancel();
            ContentsUri = null;
        }
    }
}