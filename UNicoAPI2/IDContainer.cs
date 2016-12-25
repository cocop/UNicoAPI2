﻿using System;
using System.Collections.Generic;
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
        public bool IsBuffering { get; set; } = true;

        Dictionary<string, VideoInfo> videoInfoTable = new Dictionary<string, VideoInfo>();
        Dictionary<string, Mylist> mylistTable = new Dictionary<string, Mylist>();
        Dictionary<string, User> userTable = new Dictionary<string, User>();

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
            videoInfoTable.Clear();
            mylistTable.Clear();
            userTable.Clear();
        }

        ManageType GetInstance<ManageType>(string ID, Dictionary<string, ManageType> Table, Func<string, ManageType> New)
        {
            ManageType result;

            if (!Table.TryGetValue(ID, out result))
            {
                result = New(ID);

                if (IsBuffering)
                    Table.Add(ID, result);
            }
            return result;
        }
    }
}
