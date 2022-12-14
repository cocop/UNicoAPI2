namespace UNicoAPI2.VideoService.Converter
{
    public static class Response
    {
        public static Response<T> From<T>(string Status, APIs.Response.error Error)
        {
            var result = new Response<T>();
            switch (Status)
            {
                case "ok":
                    result.Status = VideoService.Status.OK;
                    break;
                case "fail":
                    if (Error == null)
                    {
                        result.Status = VideoService.Status.UnknownError;
                        break;
                    }

                    switch (Error.code)
                    {
                        case "DELETED":
                            result.Status = VideoService.Status.Deleted;
                            break;
                        default:
                            result.Status = VideoService.Status.UnknownError;
                            break;
                    }
                    result.ErrorMessage = Error.description;
                    break;
            }

            return result;
        }

        public static Response<T> From<T>(int StatusCode, string ErrorMessage)
        {
            var result = new Response<T>();

            if (200 <= StatusCode && StatusCode < 300)
            {
                result.Status = Status.OK;
            }
            else
            {
                result.Status = Status.UnknownError;
                result.ErrorMessage = ErrorMessage;
            }

            return result;
        }
    }
}
