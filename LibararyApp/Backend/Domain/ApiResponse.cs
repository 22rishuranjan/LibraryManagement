using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ApiResponse<T>
    {

        public T Data { get; set; }

        public string Message { get; set; } = null;

        public Boolean Success { get; set; } = false;

        public int Status { get; set; } = 200;
    }
}
