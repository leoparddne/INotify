using Common.Notify.Config;
using Common.Notify.Tools.DingTalk;
using Common.Notify.Tools.DingTalk.DTO.OA;
using Common.Notify.Tools.DingTalk.DTO.OA.Callback;
using Common.Notify.Tools.DingTalk.DTO.OA.WorkFlow;

namespace Common.Notify.OA
{
    public class DingTalkOAProvider : OABaseProvider
    {
        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="config"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public override object GetTemplate(BaseConfig config, object message)
        {
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new System.Exception("Error DingTalk Config");
            }

            TemplateSchema data = DingTalkHelper.GetTemplate("PROC-F0194ADC-0942-456F-9794-70E8505C7B43", dingTalkConfig);
            //var jsonValue = JsonConvert.SerializeObject(data);
            //Console.WriteLine(jsonValue);

            return data;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public override object StartProcess(BaseConfig config, object parameter)
        {
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new System.Exception("Error DingTalk Config");
            }

            if (parameter is not DingWorkFlowInstanceInDto inDto)
            {
                throw new System.Exception("Error DingTalk parameter( need  DingWorkFlowInstanceInDto )");
            }


            var result = DingTalkHelper.ProcessInstance(inDto, dingTalkConfig);

            return result;
        }


        /// <summary>
        /// 获取实例信息
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public override object GetDingInstanceDetail(BaseConfig config, object parameter)
        {
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new System.Exception("Error DingTalk Config");
            }

            if (parameter is not string instanceID)
            {
                throw new System.Exception("Error DingTalk parameter( need  DingWorkFlowInstanceInDto )");
            }


            var result = DingTalkHelper.GetDingInstanceDetail(instanceID, dingTalkConfig);

            return result;
        }


        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public override object AccectProcess(BaseConfig config, object parameter)
        {
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new System.Exception("Error DingTalk Config");
            }

            if (parameter is not DingAgreeProcessInDto inDto)
            {
                throw new System.Exception("Error DingTalk parameter( need  DingWorkFlowInstanceInDto )");
            }

            inDto.result = "agree";
            var result = DingTalkHelper.AccectProcess(inDto, dingTalkConfig);

            return result;
        }


        /// <summary>
        /// 审批拒绝
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public override object RefuseProcess(BaseConfig config, object parameter)
        {
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new System.Exception("Error DingTalk Config");
            }

            if (parameter is not DingAgreeProcessInDto inDto)
            {
                throw new System.Exception("Error DingTalk parameter( need  DingWorkFlowInstanceInDto )");
            }

            inDto.result = "refuse";

            var result = DingTalkHelper.AccectProcess(inDto, dingTalkConfig);

            return result;
        }

        public override void Callback(BaseConfig config, object parameter, System.Action<object> callbackAction)
        {
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new System.Exception("Error DingTalk Config");
            }

            if (parameter is not DingConnectionsOpenInDto inDto)
            {
                throw new System.Exception("Error DingTalk parameter( need  DingWorkFlowInstanceInDto )");
            }


            inDto.clientId = dingTalkConfig.AppKey;
            inDto.clientSecret = dingTalkConfig.AppSecret;

            DingTalkHelper.ConnectionsOpen(inDto, dingTalkConfig);
        }
    }
}
