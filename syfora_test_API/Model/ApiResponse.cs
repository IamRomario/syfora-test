using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace syfora_test_API.Model
{
    public class ApiResponse
    {
        public string Title { get; set; }

        public string Debug { get; set; }

        public Dictionary<string, string> Errors { get; set; }

        public HttpStatusCode Status { get; set; }

        public ApiResponse() { }
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiResponse() { }
    }
}
