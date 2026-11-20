using Common.Notify.WebAPI.Enums;
using Common.Notify.WebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Common.Notify.WebAPI.Filter
{
    /// <summary>
    /// 统一返回结果包装
    /// </summary>
    public class APIResultFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 执行方法体之后，对返回结果进行包装
        /// </summary>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //过滤exception
            if (context.Exception != null)
            {
                return;
            }

            //统一返回结构
            if (context.Result != null)
            {
                Type resultType = typeof(APIResponseModel<>);

                switch (context.Result)
                {
                    case OkResult:
                        context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.Success.GetHashCode(), Message = "" });
                        break;

                    case ObjectResult obj:
                        if (IsWrapped(obj.Value, resultType))
                        {
                            return;
                        }
                        context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.Success.GetHashCode(), Message = "", Data = obj.Value });
                        break;

                    case JsonResult jsonResult:
                        if (IsWrapped(jsonResult.Value, resultType))
                        {
                            return;
                        }
                        context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.Success.GetHashCode(), Message = "", Data = jsonResult.Value });
                        break;

                    case EmptyResult:
                        context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.Success.GetHashCode(), Message = "" });
                        break;

                    case FileContentResult:
                        break;

                    default:
                        var defaultObj = context.Result as ObjectResult;
                        if (defaultObj != null)
                        {
                            if (IsWrapped(defaultObj.Value, resultType))
                            {
                                return;
                            }
                            context.Result = new JsonResult(new APIResponseModel<object> { Code = ResponseEnum.Success.GetHashCode(), Message = "", Data = defaultObj.Value });
                        }
                        break;
                }
            }

            base.OnActionExecuted(context);
        }

        /// <summary>
        /// 判断返回值是否已是统一响应模型
        /// </summary>
        private static bool IsWrapped(object value, Type resultType)
        {
            if (value == null)
            {
                return false;
            }

            var returnType = value.GetType();
            return returnType.IsGenericType && returnType.GetGenericTypeDefinition() == resultType;
        }
    }
}
