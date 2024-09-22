using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReusltApiMsg
    {
        public ReusltApiMsg()
        {
            Success = false;
            Message = "";
            Data = null;
        }
        public ReusltApiMsg(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public ReusltApiMsg(bool success, string message)
        {
            Success = success;
            Message = message;
            Data = null;
        }
        public ReusltApiMsg(bool success)
        {
            Success = success;
            Message = "";
            Data = null;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
