using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ResponeApi
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }

        public ApiResponse(int Code, T Data, string Message = "Success", bool Success = true)
        {
            this.Message = Message;
            this.Data = Data;
            this.Code = Code;
            this.Success = Success;
        }
    }
}