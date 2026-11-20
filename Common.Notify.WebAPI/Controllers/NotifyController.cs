using Common.Notify.DTO.Message;
using Common.Notify.WebAPI.Config;
using Common.Notify.WebAPI.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Common.Notify.WebAPI.Controllers
{
    public class NotifyController : BaesController
    {
        private readonly IMessageService messageService;
        private readonly NotifyOption notifyOption;

        public NotifyController(IMessageService messageService, IOptions<NotifyOption> notifyOption)
        {
            this.messageService = messageService;
            this.notifyOption = notifyOption.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        public void Send([FromBody] CommonSendMessageInDto dto)
        {
            dto.Timeout = notifyOption.Timeout;

            messageService.Send(dto);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Consumes("application/json", "multipart/form-data")]
        [DisableRequestSizeLimit]
        public void SendFile([FromForm] CommonSendMessageInDto dto)
        {
            messageService.SendFile(dto, HttpContext.Request.Form.Files);
        }
    }
}
