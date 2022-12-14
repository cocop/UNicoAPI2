namespace UNicoAPI2.VideoService.Converter.VideoPage
{
    public class UploadComment
    {
        public static VideoService.Response From(APIs.upload_comment.Response.packet Response)
        {
            var result = new VideoService.Response();

            if (Response.chat_result.status == "0")
                result.Status = Status.OK;
            else
            {
                switch (Response.chat_result.status)
                {
                    case "1": result.ErrorMessage = "同じコメントを投稿しようとしました"; break;
                    case "3": result.ErrorMessage = "投稿するためのキーが足りませんでした"; break;
                    default: break;
                }
                result.Status = Status.UnknownError;
            }

            return result;
        }

        public static VideoService.Response From(APIs.nvcomment.post.Response.Rootobject Response)
        {
            var result = Converter.Response.From<VideoService.Response>(Response.meta.status, Response.meta.message);
            return result;
        }
    }
}
