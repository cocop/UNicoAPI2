using System;
using System.Net;
using System.Threading.Tasks;
using UNicoAPI2.APIs;

namespace UNicoAPI2.Connect
{
    /// <summary>
    /// <para>セッションの流れを管理するインタフェイスです</para>
    /// <para>Sessionから使用されることを想定しています</para>
    /// </summary>
    public interface IUpFlow<ResultType>
    {
        /// <summary>
        /// 処理の一時的な結果を設定します
        /// </summary>
        void SetResult(ResultType result);

        /// <summary>
        /// 処理中のリクエストを設定します
        /// </summary>
        void SetResponse(WebResponse response);

        /// <summary>
        /// アクセッサー処理を登録します
        /// </summary>
        void SetSessionFunc(Action<IAccessor> func);
    }

    /// <summary>
    /// <para>セッションの流れを管理するインタフェイスです</para>
    /// <para>セッション外から利用されることを想定しています</para>
    /// </summary>
    public interface IDownFlow<ResultType>
    {
        /// <summary>
        /// 処理の一時的な結果を取得します
        /// </summary>
        ResultType GetResult();

        /// <summary>
        /// 処理中のリクエストを取得します
        /// </summary>
        WebResponse GetResponse();
    }

    /// <summary>
    /// <para>セッションの流れを管理するインタフェイスです</para>
    /// <para>SessionFlowから使用されることを想定しています</para>
    /// </summary>
    public interface IInnerDownFlow<ResultType> : IDownFlow<ResultType>
    {
        /// <summary>
        /// <para>リクエストの処理を停止させます</para>
        /// <para>このメソッドを実行した後にアクセッサを返すとレスポンスの処理が行われません</para>
        /// </summary>
        void Break();

        /// <summary>
        /// セッションにアクセッサを返して処理します
        /// </summary>
        void Return(IAccessor accessor);
    }

    /// <summary>
    /// セッションの流れを管理するオブジェクトです
    /// </summary>
    public class Flow<ResultType>
        : IUpFlow<ResultType>
        , IInnerDownFlow<ResultType>
    {
        public bool IsBreak { get; private set; } = false;

        ResultType result;
        WebResponse response;
        Action<IAccessor> sessionFunc;

        public ResultType GetResult()
        {
            return result;
        }

        public void SetResult(ResultType result)
        {
            this.result = result;
        }

        public WebResponse GetResponse()
        {
            return response;
        }

        public void SetResponse(WebResponse response)
        {
            this.response = response;
        }

        public void Break()
        {
            IsBreak = true;
        }

        public void Return(IAccessor accessor)
        {
            sessionFunc?.Invoke(accessor);
        }

        public void SetSessionFunc(Action<IAccessor> func)
        {
            sessionFunc = func;
        }
    }
}
