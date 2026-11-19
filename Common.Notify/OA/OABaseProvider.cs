using Common.Notify.Config;
using System;

namespace Common.Notify.OA
{
    public abstract class OABaseProvider
    {

        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="config"></param>
        /// <param name="message"></param>
        public abstract object GetTemplate(BaseConfig config, object message);

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract object StartProcess(BaseConfig config, object parameter);


        public abstract object GetDingInstanceDetail(BaseConfig config, object parameter);

        /// <summary>
        /// 审批同意
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract object AccectProcess(BaseConfig config, object parameter);


        /// <summary>
        /// 审批拒绝
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract object RefuseProcess(BaseConfig config, object parameter);

        /// <summary>
        /// 审批流程回调
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <param name="callbackAction"></param>
        public abstract void Callback(BaseConfig config, object parameter, Action<object> callbackAction);

    }
}