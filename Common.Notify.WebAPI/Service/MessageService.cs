using Common.Notify.DTO.Contact;
using Common.Notify.DTO.Message;
using Common.Notify.WebAPI.Config;
using Common.Notify.WebAPI.IService;
using Common.Toolkit.Extention;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Common.Notify.WebAPI.Service
{
    public class MessageService : IMessageService
    {
        private readonly string uploadDir;
        private readonly NotifyOption notifyOption;

        public MessageService(IOptions<NotifyOption> notifyOption)
        {
            this.notifyOption = notifyOption.Value;
            var tempPath = Path.GetTempPath();
            uploadDir = Path.Combine(tempPath, "Notify", "UploadedFiles");
        }

        public void Send(CommonSendMessageInDto dto)
        {
            MessageHelper.SendText(dto);
        }

        public void SendFile(CommonSendMessageInDto dto, IFormFileCollection files)
        {
            var savedFilePaths = new List<AttachFileConfig>();

            try
            {
                if (!files.IsNullOrEmpty())
                {
                    EnsureUploadDirectoryExists();

                    foreach (var file in files)
                    {
                        if (file.Length <= 0)
                        {
                            continue;
                        }

                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                        var filePath = Path.Combine(uploadDir, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        savedFilePaths.Add(new AttachFileConfig
                        {
                            FileName = fileName,
                            FilePath = filePath
                        });
                    }
                }

                dto.AttachFileList = savedFilePaths;

                SendMessage(dto);
            }
            finally
            {
                CleanupFiles(savedFilePaths);
            }
        }

        private void EnsureUploadDirectoryExists()
        {
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }
        }

        private void SendMessage(CommonSendMessageInDto dto)
        {
            dto.Timeout = notifyOption.Timeout;
            Send(dto);
        }

        private void CleanupFiles(List<AttachFileConfig> files)
        {
            if (files.IsNullOrEmpty()) return;

            foreach (var item in files)
            {
                try
                {
                    if (File.Exists(item.FilePath))
                    {
                        File.Delete(item.FilePath);
                    }
                }
                catch
                {
                    //忽略文件删除异常
                }
            }
        }
    }
}
