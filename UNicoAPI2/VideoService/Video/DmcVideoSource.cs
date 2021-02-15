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
        public event Action HeartbeatsError;

        APIs.heartbeats.Response.Rootobject heartbeatsInfo;
        CancellationTokenSource tokenSource = new CancellationTokenSource();


        public DmcVideoSource(CookieContainer cookieContainer, Uri heartBeatsUri, APIs.heartbeats.Response.Rootobject rootobject)
        {
            Status = HeartBeatsStatus.Active;
            ContentsUri = new Uri(rootobject.data.session.content_uri);

            Task.Run(() =>
            {
                do
                {
                    try
                    {
                        Task.Delay(rootobject.data.session.keep_method.heartbeat.lifetime / 3).Wait(tokenSource.Token);
                        var beatTask = BeatAsync(cookieContainer, heartBeatsUri, rootobject);
                        beatTask.Wait(tokenSource.Token);
                        heartbeatsInfo = beatTask.Result;
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    catch (Exception)
                    {
                        HeartbeatsError?.Invoke();
                    }
                } while (heartbeatsInfo != null && !tokenSource.IsCancellationRequested);
            });
        }

        private async Task<APIs.heartbeats.Response.Rootobject> BeatAsync(CookieContainer cookieContainer, Uri heartBeatsUri, APIs.heartbeats.Response.Rootobject rootobject)
        {
            var session = new Session<APIs.heartbeats.Response.Rootobject>();
            session.SetAccessers(
                new Func<byte[], APIs.IAccessor>[]
                {
                    (data) =>
                    {
                        var accessor = new APIs.heartbeats.Accessor();
                        accessor.Setting(cookieContainer, heartBeatsUri, rootobject.data);
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