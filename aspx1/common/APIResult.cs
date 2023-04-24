using System;
using Newtonsoft.Json;

namespace TaskSystem
{
    public enum BC_APIResultStatus
    {
        UN_KNOW = 0,
        SUCCESS = 100,
        FAIL = 200
    }
    public class BC_APIResult
    {
        public BC_APIResult() 
        {
            this.Code = (int)BC_APIResultStatus.UN_KNOW;
            this.Time = BC_Fmt.FormatToDateTimeStr(DateTime.Now);
        }
        // 响应码
        public int Code { get; set; }
        // 返回的数据
        public object Data { get; set; }
        // 时间戳
        public string Time { get; set; }
        // 消息
        public string Message { get; set; }

        public string GetSuccessAPIResult(object data, string message = "")
        {
            this.Code = (int)BC_APIResultStatus.SUCCESS;
            this.Data = data;
            this.Message = message;
            this.Time = BC_Fmt.FormatToDateTimeStr(DateTime.Now);
            return JsonConvert.SerializeObject(this);
        }
        public string GetFailAPIResult(object data, string message = "")
        {
            this.Code = (int)BC_APIResultStatus.FAIL;
            this.Data = data;
            this.Message = message;
            this.Time = BC_Fmt.FormatToDateTimeStr(DateTime.Now);
            return JsonConvert.SerializeObject(this);
        }
        public static string GetAPIResult(object data, int code = (int)BC_APIResultStatus.UN_KNOW, string message = "")
        {
            BC_APIResult result = new BC_APIResult();
            result.Code = code;
            result.Data = data;
            result.Message = message;
            result.Time = BC_Fmt.FormatToDateTimeStr(DateTime.Now);
            return JsonConvert.SerializeObject(result);
        }

    }
}