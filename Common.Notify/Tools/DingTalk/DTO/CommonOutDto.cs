namespace Common.Notify.Tools.DingTalk.DTO
{
    public class CommonOutDto<T>
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public T result { get; set; }
        public string request_id { get; set; }
    }
}
