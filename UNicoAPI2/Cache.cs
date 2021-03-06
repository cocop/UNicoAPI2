﻿using System;

namespace UNicoAPI2
{
    public class Cache
    {
        public static TimeSpan DefaultDeadline { get; set; } = TimeSpan.FromMinutes(30);
    }

    /******************************************/
    /// <summary>
    /// キャッシュ管理
    /// </summary>
    /******************************************/
    public class Cache<Type> : Cache
    {
        public static implicit operator Type(Cache<Type> This)
        {
            if (This.IsAvailab)
                return This.Value;

            return default(Type);
        }


        /// <summary>
        /// 有効期限
        /// </summary>
        public TimeSpan Deadline { get; set; }

        /// <summary>
        /// 最後に値を設定した時間
        /// </summary>
        public DateTime? GotTime { get; private set; } = null;

        /// <summary>
        /// 有効かどうか
        /// </summary>
        public bool IsAvailab
        {
            get
            {
                return GotTime != null && DateTime.Now < GotTime?.Add(Deadline);
            }
        }

        /// <summary>
        /// キャッシュ値
        /// </summary>
        public Type Value
        {
            get
            {
                return value;
            }

            set
            {
                GotTime = DateTime.Now;
                this.value = value;
                ChangedValue();
            }
        }
        Type value;
        public event Action ChangedValue = new Action(() => { });

        /******************************************/
        /******************************************/

        /// <summary></summary>
        public Cache()
        {
            Deadline = DefaultDeadline;
        }

        /// <summary>
        /// 値を初期化して作成
        /// </summary>
        public Cache(Type Value)
        {
            Deadline = DefaultDeadline;
            this.Value = Value;
        }

        /// <summary></summary>
        public void Clear()
        {
            Value = default(Type);
            GotTime = null;
        }
    }
}
