namespace Common.Notify.WebAPI.Model
{
    /// <summary>
    /// 统一响应模型
    /// </summary>
    public class APIResponseModel<T>
    {
        /// <summary>
        /// 响应码
        /// 0--成功
        /// 1--失败
        /// 401--未授权
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
