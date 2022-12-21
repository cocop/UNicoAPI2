using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UNicoAPI2.APIs.dmc.heartbeats.Response;
using UNicoAPI2.Connect;

namespace UNicoAPI2.VideoService.Video
{
    public class DmcVideoSource : IDisposable
    {
        public Uri ContentsUri { get; private set; }
        public HeartBeatsStatus Status { get; private set; }
        public event Action HeartbeatsError;

        Rootobject heartbeatsInfo;
        CancellationTokenSource tokenSource = new CancellationTokenSource();


        public DmcVideoSource(CookieContainer cookieContainer, Uri heartBeatsUri, Rootobject rootobject)
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

        private async Task<Rootobject> BeatAsync(CookieContainer cookieContainer, Uri heartBeatsUri, Rootobject rootobject)
        {
            var session = new Session<Rootobject>((flow) =>
            {
                flow.Return(new APIs.dmc.heartbeats.Accessor()
                {
                    CookieContainer = cookieContainer,
                    HeartBeatsUri = heartBeatsUri,
                    Data = rootobject.data
                });

                return new APIs.dmc.heartbeats.Parser().Parse(flow.GetResult());
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