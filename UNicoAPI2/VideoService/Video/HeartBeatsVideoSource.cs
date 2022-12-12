using System;

namespace UNicoAPI2.VideoService.Video
{
    public class HeartBeatsVideoSource : IDisposable
    {
        public Uri VideoUri { get; }

        public HeartBeatsStatus Status { get; protected set; }

        public void Dispose()
        {
            Status = HeartBeatsStatus.Dispose;
        }
    }
}