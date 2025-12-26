using Newtonsoft.Json;

namespace IK.SCP.Application.Common.Response
{
    public class StatusResponse<T>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public string[] Messages { get; set; }
        public T Data { get; set; }
    }

    public class StatusResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; } = 200;

        [JsonProperty("messages")]
        public List<string> Messages { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data")]
        public object Data { get; set; }

        public StatusResponse()
        {
            Messages = new List<string>();
        }

        public StatusResponse(bool success, string message)
        {
            Messages = new List<string>();
            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }

            Success = success;
        }

        public StatusResponse(bool success, string message, int statusCode = 200)
        {
            Messages = new List<string>();
            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }

            Success = success;
            StatusCode = statusCode;
        }

        public StatusResponse(bool success, string message, object data)
        {
            Messages = new List<string>();
            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }

            Success = success;
            Data = data;
        }

        public void AddMessage(string msg, bool allowClear = false)
        {
            if (allowClear)
            {
                Messages?.Clear();
            }

            if (!Messages.Contains(msg) && !string.IsNullOrWhiteSpace(msg))
            {
                Messages.Add(msg);
            }
        }

        public void ClearMessages()
        {
            Messages?.Clear();
        }

        public static StatusResponse True(string message, int statusCode = 200, object? data = null)
        {
            return new StatusResponse(success: true, message, statusCode)
            {
                Data = data
            };
        }

        public static Task<StatusResponse> TrueAsync(string message, int statusCode = 200, object? data = null)
        {
            return Task.FromResult(True(message, statusCode, data));
        }

        public static StatusResponse False(string message, int statusCode = 200)
        {
            return new StatusResponse(success: false, message, statusCode);
        }

        public static Task<StatusResponse> FalseAsync(string message, int statusCode = 200)
        {
            return Task.FromResult(new StatusResponse(success: false, message, statusCode));
        }

        public static Task<StatusResponse> FalseAsync(List<string> messages, int statusCode = 200)
        {
            return Task.FromResult(False(messages, statusCode));
        }

        public static StatusResponse False(List<string> messages, int statusCode = 200)
        {
            return new StatusResponse
            {
                Data = null,
                Messages = messages,
                StatusCode = statusCode,
                Success = false
            };
        }

        public static StatusResponse TrueFalse(bool iif, string messageTrue, string messageFalse, int statusCode = 200, object? data = null)
        {
            if (iif)
            {
                return True(messageTrue, statusCode, data);
            }

            return False(messageFalse, statusCode);
        }

        public static Task<StatusResponse> TrueFalseAsync(bool iif, string messageTrue, string messageFalse, int statusCode = 200, object? data = null)
        {
            if (iif)
            {
                return TrueAsync(messageTrue, statusCode, data);
            }

            return FalseAsync(messageFalse, statusCode);
        }
    }
}
