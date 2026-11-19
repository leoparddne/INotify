namespace Common.Notify.Tools.Feishu.DTO
{
    public class FeishuTokenOutDto : FeishuCommonOutDto
    {
        public string tenant_access_token { get; set; }
        public int expire { get; set; }
    }
}
