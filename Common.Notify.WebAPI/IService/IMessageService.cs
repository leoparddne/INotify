using Common.Notify.DTO.Message;
using Microsoft.AspNetCore.Http;

namespace Common.Notify.WebAPI.IService
{
    public interface IMessageService
    {
        void Send(CommonSendMessageInDto dto);

        void SendFile(CommonSendMessageInDto dto, IFormFileCollection files);
    }
}
