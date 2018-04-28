using System;
using UNicoAPI2.VideoService.Mylist;
using UNicoAPI2.VideoService.User;
using UNicoAPI2.VideoService.Video;

namespace UNicoAPI2
{
    /******************************************/
    /// <summary>
    /// ID管理
    /// </summary>
    /******************************************/
    public class IDContainer
    {
        /// <summary>
        /// trueの場合一つのIDに付き一つのインスタンスが保証される
        /// </summary>
        public bool IsBuffering
        {
            get
            {
                return isBuffering;
            }
            set
            {
                if (!value)
                    Clear();

                isBuffering = value;
            }
        }
        bool isBuffering = true;

        /// <summary>
        /// ID毎の最大バッファ数
        /// </summary>
        public int BufferLength
        {
            get
            {
                return bufferLength;
            }
            set
            {
                bufferLength = value;
                videoInfoTable.BufferCount = value;
                mylistTable.BufferCount = value;
                userTable.BufferCount = value;
            }
        }
        int bufferLength = 10;

        BufferingManager<VideoInfo> videoInfoTable = new BufferingManager<VideoInfo>();
        BufferingManager<Mylist> mylistTable = new BufferingManager<Mylist>();
        BufferingManager<User> userTable = new BufferingManager<User>();

        /// <summary>動画情報を取得する</summary>
        /// <param name="ID">動画ID</param>
        public VideoInfo GetVideoInfo(string ID)
        {
            return GetInstance(ID, videoInfoTable, (id) => new VideoInfo(id));
        }

        /// <summary>マイリストを取得する</summary>
        /// <param name="ID">マイリストID</param>
        public Mylist GetMylist(string ID)
        {
            return GetInstance(ID, mylistTable, (id) => new Mylist(id));
        }

        /// <summary>ユーザー情報を取得する</summary>
        /// <param name="ID">ユーザーID</param>
        public User GetUser(string ID)
        {
            return GetInstance(ID, userTable, (id) => new User(id));
        }

        /// <summary>
        /// バッファを全て削除する
        /// </summary>
        public void Clear()
        {
            lock (videoInfoTable)
            lock (mylistTable)
            lock (userTable)
            {
                videoInfoTable.Clear();
                mylistTable.Clear();
                userTable.Clear();
            }
        }

        ManageType GetInstance<ManageType>(string ID, BufferingManager<ManageType> Table, Func<string, ManageType> New)
        {
            lock (Table)
            {
                if (IsBuffering)
                    return Table.Get(ID, New);

                return New(ID);
            }
        }
    }
}
